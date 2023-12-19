using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Principal;
using System.IO;
using MBM.DataAccess;
using MBM.Entities;
using MBM.Library;

namespace MBM.BillingEngine
{
	public class Configurations
	{
		private DataFactory _dal;
		private Logger _logger;
		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
		public Configurations(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

		/// <summary>
		/// Returns all currency conversions available
		/// </summary>
		/// <param name="snapDate">maximum date for currency conversions</param>
		/// <returns>list of valid currencies</returns>
		public List<CurrencyConversion> Currencies(DateTime snapDate)
		{
			try
			{
				return _dal.Configurations.GetCurrencyConversions(snapDate);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -279459);
				return new List<CurrencyConversion>();
			}
		}

		/// <summary>
		/// Update settings
		/// </summary>
		/// <param name="config">new settings</param>
		public void UpdateConfig(ConfigurationEntity config)
		{
			_dal.Configurations.UpdateConfig(config);
		}


		/// <summary>
		/// Get a full list of configuration settings
		/// </summary>
		public List<ConfigurationEntity> GetConfig()
		{
			return _dal.Configurations.GetConfigurationList();
		}
	}
}
