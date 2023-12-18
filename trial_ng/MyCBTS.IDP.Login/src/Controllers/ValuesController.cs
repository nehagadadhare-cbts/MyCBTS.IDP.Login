//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using MyCBTS.IDP.Login.Logger;
//using MyCBTS.IDP.Login.Models;
//using System;
//using System.Threading.Tasks;
//using APIClient = MyCBTS.Api.Client;
//using System.Net.Http;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
//using MyCBTS.IDP.Login.Extensions;
//using System.Collections.Generic;
//using System.Linq;

//namespace MyCBTS.IDP.Login.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : Controller
//    {
//        private ILogger<ValuesController> _logger;
//        private readonly MyCBTS.IDP.Login.Logger.ILoggerManager _loggerManager;
//       // private readonly APIClient.ICBTSServiceClient _client;

//        public ValuesController(ILogger<ValuesController> logger,
//                                ILoggerManager loggerManager,
//                               // APIClient.ICBTSServiceClient client)
//        {
//            _logger = logger;
//            _loggerManager = loggerManager;
//            _client = client;
//        }

//        /// <summary>
//        /// Get Data.
//        /// </summary>
//        // GET api/values
//        [HttpGet]
//        public async Task<IActionResult> Get()

//        {
//            try
//            {
//                var accountList = new List<string>()
//                {
//                    new string("0001516"),
//                    new string("0001973"),
//                    new string("0002303"),
//                    new string("0008728"),
//                    new string("0009532"),
//                    new string("0012108"),
//                    new string("0015677"),
//                };

//                List<Task<APIClient.CbadBillHistoryResponse>> billHistortTasks = new List<Task<APIClient.CbadBillHistoryResponse>>();
//                foreach (var account in accountList)
//                {
//                    //billHistortTasks.Add(_client.GetCBADBillsAsync(account));
//                }

//                var billHistortTasksResult = await Task.WhenAll(billHistortTasks);
//                var billhistory = new List<APIClient.CbadBillSummary>();

//                foreach (var task in billHistortTasksResult) { billhistory.AddRange(task?.BillSummaries); }
//                billhistory = billhistory?.OrderByDescending(x => x?.BillDate)?.Take(4)?.ToList();

//                List<Task<APIClient.CbadBillResponse>> billDetailTasks = new List<Task<APIClient.CbadBillResponse>>();

//                foreach (var billdata in billhistory)
//                {
//                    APIClient.Bill bill = new APIClient.Bill()
//                    {
//                        AccountNumber = billdata.AccountNumber,
//                        BillDate = billdata.BillDate
//                    };
//                    billDetailTasks.Add(_client.GetCBADBillDetailsAsync(bill));
//                }

//                var billhistoryTaskResult = await Task.WhenAll(billDetailTasks);
//                var billsummary = new List<APIClient.CbadBill>();

//                foreach (var task in billhistoryTaskResult) { billsummary.Add(task?.CbadBill); }

//                return Ok(billsummary);
//            }
//            catch (APIClient.ApiException ex)
//            {
//                var code = ex.StatusCode;
//                var message = JsonConvert.DeserializeObject<ErrorDescription[]>(ex.Response);
//            }

//            return Ok();
//        }

//        /// <summary>
//        /// Get Data by id.
//        /// </summary>

//        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult<string> Get(int id)
//        {
//            return "value";
//        }

//        /// <summary>
//        /// Post Data.
//        /// </summary>
//        // POST api/values
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}