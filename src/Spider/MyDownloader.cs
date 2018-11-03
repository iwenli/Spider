using DotnetSpider.Downloader;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider
{
	public class MyDownloader
	{
		//QCCSESSID = tc8qnunpciiofa7ll1bgv7ts67
		//https://www.qichacha.com/search?key=上海速时网络科技有限公司

		static HttpClientDownloader HttpClient;

		static MyDownloader()
		{
			HttpClient = new HttpClientDownloader();
			HttpClient.AddCookie(new System.Net.Cookie("QCCSESSID", "tc8qnunpciiofa7ll1bgv7ts67", "/", ".qichacha.com"));
		}

		public static  string GetHtml() {
			var _content =  HttpClient.Download(new Request("https://www.qichacha.com/search?key=上海速时网络科技有限公司")).Content.ToString();
			return _content;
		}
	}
}
