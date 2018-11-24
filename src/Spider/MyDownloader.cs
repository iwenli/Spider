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
			//HttpClient.AddCookie(new System.Net.Cookie("QCCSESSID", "tc8qnunpciiofa7ll1bgv7ts67", "/", ".qichacha.com"));

			//;   zg_did=%7B%22did%22%3A%20%2216739bd0d6296c-05eb8ff11222c3-b79183d-13c680-16739bd0d641f2%22%7D; UM_distinctid=1673e91a73fb78-04e1f4a087381c-b79183d-13c680-1673e91a740776; CNZZDATA1254842228=1245779446-1541473539-%7C1543045290; hasShow=1; zg_de1d1a35bfa24ce29bbf2c7eb17e6c4f=%7B%22sid%22%3A%201543048493469%2C%22updated%22%3A%201543050002607%2C%22info%22%3A%201542861950315%2C%22superProperty%22%3A%20%22%7B%7D%22%2C%22platform%22%3A%20%22%7B%7D%22%2C%22utm%22%3A%20%22%7B%7D%22%2C%22referrerDomain%22%3A%20%22www.baidu.com%22%2C%22cuid%22%3A%20%2210ba9ba4ff1721ac0de9616288793011%22%7D; Hm_lpvt_3456bee468c83cc63fb5147f119f1075=1543050003
			var _cookieList = new List<string>();
			_cookieList.Add("QCCSESSID=u1qlm3camss8fo0cg61ltd6010");
			//_cookieList.Add("acw_tc=8ccd104315414788538176746eabf969154afa766a1345dd6779dda6b3");
			_cookieList.Add("_uab_collina=154147884445273729213581");
			HttpClient.AddCookies(string.Join(";", _cookieList), ".qichacha.com");
		}
		public static string GetHtml(string name = "和壹（广州）品牌管理有限公司")
		{
			var _url = $"{"https:"}//www.qichacha.com/search?key={name}";
			var _content = HttpClient.Download(new Request(_url)).Content.ToString();
			var _content1 = HttpClient.Download(new Request(_url)).Content.ToString();
			var _content2 = HttpClient.Download(new Request(_url)).Content.ToString();
			var _content3 = HttpClient.Download(new Request(_url)).Content.ToString();
			return _content;
		}
	}
}
