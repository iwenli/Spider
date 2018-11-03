using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Processor.RequestExtractor;
using DotnetSpider.Extension;
using DotnetSpider.Extension.Model;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction;
using DotnetSpider.Extraction.Model;
using DotnetSpider.Extraction.Model.Attribute;
using Newtonsoft.Json;
using Spider.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Spider
{
	class QCCSpider : EntitySpider
	{
		List<string> List = new
			List<string>
		{   "上海速时网络科技有限公司"
			,"武汉振兴鑫诚装饰材料有限公司"
			,"广州德膳企业管理有限公司"
			,"佛山市威能建材有限公司	"
			,"湖北澳克玛农业有限公司	"
			,"秦皇岛合纵力餐饮管理有限公司"
			,"湖北一池天下生态农业科技有限公司"
			,"江苏欧象万盛木业有限公司"
			,"湖南芯瑞通光电科技有限公司"
			,"北京汉釜宫国际餐饮管理有限公司	"
			,"昆明闺蜜内衣有限公司	"
			,"中创时代(北京)国际投资管理有限公司	"
			,"北京瑞科同创能源科技有限公司	"
			,"惠州市中天阳光电力光伏有限公司	"
			,"秦皇岛合纵力餐饮管理有限公司	"
			,"昆明西汇农业科技有限公司	"
			,"山东普达企业管理咨询有限公司	"
			,"江苏盐城宏凯鹅苗孵化中心	"
			,"重庆泉佳喜餐饮文化有限公司	"
			,"南京九州盛世餐饮管理有限公司	"
			,"湖北妙农农产品有限公司	"
			,"秦皇岛合纵力科技开发有限公司	"
			,"昆明西汇农业科技有限公司	"
			,"广州德膳企业管理有限公司	"
			,"济南瑞粮餐饮管理有限公司	"
			,"上海郁隆禾餐饮管理有限公司	"
			,"浙江食叁味餐饮管理有限公司	"
			,"上海速时网络科技有限公司	"};

		protected override void OnInit(params string[] arguments)
		{
			Identity = ("QCCSpider_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss"));
			Downloader.AddCookie(new System.Net.Cookie("QCCSESSID", "tc8qnunpciiofa7ll1bgv7ts67", "/", ".qichacha.com"));

			AddPipeline(new MyPipeline());
			//AddPipeline(new JsonFileEntityPipeline());
			//AddPipeline(new SqlServerEntityPipeline());

			AddPageProcessor(new MyProcessor());
			foreach (var item in List)
			{
				AddRequests("https://www.qichacha.com/search?key=" + item.Trim());
			}


			AddEntityType<QCCEntity>();
		}

		private class MyProcessor : BasePageProcessor
		{
			public MyProcessor()
			{
			}

			protected override void Handle(Page page)
			{
				// 利用 Selectable 查询并构造自己想要的数据对象
				var elements = page.Selectable().SelectList(Selectors.XPath("//table[@class='m_srchList']/tbody/tr")).Nodes();
				foreach (var item in elements)
				{
					var _url = item.Select(Selectors.XPath(".//a[@class='ma_h1']/@href")).GetValue();
					page.AddTargetRequest(_url);
				}

				if (page.TargetUrl.Contains("firm_"))
				{
					page.AddResultItem("QCCEntity", new QCCEntity
					{
						Name = page.Selectable().Select(Selectors.XPath("//div[@id='company-top']/div[1]/div[2]/div[1]/h1"))?.GetValue()?.Trim() ?? "",
						Mobile = page.Selectable().Select(Selectors.XPath("//div[@id='company-top']/div[1]/div[2]/div[3]/span[1]/span[2]/span"))?.GetValue()?.Trim() ?? "",
						WebSite = page.Selectable().Select(Selectors.XPath("//div[@id='company-top']/div[1]/div[2]/div[3]/span[3]/a[1]/@href"))?.GetValue()?.Trim() ?? "",
						Email = page.Selectable().Select(Selectors.XPath("//div[@id='company-top']/div[1]/div[2]/div[4]/span[1]/span[2]/a[1]"))?.GetValue()?.Trim() ?? "",
						Address = page.Selectable().Select(Selectors.XPath("//div[@id='company-top']/div[1]/div[2]/div[4]/span[3]/a[1]"))?.GetValue()?.Trim() ?? "",
						Introduction = page.Selectable().Select(Selectors.XPath("//div[@id='company-top']/div[1]/div[2]/div[5]/span[2]"))?.GetValue()?.Trim() ?? "",

						RegisteredCapital = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[1]/td[2]"))?.GetValue()?.Trim() ?? "",
						PaidInCapital = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[1]/td[4]"))?.GetValue()?.Trim() ?? "",
						State = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[2]/td[2]"))?.GetValue()?.Trim() ?? "",
						FoundDate = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[2]/td[4]"))?.GetValue()?.Trim() ?? "",
						USCD = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[3]/td[2]"))?.GetValue()?.Trim() ?? "",
						TIN = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[3]/td[4]"))?.GetValue()?.Trim() ?? "",
						RN = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[4]/td[2]"))?.GetValue()?.Trim() ?? "",
						OC = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[4]/td[4]"))?.GetValue()?.Trim() ?? "",
						Type = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[5]/td[2]"))?.GetValue()?.Trim() ?? "",
						Industry = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[5]/td[4]"))?.GetValue()?.Trim() ?? "",
						ApprovalDate = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[6]/td[2]"))?.GetValue()?.Trim() ?? "",
						RA = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[6]/td[4]"))?.GetValue()?.Trim() ?? "",
						Area = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[7]/td[2]"))?.GetValue()?.Trim() ?? "",
						EName = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[7]/td[4]"))?.GetValue()?.Trim() ?? "",
						OName = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[8]/td[2]"))?.GetValue()?.Trim() ?? "",
						NOP = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[8]/td[4]"))?.GetValue()?.Trim() ?? "",
						StaffSize = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[9]/td[2]"))?.GetValue()?.Trim() ?? "",
						OperatingPeriod = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[9]/td[4]"))?.GetValue()?.Trim() ?? "",
						BusinessScope = page.Selectable().Select(Selectors.XPath("//*[@id='Cominfo']/table[2]/tbody/tr[11]/td[2]"))?.GetValue()?.Trim() ?? "",
						Uri = page.TargetUrl
					});
				}
			}
		}
		private class MyPipeline : BasePipeline
		{
			public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
			{

				foreach (var resultItem in resultItems)
				{
					if (resultItem["QCCEntity"] != null)
					{
						Console.WriteLine($"抽取数据: {JsonConvert.SerializeObject(resultItem["QCCEntity"])}");
					}
				}
				// 可以自由实现插入数据库或保存到文件
			}
		}

		class QCCEntity : IBaseEntity
		{
			/// <summary>
			/// 企业名称
			/// </summary>
			public string Name { set; get; }
			/// <summary>
			/// 企业官网
			/// </summary>
			public string WebSite { set; get; }
			/// <summary>
			/// 企业电话
			/// </summary>
			public string Mobile { set; get; }
			/// <summary>
			/// 企业邮箱
			/// </summary>
			public string Email { set; get; }
			/// <summary>
			/// 企业地址
			/// </summary>
			public string Address { set; get; }
			/// <summary>
			/// 企业简介
			/// </summary>
			public string Introduction { set; get; }
			/// <summary>
			/// 注册资本
			/// </summary>
			public string RegisteredCapital { set; get; }
			/// <summary>
			/// 实缴资本
			/// </summary>
			public string PaidInCapital { set; get; }
			/// <summary>
			/// 经营状态
			/// </summary>
			public string State { set; get; }
			/// <summary>
			/// 成立日期
			/// </summary>
			public string FoundDate { set; get; }
			/// <summary>
			/// 统一社会信用代码
			/// </summary>
			public string USCD { set; get; }
			/// <summary>
			/// 纳税人识别号
			/// </summary>
			public string TIN { set; get; }
			/// <summary>
			/// 注册号
			/// </summary>
			public string RN { set; get; }
			/// <summary>
			/// 组织机构代码
			/// </summary>
			public string OC { set; get; }
			/// <summary>
			/// 公司类型
			/// </summary>
			public string Type { set; get; }
			/// <summary>
			/// 行业
			/// </summary>
			public string Industry { set; get; }
			/// <summary>
			/// 核准日期
			/// </summary>
			public string ApprovalDate { set; get; }
			/// <summary>
			/// 登记机关
			/// </summary>
			public string RA { set; get; }
			/// <summary>
			/// 所属地区
			/// </summary>
			public string Area { set; get; }
			/// <summary>
			/// 英文名
			/// </summary>
			public string EName { set; get; }
			/// <summary>
			/// 曾用名
			/// </summary>
			public string OName { set; get; }
			/// <summary>
			/// 参保人数
			/// </summary>
			public string NOP { set; get; }
			/// <summary>
			/// 人员规模
			/// </summary>
			public string StaffSize { set; get; }
			/// <summary>
			/// 营业期限
			/// </summary>
			public string OperatingPeriod { set; get; }
			/// <summary>
			/// 经营范围
			/// </summary>
			public string BusinessScope { set; get; }
			/// <summary>
			/// 企查查网址
			/// </summary>
			public string Uri { set; get; }
		}
	}
}
