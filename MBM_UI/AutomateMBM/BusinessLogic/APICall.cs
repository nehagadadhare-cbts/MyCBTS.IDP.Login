using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutomateMBM.BusinessLogic
{
    public class APICall
    {
        public static void ApiCheck()
        { 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55849/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Values/GetAllEmployees");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    //var readTask = result.Content.ReadAsAsync<();
                    //readTask.Wait();

                    //var students = readTask.Result;

                    //foreach (var student in students)
                    //{
                    //    Console.WriteLine(student.Name);
                    //}
                }
            }
            Console.ReadLine();
        }        
        
    }
}
