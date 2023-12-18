using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MyCBTS.IDP.Login.Configuration;
using MyCBTS.IDP.Login.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Utility
{
    public class CommonUtility : ICommonUtility
    {

        private readonly AppConfiguration _appConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommonUtility(IOptions<AppConfiguration> appConfiguration,
                             IHttpContextAccessor httpContextAccessor)
        {
            _appConfiguration = appConfiguration.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetBrandName()
        {
            string BrandName;
            var sessionBrand = _httpContextAccessor.HttpContext.Session.GetString(APIConstants.SessionClient);
            BrandName = string.IsNullOrEmpty(sessionBrand) ? _appConfiguration?.DefaultBrand?.ToString() : sessionBrand;
            return BrandName;
        }
    }
}
