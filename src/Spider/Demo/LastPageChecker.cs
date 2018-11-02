using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Demo
{
	class LastPageChecker : ILastPageChecker
	{
		string _value = "";
		internal LastPageChecker(string value)
		{
			_value = value;
		}
		public virtual bool IsLastPage(Page page)
		{
			return page.Request.Url.Contains(_value);
		}
	}
}
