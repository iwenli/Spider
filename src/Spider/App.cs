using Spider.Demo;
using Spider.Repository;
using Spider.Sample;
using SqlSugar;
using System;
using System.Collections.Generic;
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
			//Task.Run(()=>{ });

			//Task.Factory.StartNew(() => { });

			//new MySpider().Run();

			var _client = new BaseRepository<JMEntity>();
			var _context = _client.Context;
			//建表
			//_context.CreateTableByEntity(false, typeof(QCCEntity));

			var _listSugar = _client.Db.Context.Queryable<JMEntity>()
				.AS("spider_zsjm_2018_11_03")
				.Where(m => SqlFunc.Subqueryable<QCCEntity>().Where(s => s.Name == m.Company).NotAny())
				.GroupBy(m => m.Company).Select(m => m.Company);
			var _sql = _listSugar.ToSql();
			Console.WriteLine(_sql);
			var _list = _listSugar.ToList();

			var _qccSpider = new QCCSpider();

			//MyDownloader.GetHtml();
			foreach (var item in _list)
			{
				_qccSpider.AddRequestByKey(item);
			}

			_qccSpider.Run();

			Console.WriteLine("按任意键退出...");
            Console.Read();
        }
    }
}
