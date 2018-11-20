using DotnetSpider.Extraction.Model;
using DotnetSpider.Extraction.Model.Attribute;
using DotnetSpider.Extraction.Model.Formatter;
using Spider.Utility;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Spider.Company
{
	/// <summary>
	/// 企业基本信息
	/// </summary>
	[SugarTable("company")]
	[Entity(Expression = "//*[@id='searchlist']/table/tbody/tr")]
	class CompanyEntity : IBaseEntity
	{
		/// <summary>
		/// 自增标识
		/// </summary>
		[SugarColumn(ColumnName = "id", IsNullable = false, IsIdentity = true)]
		public long Id { set; get; }
		/// <summary>
		/// 企业名称
		/// </summary>
		[SugarColumn(ColumnName = "name", IsNullable = false, IsPrimaryKey = true, Length = 100)]
		[Field(Expression = ".//td[2]/a")]
		[ReplaceFormatter(NewValue = "", OldValue = "<em>")]
		[ReplaceFormatter(NewValue = "", OldValue = "</em>")]
		public string Name { set; get; }
		/// <summary>
		/// 企业logo
		/// </summary>
		[SugarColumn(ColumnName = "logo", IsNullable = false, Length = 500)]
		[Field(Expression = ".//td[1]/img/@src")]
		public string Logo { set; get; }
		/// <summary>
		/// 联系人类型
		/// </summary>
		[SugarColumn(ColumnName = "contact_type", IsNullable = true, Length = 50)]
		[Field(Expression = ".//td[2]/p[1]")]
		[RegexFormatter(Pattern = @"(\s\S*?)：", Group = 1)]
		[ReplaceFormatter(NewValue = "", OldValue = " ")]
		//[RegexFormatter(Group = 1, Pattern = "")]
		public string ContactType { set; get; }
		/// <summary>
		/// 联系人名称
		/// </summary>
		[SugarColumn(ColumnName = "contact_name", IsNullable = true, Length = 50)]
		[Field(Expression = ".//td[2]/p[1]/a")]
		public string ContactName { set; get; }

		/// <summary>
		/// 注册资金
		/// </summary>
		[SugarColumn(ColumnName = "register_bankroll", IsNullable = true, Length = 50)]
		[Field(Expression = ".//td[2]/p[1]/span[1]")]
		[ReplaceFormatter(NewValue = "", OldValue = "注册资本：")]
		public string RegisterBankroll { set; get; }
		/// <summary>
		/// 成立时间
		/// </summary>
		[SugarColumn(ColumnName = "establish_time", IsNullable = true, Length = 50)]
		[Field(Expression = ".//td[2]/p[1]/span[2]")]
		[ReplaceFormatter(NewValue = "", OldValue = "成立时间：")]
		public string EstablishTime { set; get; }
		/// <summary>
		/// 邮箱
		/// </summary>
		[SugarColumn(ColumnName = "email", IsNullable = true, Length = 50)]
		[Field(Expression = ".//td[2]/p[2]")]
		[RegexFormatter(Pattern = @"邮箱：([\w\d@.]*)", Group = 1)]
		public string Email { set; get; }
		/// <summary>
		/// 电话
		/// </summary>
		[SugarColumn(ColumnName = "tel", IsNullable = true, Length = 50)]
		[Field(Expression = ".//td[2]/p[2]/span")]
		[ReplaceFormatter(NewValue = "", OldValue = "电话：")]
		public string Tel { set; get; }
		/// <summary>
		/// 注册地 //*[@id="searchlist"]/table/tbody/tr[1]/td[2]/p[3]
		/// </summary>
		[SugarColumn(ColumnName = "registe_area", IsNullable = true, Length = 500)]
		[Field(Expression = ".//td[2]/p[3]")]
		[ReplaceFormatter(NewValue = "", OldValue = "地址：")]
		[ReplaceFormatter(NewValue = "", OldValue = "<em>")]
		[ReplaceFormatter(NewValue = "", OldValue = "</em>")]
		[ReplaceFormatter(NewValue = "", OldValue = " ")]
		[ReplaceFormatter(NewValue = "", OldValue = "\n")]
		public string RegisteArea { set; get; }
		/// <summary>
		/// 状态
		/// </summary>
		[SugarColumn(ColumnName = "status", IsNullable = true, Length = 50)]
		[Field(Expression = ".//td[3]/span")]
		public string Status { set; get; }
		/// <summary>
		/// 企业详情地址
		/// </summary>
		[SugarColumn(ColumnName = "detail_uri", IsNullable = true, Length = 500)]
		[Field(Expression = ".//td[2]/a/@href")]
		public string DetailUri { set; get; }
		/// <summary>
		/// 记录时间
		/// </summary>
		[SugarColumn(ColumnName = "add_time", IsNullable = true)]
		public long AddTime { set; get; } = DateTime.Now.ConvertToTimeStamp();

		public override string ToString()
		{
			return $"名称：{Name} 状态：{Status} 联系人：{ContactName} 电话：{Tel}";
		}
	}
}
