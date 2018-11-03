using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Processor.Filter;
using DotnetSpider.Core.Processor.RequestExtractor;
using DotnetSpider.Extension;
using DotnetSpider.Extension.Model;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction.Model;
using DotnetSpider.Extraction.Model.Attribute;
using DotnetSpider.Extraction.Model.Formatter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Demo
{
	public class WwwHao315ComSpider : EntitySpider
	{
		protected override void OnInit(params string[] arguments)
		{
			Identity = ("hao315_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss"));
			AddPipeline(new ConsoleEntityPipeline());
			//AddPipeline(new JsonFileEntityPipeline());
			//AddPipeline(new SqlServerEntityPipeline());

			AddRequests("http://www.hao315.com/paihangbang/index/1");
			AddEntityType<WwwHao315ComEntify>()
				.SetRequestExtractor(new AutoIncrementRequestExtractor("index/1"))
				.SetLastPageChecker(new LastPageChecker("index/177"));

			//.SetFilter(new PatternFilter("www\\.78\\.com/"));
			//AddEntityType<Www78CnEntify>().SetRequestExtractor(new XPathRequestExtractor(".//div[@class='pager']")).Filter = new PatternFilter("www\\.cnblogs\\.com/");
		}

	}
	[Entity(Expression = "//div[@class='right_text']/ul[not(@class)]")]
	class WwwHao315ComEntify : JMEntity
	{
		[Column]
		public override string Origin => "www.hao315.com";

		[Field(Expression = ".//li[3]/a[1]")]
		[Column]
		public override string Name { get; set; }

		[Field(Expression = ".//li[2]/a[1]")]
		[ReplaceFormatter(NewValue = "", OldValue = "[")]
		[ReplaceFormatter(NewValue = "", OldValue = "]")]
		[Column]
		public override string Industry { get; set; }


		[Field(Expression = ".//li[7]/a[1]")]
		[Column]
		public override string Area { get; set; }

		[Field(Expression = ".//li[4]/a[1]")]
		[Column]
		public override string Company { get; set; }

		[Field(Expression = ".//li[3]/a[1]/@href")]
		[Column]
		public override string Uri { get; set; }

		[Column]
		public override string Img => "";

		[Field(Expression = ".//li[8]/a[1]")]
		[Column]
		public override string Investment { get; set; }

		[Field(Expression = ".//li[6]")]
		[Column]
		public override string Like { get; set; }
		 
		[Column]
		public override string GrowthValue { get; set; } = "";
		[Field(Expression = ".//li[5]/span[1]")]
		[Column]
		public override string CreditScore { get; set; } = "";

		[Column]
		public override string OnlineTime { get; set; } = "";
	} 
	
}
