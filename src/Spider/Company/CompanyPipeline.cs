using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction.Model;
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
			Console.WriteLine($"[管道]等待{CompanySpider.SleepValue}秒处理下条数据");
			Thread.Sleep(1000 * CompanySpider.SleepValue);
			foreach (var data in datas)
			{
				var _entity = data as CompanyEntity;
				try
				{
					var result = Repository.Add(_entity).Result;
					Console.WriteLine($"抽取数据[{result}]: {_entity.ToString()}");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"抽取数据异常:{_entity.ToString()}  {ex.Message}");
				}
			}

			return datas.Count();
		}
	}
}
