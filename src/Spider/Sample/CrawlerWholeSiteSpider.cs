using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Processor.Filter;
using DotnetSpider.Core.Processor.RequestExtractor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Downloader;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Sample
{
	public class CrawlerWholeSiteSpider
	{
		public static void Run()
		{
			var  spider = DotnetSpider.Core.Spider.Create(
				// 使用内存队列调度程序
				new QueueDuplicateRemovedScheduler(),
				// 默认页面处理器将保存整个html，并通过正则表达式将URL提取到目标URL
				new DefaultPageProcessor
				{
					Filter = new PatternFilter(new[] { "cnblogs\\.com" }),
					RequestExtractor = new XPathRequestExtractor(".")
				})
				// 将抓取工具结果保存到文件夹中的文件：\ {running directory} \ data \ {crawler identity} \ {guid} .dsd
				.AddPipeline(new FilePipeline());

			// dowload html by http client
			spider.Downloader = new HttpClientDownloader();
			spider.Name = "CNBLOGS";
			// 4 threads 4线程
			spider.ThreadNum = 4;
			spider.TaskId = "cnblogs";
			// traversal deep 遍历深度
			spider.Depth = 3;
			spider.EncodingName = "UTF-8";
			// stop crawler if it can't get url from the scheduler after 30000 ms 当爬虫连续30秒无法从调度中心取得需要采集的链接时结束.
			spider.EmptySleepTime = 30000;
			// Set start/seed url
			spider.AddRequests("https://www.cnblogs.com/");
			// start crawler 启动爬虫
			spider.Run();
		}
	}
}
