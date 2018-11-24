using Newtonsoft.Json;
using Spider.Company;
using Spider.Demo;
using Spider.Repository;
using Spider.Sample;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spider
{
	/// <summary>
	/// 
	/// </summary>
	public class App
	{

		//http://www.cnblogs.com/grom/p/8931650.html


		/// <summary>
		/// 应用程序的主入口点
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			////建表
			//var _client = new BaseRepository<JMEntity>();
			//var _context = _client.Context;
			//_context.CreateTableByEntity(false, typeof(CompanyEntity));

			//new MySpider().Run();
			new CompanySpider().Run();

			//var _html = MyDownloader.GetHtml();

			//var _company = new CompanySpider();
			//_company.InitData();
			//while (true)
			//{
			//	var _name = "";
			//	if (CompanySpider.CompanyNameQueue.TryDequeue(out _name))
			//	{
			//		if (_name != null && _name.Length > 2)
			//		{
			//			 _html = MyDownloader.GetHtml(_name);
			//		}
			//	}
			//}

			#region 企查查

			//var _listSugar = _client.Db.Context.Queryable<JMEntity>()
			//	.AS("spider_zsjm_2018_11_03")
			//	.Where(m => SqlFunc.Subqueryable<QCCEntity>().Where(s => s.Name == m.Company).NotAny())
			//	.GroupBy(m => m.Company).Select(m => m.Company);
			//var _sql = _listSugar.ToSql();
			//Console.WriteLine(_sql);
			//var _list = _listSugar.ToList();

			//var _qccSpider = new QCCSpider();

			////MyDownloader.GetHtml();
			//foreach (var item in _list)
			//{
			//	_qccSpider.AddRequestByKey(item);
			//}

			//_qccSpider.Run(); 
			#endregion

			//var _client = new BaseRepository<JMEntity>();
			//var _context = _client.Context;

			//var _listSugar = _client.Db.Context.Queryable<JMEntity>()
			//	.AS("spider_obj")
			//	.Where(m => m.Company != "")
			//	.Where(m => SqlFunc.Subqueryable<CompanyEntity>().Where(s => s.Name == m.Company).NotAny())
			//	.GroupBy(m => m.Company).Select(m => m.Company.Trim());

			//var _list = _listSugar.ToList();
			//var CompanyNameQueue = new ConcurrentQueue<string>(_list.Select(m => m.Trim()));

			//Console.WriteLine($"待处理企业：{CompanyNameQueue.Count} 条");

			Console.WriteLine("按任意键退出...");
			Console.Read();
		}
	}
}
