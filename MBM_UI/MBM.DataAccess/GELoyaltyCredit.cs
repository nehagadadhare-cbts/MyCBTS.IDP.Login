using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.SqlClient;
using MBM.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace MBM.DataAccess
{
	public class GELoyaltyCredit
	{
		#region Setup

		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// This filter can be applied to reduce the type of entites returned
		/// </summary>
		public enum RECORDS_FILTER
		{
			ALL = 0,
			NO_GATEWAYS = 1,
			GATEWAYS_ONLY = 2,
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
		public GELoyaltyCredit(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}

		#endregion

		/// <summary>
		/// Retrieve LoyaltyCredit from the data store
		/// </summary>
		/// <param name="invoiceID">invoice to pull</param>
		/// <returns>list of entities returned</returns>
		public System.Data.DataTable GetLoyaltyCredit(int invoiceID)
		{
			System.Data.DataTable queryResults = null;

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<GE_LoyaltyCredit_GetByInvoiceResult> results = db.GE_LoyaltyCredit_GetByInvoice(invoiceID);

					queryResults = new System.Data.DataTable();

					queryResults.Columns.Add("Service Start Date", System.Type.GetType("System.DateTime"));
					queryResults.Columns.Add("SSO", System.Type.GetType("System.String"));
					queryResults.Columns.Add("Phone Number", System.Type.GetType("System.String"));
                    //queryResults.Columns.Add("Profile ID", System.Type.GetType("System.Int32"));//ERO 1037
					queryResults.Columns.Add("Loyalty Credit", System.Type.GetType("System.Double"));
					queryResults.Columns.Add("Currency ID", System.Type.GetType("System.Int32"));
					queryResults.Columns.Add("Manual Entry", System.Type.GetType("System.Boolean"));
					queryResults.Columns.Add("Notes", System.Type.GetType("System.String"));

					foreach (GE_LoyaltyCredit_GetByInvoiceResult r in results)
					{
						System.Data.DataRow nextRow = queryResults.NewRow();

						nextRow[0] = r.ServiceStartDate;
						nextRow[1] = r.SSO;
						nextRow[2] = r.PhoneNumber;
                        //nextRow[3] = r.ProfileID;//ERO 1037
						nextRow[3] = r.LoyaltyCredit;
						nextRow[4] = r.CurrencyID;
						nextRow[5] = r.ManualEntry;
						nextRow[6] = r.Notes;

						queryResults.Rows.Add(nextRow);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -854374);
				throw;
			}

			return queryResults;
		}
	}
}
