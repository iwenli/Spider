using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Extraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Spider.Company
{
	class CompanyProcessor : BasePageProcessor
	{
		public CompanyProcessor()
		{
		}

		protected override void Handle(Page page)
		{
			if (page.TargetUrl != "https://www.qichacha.com/" && !page.TargetUrl.Contains("search?key"))
			{
				//跳转了
				Logger.Log<CompanyProcessor>(LogLevel.Critical, 1, this, null,
						(a, b) =>
						{
							return $"[处理器]被屏蔽了。先停止吧";
						});
				Thread.Sleep(1000 * 1000000);
			}
			Logger.Log(LogLevel.Critical, 1, this, null,
						(a, b) =>
						{
							return $"[处理器]等待{CompanySpider.SleepValue}秒添加下条路由";
						});
			Thread.Sleep(1000 * CompanySpider.SleepValue);
			var _url = CompanySpider.GetSearchUrl();
			page.AddTargetRequest(_url);
		}
	}
}
