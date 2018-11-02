using Spider.Demo;
using Spider.Sample;
using System;
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

			new MySpider().Run();

			Console.WriteLine("按任意键退出...");
            Console.Read();
        }
    }
}
