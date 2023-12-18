using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MyCBTS.IDP.Login.Models;

namespace MyCBTS.IDP.Login.Utility
{
    public class ReCaptchaValidation
    {
        public bool ValidateCaptch(string captchaResponse, string secretKey)
        {
            bool validated = false;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://www.google.com");
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage resp = client.GetAsync("https://www.google.com/recaptcha/api/siteverify?secret=" + secretKey + "&response=" + captchaResponse).Result;


                if (resp.IsSuccessStatusCode)
                {
                    var responseString = resp.Content.ReadAsStringAsync().Result.ToString();
                    var obj = JsonConvert.DeserializeObject<ReCaptcha>(responseString);
                    if (obj.success)
                    {
                        validated = true;
                    }
                }
            }
            return validated;
        }
    }
}
