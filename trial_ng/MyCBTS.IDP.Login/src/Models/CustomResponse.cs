using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Models
{
    public class CustomResponse
    {
        public CustomResponse()
        {
            this.Errors = new List<string>();
        }
        public IList<string> Errors { get; private set; }
        public void AddError(string error)
        {
            Errors.Add(error);
        }
        public string SuccessMessage { get; set; }
        public string BrandName { get; set; }
    }
}
