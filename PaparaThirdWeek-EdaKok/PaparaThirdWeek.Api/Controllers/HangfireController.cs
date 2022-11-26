using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController:ControllerBase
    {
        private readonly IFakeDataUserService _userService;
        private readonly IConfiguration _config;
        public HangfireController(IFakeDataUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [Route("GetAllUsers")]
        [HttpGet]
        public ActionResult GetUser()
        {
            var result = _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("RetrieveDataFromAPI")]
        public void RetrieveDataFromAPI()
        {
            //retrieve data from external api 

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/posts"); // retrieve posts 

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            List<FakeDataUserDTO> items = (List<FakeDataUserDTO>)JsonConvert.DeserializeObject(jsonString, typeof(List<FakeDataUserDTO>));
            _userService.Add(items[0]); // get the first user from list 
            Console.WriteLine(items);
            Console.WriteLine($"Data has been retrieved from external API");
        }

        [HttpGet]
        [Route("RetrieveData")]
        public IActionResult RetrieveData()
        {
            RecurringJob.AddOrUpdate(() => RetrieveDataFromAPI(), _config.GetConnectionString("CronTime")); // recurring job fires the method
            return Ok($"Recurring Job Scheduled. Data will be retrieved every 5 minutes from external API!");
        }
    }
}

