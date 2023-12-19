using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using MBM.Entities;

namespace MBM.DataAccess
{
    public class ConfigurationsDAL
	{
		#region Setup

		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
		public ConfigurationsDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}

		#endregion

		/// <summary>
		/// Get a full list of all active currencies
		/// </summary>
		/// <param name="snapDate">The date to use as the basis for active currencies. If null
		/// get all currency conversions.
		/// </param>
		/// <remarks>
		/// If a currency conversion happens on 1/1/2020 and a snapDate of 1/1/2012 then it will
		/// not be included in the list. Likewise if using the same snapdate and there are
		/// two entries for Canadian Dollars (CAD) of 1/1/2010 and 5/20/2010 then only the 5/20/2010
		/// entry will be returned.
		/// </remarks>
		/// <returns>list of currencies found</returns>
		public List<CurrencyConversion> GetCurrencyConversions(DateTime? snapDate)
		{
			List<CurrencyConversion> entities = new List<CurrencyConversion>();

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					if (snapDate != null)
					{
						ISingleResult<Currency_GetCurrentResult> results = db.Currency_GetCurrent(snapDate);

						foreach (Currency_GetCurrentResult r in results)
						{
							CurrencyConversion entity = new CurrencyConversion()
							{
								ID = r.ID,
								Description = r.Description,
								Code = r.CurrencyCode,
								Symbol = r.CurrencySymbol,
								ActiveDate = r.ActiveDate,
								ConversionRate = r.Conversion,
							};

							if (entity != null)
							{
								entities.Add(entity);
							}
						}
					}
					else
					{
						ISingleResult<Currency_GetAllResult> results = db.Currency_GetAll();

						foreach (Currency_GetAllResult r in results)
						{
							CurrencyConversion entity = new CurrencyConversion()
							{
								ID = r.ID,
								Description = r.Description,
								Code = r.CurrencyCode,
								Symbol = r.CurrencySymbol,
								ActiveDate = r.ActiveDate,
								ConversionRate = r.Conversion,
							};

							if (entity != null)
							{
								entities.Add(entity);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -926850);
				throw;
			}

			return entities;
		}


		/// <summary>
		/// Get a full list of all configuration settings
		/// </summary>
		/// <param name="entities"></param>
		/// <returns>list of configuration entities found</returns>
		public List<ConfigurationEntity> GetConfigurationList()
		{
			List<ConfigurationEntity> entities = new List<ConfigurationEntity>();

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<Configuration_GetResult> results = db.Configuration_Get();

					foreach (Configuration_GetResult r in results)
					{
						ConfigurationEntity entity = new ConfigurationEntity()
						{
							ConfigurationName = r.ConfigurationName,
							ConfigurationValue = r.ConfigurationValue,
							LastUpdatedBy = r.LastUpdatedBy,
							LastUpdatedDate = r.LastUpdatedDate,
						};

						if (entity != null)
						{
							entities.Add(entity);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -516594);
				throw;
			}

			return entities;
		}

		/// <summary>
		/// Retrieve a list of all Canadian Taxes
		/// </summary>
		/// <param name="snapDate">maximum date to check for tax entries - ignore any future tax entries</param>
		/// <returns>entities found</returns>
		public CanadianTaxCollection GetCanadianTaxes(DateTime? snapDate)
		{
			CanadianTaxCollection entities = new CanadianTaxCollection();

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<CanadianTaxes_GetByDateResult> results = db.CanadianTaxes_GetByDate(snapDate);

					foreach (CanadianTaxes_GetByDateResult r in results)
					{
						CanadianTax entity = new CanadianTax()
						{
							Province = r.ProvinceCode,
							GST = r.GST,
							QST = r.QST,
							PST = r.PST,
							HST_Recoverable = r.HST_R,
							HST_NonRecoverable = r.HST_NR
						};

						if (entity != null)
						{
							entities.Add(entity);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -576620);
				throw;
			}

			return entities;
		}

		/// <summary>
		/// Update basic configuration information
		/// </summary>
		/// <param name="config">configuration entity</param>
        public int UpdateConfig(ConfigurationEntity config)
        {
			int status = -719667;

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					db.Configuration_Update(
						config.ConfigurationName,
						config.ConfigurationValue,
						config.LastUpdatedDate,
						config.LastUpdatedBy
						);

					status = 1;
				}
			}
			catch (Exception ex)
			{
				status = -360065;
				_logger.Exception(ex, status);
			}
			
			return status;
        }

    }
}
