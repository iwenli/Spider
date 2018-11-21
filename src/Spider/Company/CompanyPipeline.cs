using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spider.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Spider.Company
{
	class CompanyPipeline : EntityPipeline
	{
		BaseRepository<CompanyEntity> Repository = new BaseRepository<CompanyEntity>();

		protected override int Process(IEnumerable<IBaseEntity> datas, dynamic sender = null)
		{
			var _name = CompanySpider.CurrentCompany;
			Logger.Log(LogLevel.Information, 1, this, null,
						(a, b) =>
						{
							return $"[管道]等待{CompanySpider.SleepValue}秒处理下条数据";
						});
			Thread.Sleep(1000 * CompanySpider.SleepValue);
			foreach (var data in datas)
			{
				var _entity = data as CompanyEntity;
				_entity.Seed = _name;
				try
				{
					var result = Repository.Add(_entity).Result;
					Logger.Log(LogLevel.Information, 1, this, null,
						(a, b) =>
						{
							return $"[管道][{result}-{_entity.Seed}]: {_entity.ToString()}";
						});

				}
				catch (Exception ex)
				{
					Logger.Log(LogLevel.Error, 1, this, null,
						(obj, e) =>
						{
							return $"[管道][{_entity.Seed}]异常:{_entity.ToString()}  {ex.Message}";
						});
				}
			}

			return datas.Count();
		}
	}
}
