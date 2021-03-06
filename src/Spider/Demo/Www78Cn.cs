﻿using DotnetSpider.Core;
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
	public class Www78CnSpider : EntitySpider
	{
		protected override void OnInit(params string[] arguments)
		{
			Identity = ("78.cn_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss"));
			//AddPipeline(new ConsoleEntityPipeline());
			//AddPipeline(new JsonFileEntityPipeline());
			AddPipeline(new SqlServerEntityPipeline());

			AddRequests("http://www.78.cn/xmlist/?page=1");
			AddEntityType<Www78CnEntify>()
				.SetRequestExtractor(new AutoIncrementRequestExtractor("page=1"))
				.SetLastPageChecker(new LastPageChecker("page=507"));

			//.SetFilter(new PatternFilter("www\\.78\\.com/"));
			//AddEntityType<Www78CnEntify>().SetRequestExtractor(new XPathRequestExtractor(".//div[@class='pager']")).Filter = new PatternFilter("www\\.cnblogs\\.com/");
		}


	}

	[Entity(Expression = "//div[@class='contains_ld']")]
	class Www78CnEntify : JMEntity
	{  
		[Column]
		public override string Origin => "www.78.cn";

		[Field(Expression = ".//a[@class='contains_lp1']")]
		[Column]
		public override string Name { get; set; }

		[Field(Expression = ".//p[@class='contains_lp3']/span[1]/i")]
		[Column]
		public override string Industry { get; set; }


		[Field(Expression = ".//div[@class='contains_ld2']/p[2]/span[1]/i")]
		[ReplaceFormatter(NewValue = " ", OldValue = " &nbsp; ")]
		[Column]
		public override string Area { get; set; }

		[Field(Expression = ".//div[@class='contains_ld2']/p[3]/span[1]/i")]
		[Column]
		public override string Company { get; set; }

		[Field(Expression = ".//div[1]/a[1]/@href")]
		[Column]
		public override string Uri { get; set; }

		[Field(Expression = ".//div[1]/a[1]/img/@src")]
		[Column]
		public override string Img { get; set; }

		[Field(Expression = ".//span[@class='contains_pl4_s'][1]/i")]
		[Column]
		public override string Investment { get; set; }

		[Field(Expression = ".//span[@class='addAt']")]
		[Column]
		public override string Like { get; set; }

		[Field(Expression = ".//div[@class='contains_lp2']/span[1]/i")]
		[Column]
		public override string GrowthValue { get; set; }

		[Field(Expression = ".//div[@class='contains_lp2']/span[2]/i")]
		[Column]
		public override string CreditScore { get; set; }

		[Field(Expression = ".//div[@class='contains_lp2']/span[3]/i")]
		[Column]
		public override string OnlineTime { get; set; }
	}
}
