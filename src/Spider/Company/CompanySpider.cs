using DotnetSpider.Extension;
using DotnetSpider.Extension.Pipeline;
using Spider.Demo;
using Spider.Repository;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Spider.Company
{
	class CompanySpider : EntitySpider
	{
		public static int SleepValue = 10;

		public static string CurrentCompany = "";

		static ConcurrentQueue<string> CompanyNameQueue;


		protected override void OnInit(params string[] arguments)
		{
			InitData();

			Identity = ("CompanySpider_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss"));
			Downloader.AddCookie(new System.Net.Cookie("QCCSESSID", "tc8qnunpciiofa7ll1bgv7ts67", "/", ".qichacha.com"));
			SleepTime = 1000 * 5;
			EmptySleepTime = 1000 * 60 * 10;

			AddPipeline(new CompanyPipeline());
			AddPageProcessor(new CompanyProcessor());

			var _url = GetSearchUrl();
			if (_url.Length > 0)
			{
				AddRequests(_url);
			}
			AddEntityType<CompanyEntity>();
		}



		void InitData()
		{
			var _client = new BaseRepository<JMEntity>();
			var _context = _client.Context;

			var _listSugar = _client.Db.Context.Queryable<JMEntity>()
				.AS("spider_obj")
				.Where(m => m.Company != "")
				.Where(m => SqlFunc.Subqueryable<CompanyEntity>().Where(s => s.Name == m.Company).NotAny())
				.GroupBy(m => m.Company).OrderBy(m => m.Company, OrderByType.Desc).Select(m => m.Company.Trim());

			var _list = _listSugar.ToList();
			CompanyNameQueue = new ConcurrentQueue<string>(_list.Select(m => m.Trim()));

			Console.WriteLine($"待处理企业：{CompanyNameQueue.Count} 条");
		}
		public static string GetSearchUrl()
		{
			CurrentCompany = "";
			if (CompanyNameQueue.TryDequeue(out CurrentCompany))
			{
				if (CurrentCompany != null && CurrentCompany.Length > 2)
				{
					return "https://www.qichacha.com/search?key=" + CurrentCompany.Trim();
				}
			}
			return "";
		}
	}
}
