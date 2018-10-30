using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Processor.Filter;
using DotnetSpider.Core.Processor.RequestExtractor;
using DotnetSpider.Extension;
using DotnetSpider.Extension.Model;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction.Model.Attribute;
using DotnetSpider.Extraction.Model.Formatter;
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
			AddPipeline(new ConsoleEntityPipeline());
			//AddPipeline(new JsonFileEntityPipeline());

			AddRequests("http://www.78.cn/xmlist/?page=1");
			AddEntityType<Www78CnEntify>()
				.SetRequestExtractor(new AutoIncrementRequestExtractor("page=1"))
				.SetLastPageChecker(new LastPageChecker());

			//.SetFilter(new PatternFilter("www\\.78\\.com/"));
			//AddEntityType<Www78CnEntify>().SetRequestExtractor(new XPathRequestExtractor(".//div[@class='pager']")).Filter = new PatternFilter("www\\.cnblogs\\.com/");
		}


	}
	class LastPageChecker : ILastPageChecker
	{
		public bool IsLastPage(Page page)
		{
			return page.Request.Url.Contains("page=507");
		}
	}
	[Entity(Expression = "//div[@class='contains_ld']")]
	class Www78CnEntify : JMEntity
	{
		public override string Origin => "www.78.cn";

		[Field(Expression = ".//a[@class='contains_lp1']")]
		public override string Name { get; set; }

		[Field(Expression = ".//p[@class='contains_lp3']/span[1]/i")]
		public override string Industry { get; set; }


		[Field(Expression = ".//div[@class='contains_ld2']/p[2]/span[1]/i")]
		[ReplaceFormatter(NewValue = " ", OldValue = " &nbsp; ")]
		public override string Area { get; set; }

		[Field(Expression = ".//div[@class='contains_ld2']/p[3]/span[1]/i")]
		public override string Company { get; set; }

		[Field(Expression = ".//div[1]/a[1]/@href")]
		public override string Uri { get; set; }

		[Field(Expression = ".//div[1]/a[1]/img/@src")]
		public override string Img { get; set; }

		[Field(Expression = ".//span[@class='contains_pl4_s'][1]/i")]
		public override string Investment { get; set; }

		[Field(Expression = ".//span[@class='addAt']")]
		public override int Like { get; set; }

		[Field(Expression = ".//div[@class='contains_lp2']/span[1]/i")]
		public override int GrowthValue { get; set; }

		[Field(Expression = ".//div[@class='contains_lp2']/span[2]/i")]
		public override int CreditScore { get; set; }

		[Field(Expression = ".//div[@class='contains_lp2']/span[3]/i")]
		public override string OnlineTime { get; set; }
	}


	/// <summary>
	/// 招商加盟基础数据
	/// </summary>

	[Schema("ZSJM", "project")]
	class JMEntity : BaseEntity
	{
		/// <summary>
		/// 项目来源
		/// </summary>
		[Column("Origin")]
		public virtual string Origin { set; get; }
		/// <summary>
		/// 项目名称
		/// </summary>
		[Column("Name")]
		public virtual string Name { set; get; }

		/// <summary>
		/// 所属行业
		/// </summary>
		[Column("Industry")]
		public virtual string Industry { set; get; }

		/// <summary>
		/// 所属区域
		/// </summary>
		[Column("Area")]
		public virtual string Area { set; get; }

		/// <summary>
		/// 公司
		/// </summary>
		[Column("Company")]
		public virtual string Company { set; get; }

		/// <summary>
		/// 项目url
		/// </summary>
		[Column("Uri")]
		public virtual string Uri { set; get; }

		/// <summary>
		/// 项目Img
		/// </summary>
		[Column("Img")]
		public virtual string Img { set; get; }

		/// <summary>
		/// 投资额
		/// </summary>
		[Column("Investment")]
		public virtual string Investment { set; get; }

		/// <summary>
		/// 点赞数
		/// </summary>
		[Column("Like")]
		public virtual int Like { set; get; }

		/// <summary>
		/// 成长值
		/// </summary
		[Column("GrowthValue")]
		public virtual int GrowthValue { set; get; }

		/// <summary>
		/// 信用分
		/// </summary>
		[Column("CreditScore")]
		public virtual int CreditScore { set; get; }

		/// <summary>
		/// 累计推广时长
		/// </summary>
		[Column("OnlineTime")]
		public virtual string OnlineTime { set; get; }
	}
}
