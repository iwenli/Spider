using DotnetSpider.Extension;
using Microsoft.Extensions.Logging;
using Spider.Demo;
using Spider.Repository;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spider.Company
{
	class CompanySpider : EntitySpider
	{
		public static int SleepValue = 5;

		public static string CurrentCompany = "";
		public static string NextCompany = "";

		public static ConcurrentQueue<string> CompanyNameQueue;

		protected override void OnInit(params string[] arguments)
		{
			InitData();

			Identity = ("CompanySpider_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss"));
			var _cookieList = new List<string>();
			_cookieList.Add("QCCSESSID=u1qlm3camss8fo0cg61ltd6010");
			_cookieList.Add("_uab_collina=154147884445273729213581");
			Downloader.AddCookies(string.Join(";", _cookieList), ".qichacha.com");
			SleepTime = 1000 * 5;
			EmptySleepTime = 1000 * 60 * 10;

			AddPipeline(new CompanyPipeline());
			AddPageProcessor(new CompanyProcessor());

			AddRequests("https://www.qichacha.com/search?key=CompanyProcessors");
			AddEntityType<CompanyEntity>();
		}

		public void InitData()
		{
			var _client = new BaseRepository<JMEntity>();
			var _context = _client.Context;

			var _sql = @"SELECT company FROM dbo.spider_obj WHERE is_qcc = 0 
							GROUP BY company
							ORDER BY MIN(creation_time) DESC";
			var _listSugar = _client.Db.Ado.SqlQuery<string>(_sql);

			//.Context.Queryable<JMEntity>()
			//.AS("spider_obj")
			//.Where(m => m.Company != "")
			//.Where(m => SqlFunc.Subqueryable<CompanyEntity>().Where(s => s.Name == m.Company).NotAny())
			//.GroupBy(m => m.Company).Select(m => new {name =  m.Company.Trim() });

			var _list = _listSugar.ToList();
			CompanyNameQueue = new ConcurrentQueue<string>(_list.Select(m => m.Trim()));

			//Logger.Log(LogLevel.Information, 1, this, null,
			//			(a, b) =>
			//			{
			//				return $"[爬虫]待处理企业：{CompanyNameQueue.Count} 条";
			//			});
		}
		public static string GetSearchUrl()
		{
			CurrentCompany = NextCompany;
			UpdateStatus(CurrentCompany);
			NextCompany = "";
			if (CompanyNameQueue.TryDequeue(out NextCompany))
			{
				if (NextCompany != null && NextCompany.Length > 2)
				{
					var _url = "https://www.qichacha.com/search?key=" + NextCompany.Trim();
					Console.WriteLine($"[爬虫]添加链接: {_url}");
					return _url;
				}
			}

			return "";
		}

		public static void UpdateStatus(string company)
		{
			Task.Run(async () =>
			{
				var _client = new BaseRepository<JMEntity>();
				var _sql = "UPDATE dbo.spider_obj SET is_qcc = 1 WHERE company = @company";
				var _paramter = new List<SugarParameter>();
				_paramter.Add(new SugarParameter("@company", company));
				var _result = await _client.Update(_sql, _paramter.ToArray());
				Console.WriteLine($"[爬虫]跟新[{company}]数据状态: {(_result ? "成功" : "失败")}");
			});
		}
	}
}
