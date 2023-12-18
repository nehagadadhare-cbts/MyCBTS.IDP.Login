using AutoMapper;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.Logger;
using MyCBTS.IDP.Login.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static MyCBTS.IDP.Login.Models.EnumList;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.Helpers
{
    public class ApiClientHelper
    {
        //private readonly ApiClient.ICBTSServiceClient _client;
        private readonly ApiClient.IBillHistoryClient _billHistoryClient;
        private readonly IMapper _iMapper;
        private readonly ILogger<ApiClientHelper> _logger;
        private readonly ILoggerManager _loggerManager;

        public ApiClientHelper(ApiClient.IBillHistoryClient billHistoryClient,
                               IMapper iMapper,
                               ILogger<ApiClientHelper> logger,
                               ILoggerManager loggerManager)
        {
            _billHistoryClient = billHistoryClient;
            _iMapper = iMapper;
            _logger = logger;
            _loggerManager = loggerManager;
        }

        public async Task<List<BillSummary>> ValidateBillDetails(RegisterViewModel usermodel)
        {
            string requestPath = string.Empty;
            string accountNumber = usermodel.AccountNumber?.Trim();
            List<BillSummary> billdetails = new List<BillSummary>();
            List<BillSummary> validBill = new List<BillSummary>();
            BillingSystem billingSystem = (BillingSystem)Enum.Parse(typeof(BillingSystem), usermodel.BillingSystem.ToUpper());
            try
            {
                switch (billingSystem)
                {
                    case BillingSystem.CBAD:
                        var billsCBAD = await _billHistoryClient.GetCBADBillsAsync(accountNumber);
                        billdetails = _iMapper.Map<List<BillSummary>>(billsCBAD?.BillSummaries.ToList());
                        break;

                    case BillingSystem.CBTS:
                        var billsCBTS = await _billHistoryClient.GetCBTSBillsAsync(accountNumber);
                        billdetails = _iMapper.Map<List<BillSummary>>(billsCBTS?.BillSummaries.ToList());
                        break;

                    case BillingSystem.CBTSCA:
                        var billsCBTSCA = await _billHistoryClient.GetCBTSCABillsAsync(accountNumber);
                        billdetails = _iMapper.Map<List<BillSummary>>(billsCBTSCA?.BillSummaries.ToList());
                        break;

                    case BillingSystem.CBTSUK:
                        var billsCBTSUK = await _billHistoryClient.GetCBTSUKBillsAsync(accountNumber);
                        billdetails = _iMapper.Map<List<BillSummary>>(billsCBTSUK?.BillSummaries.ToList());
                        break;

                    case BillingSystem.CRIS:
                        var billsCRIS = await _billHistoryClient.GetCRISBillsAsync(accountNumber);
                        billdetails = _iMapper.Map<List<BillSummary>>(billsCRIS?.BillSummaries.ToList());
                        break;

                    case BillingSystem.CABS:
                        var billsCABS = await _billHistoryClient.GetCABSBillsAsync(accountNumber);
                        billdetails = _iMapper.Map<List<BillSummary>>(billsCABS?.BillSummaries.ToList());
                        break;
                    case BillingSystem.ASCENDON:
                        var billsCSG = await _billHistoryClient.GetAscendonBillsAsync(accountNumber);
                        billdetails = _iMapper.Map<List<BillSummary>>(billsCSG?.BillSummaries.ToList());
                        break;
                    default:
                        break;
                }

                if (billingSystem == BillingSystem.CBAD || billingSystem == BillingSystem.CRIS)
                {
                    //var bills = billdetails.Where(a => a?.InvoiceNumber.Trim() == usermodel.InvoiceNumber).OrderByDescending(a => a.BillDate).ToList();
                    var bills = billdetails.Where(a => a?.TotalAmountDue == usermodel.TotalAmountDue).OrderByDescending(a => a.BillDate).ToList();
                    validBill = bills.Where(bill => (bill.BillDate >= (DateTime.Now.AddMonths(-2))))?.Take(1).ToList();
                }
                else 
                {
                    var bills = billdetails.Where(a => a?.InvoiceNumber.Trim() == usermodel.InvoiceNumber && 
                                                     a?.TotalAmountDue == usermodel.TotalAmountDue).OrderByDescending(a => a.BillDate).ToList();
                    validBill = bills.Where(bill => (bill.BillDate >= (DateTime.Now.AddMonths(-2))))?.Take(1).ToList();
                }
                return validBill;
            }
            catch (ApiClient.ApiException ex)
            {
                var objUserLogger = new UserLogger();
                objUserLogger.UserName = usermodel.EmailAddress?.Trim().ToLower();
                objUserLogger.AccountNumber = usermodel.AccountNumber;
                string errorMessage = GetErrorMessage(ex);
                await _loggerManager.LogError(this._logger, null, errorMessage, objUserLogger).ConfigureAwait(false);
                return validBill;
            }
        }

        public string GetErrorMessage(ApiClient.ApiException exception)
        {
            string errorDescritption = string.Empty;
            errorDescritption = ((ApiClient.ApiException<ApiClient.SimpleError>)exception).Result?.ErrorDescription;
            if (!string.IsNullOrEmpty(errorDescritption))
            {
                return errorDescritption;
            }
            return "Something went wrong";
        }
    }
}