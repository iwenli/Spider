using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Utility
{
	/// <summary>
	/// 时间戳和日期的转化
	/// </summary>
	public static class DateTimeUtility
	{
		/// <summary>
		/// 1970.1.1 UTC时间
		/// </summary>
		private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// 日期转换为时间戳
		/// </summary>
		/// <param name="dateTime">日期</param>
		/// <param name="isMilli">是否毫秒</param>
		/// <returns></returns>
		public static long ConvertToTimeStamp(this DateTime dateTime, bool isMilli = true)
		{
			var _ts = dateTime.AddHours(-8) - Jan1st1970;
			return (long)(isMilli ? _ts.TotalMilliseconds : _ts.TotalSeconds);
		}

		/// <summary>
		/// 时间戳转换为日期（时间戳单位秒）
		/// </summary>
		/// <param name="TimeStamp"></param>
		/// <returns></returns>
		public static DateTime ConvertToDateTime(this long timeStamp, bool isMilli = true)
		{
			var _dateTime = isMilli ? Jan1st1970.AddMilliseconds(timeStamp) : Jan1st1970.AddSeconds(timeStamp);
			return _dateTime.AddHours(8);
		}

	}
}
