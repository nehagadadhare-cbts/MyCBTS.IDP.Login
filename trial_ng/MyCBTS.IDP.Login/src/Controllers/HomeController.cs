using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCBTS.IDP.Login.Configuration;
using MyCBTS.IDP.Login.Mapping;
using MyCBTS.IDP.Login.Models;
using System.Diagnostics;

namespace MyCBTS.IDP.Login.Controllers
{
    public class HomeController : Controller
    {

        [Route("~/error")]
        [Route("/home/error")]
        public IActionResult Error([FromQuery]string errorId)
        {
            return View();
        }
    }
}