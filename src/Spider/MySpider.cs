using DotnetSpider.Core.Processor.RequestExtractor;
using DotnetSpider.Extension;
using DotnetSpider.Extension.Pipeline;
using Spider.Demo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider
{
	class MySpider : EntitySpider
	{
		protected override void OnInit(params string[] arguments)
		{
			Identity = ("MySpider_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss"));
			//AddPipeline(new ConsoleEntityPipeline());
			//AddPipeline(new JsonFileEntityPipeline());
			AddPipeline(new SqlServerEntityPipeline());


			AddRequests("http://www.hao315.com/paihangbang/index/1");
			AddEntityType<WwwHao315ComEntify>()
				.SetRequestExtractor(new AutoIncrementRequestExtractor("index/1"))
				.SetLastPageChecker(new LastPageChecker("index/177"));

			AddRequests("http://www.78.cn/xmlist/?page=1");
			AddEntityType<Www78CnEntify>()
				.SetRequestExtractor(new AutoIncrementRequestExtractor("page=1"))
				.SetLastPageChecker(new LastPageChecker("page=507"));
		}
	}
}
