using DotnetSpider.Extension.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Demo
{
	/// <summary>
	/// 加盟项目实体基类

	[Schema("bds260192675_db", "spider_zsjm", TableNamePostfix.Today)]
	class JMEntity : BaseEntity
	{
		/// <summary>
		/// 项目来源
		/// </summary>
		public virtual string Origin { set; get; }
		/// <summary>
		/// 项目名称
		/// </summary> 
		public virtual string Name { set; get; }

		/// <summary>
		/// 所属行业
		/// </summary> 
		public virtual string Industry { set; get; }

		/// <summary>
		/// 所属区域
		/// </summary> 
		public virtual string Area { set; get; }

		/// <summary>
		/// 公司
		/// </summary> 
		public virtual string Company { set; get; }

		/// <summary>
		/// 项目url
		/// </summary> 
		public virtual string Uri { set; get; }

		/// <summary>
		/// 项目Img
		/// </summary> 
		public virtual string Img { set; get; }

		/// <summary>
		/// 投资额
		/// </summary 
		public virtual string Investment { set; get; }

		/// <summary>
		/// 点赞数
		/// </summary> 
		public virtual string Like { set; get; }

		/// <summary>
		/// 成长值
		/// </summary 
		public virtual string GrowthValue { set; get; }

		/// <summary>
		/// 信用分
		/// </summary> 
		public virtual string CreditScore { set; get; }

		/// <summary>
		/// 累计推广时长
		/// </summary> 
		public virtual string OnlineTime { set; get; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
