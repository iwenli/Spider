using DotnetSpider.Extension;
using Microsoft.Extensions.Logging;
using Spider.Demo;
using Spider.Repository;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Spider.Company
{
	class CompanySpider : EntitySpider
	{
		public static int SleepValue = 5;

		public static string CurrentCompany = "";
		public static string NextCompany = "";

		static ConcurrentQueue<string> CompanyNameQueue;

		protected override void OnInit(params string[] arguments)
		{
			InitData();

			Identity = ("CompanySpider_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss"));
			Downloader.AddCookie(new System.Net.Cookie("QCCSESSID", "u1qlm3camss8fo0cg61ltd6010", "/", ".qichacha.com"));
			SleepTime = 1000 * 5;
			EmptySleepTime = 1000 * 60 * 10;

			AddPipeline(new CompanyPipeline());
			AddPageProcessor(new CompanyProcessor());

			AddRequests("https://www.qichacha.com/search?key=石头剪刀布地址电饭锅电饭锅电饭锅电饭锅多个");
			AddEntityType<CompanyEntity>();
		}

		void InitData()
		{
			var _client = new BaseRepository<JMEntity>();
			var _context = _client.Context;

			var _sql = @"SELECT  [company]
							FROM
							(
								SELECT [company],MAX(creation_time) AS creation_time
								FROM spider_obj
								WHERE NOT EXISTS
								(
									SELECT 1 FROM dbo.company WHERE name = company
								)
									  AND LEN(company) > 2
								GROUP BY [company]
							) A ORDER BY creation_time DESC";
			var _listSugar = _client.Db.Ado.SqlQuery<string>(_sql);

			//.Context.Queryable<JMEntity>()
			//.AS("spider_obj")
			//.Where(m => m.Company != "")
			//.Where(m => SqlFunc.Subqueryable<CompanyEntity>().Where(s => s.Name == m.Company).NotAny())
			//.GroupBy(m => m.Company).Select(m => new {name =  m.Company.Trim() });

			var _list = _listSugar.ToList();//.Skip(1000);
			CompanyNameQueue = new ConcurrentQueue<string>(_list.Select(m => m.Trim()));

			Logger.Log(LogLevel.Information, 1, this, null,
						(a, b) =>
						{
							return $"[爬虫]待处理企业：{CompanyNameQueue.Count} 条";
						});
		}
		public static string GetSearchUrl()
		{
			CurrentCompany = NextCompany;
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
	}
}
