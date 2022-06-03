using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EmployeeContext db ;

        private MySqlDatabase MySqlDatabase { get; set; }
      
        public HomeController(ILogger<HomeController> logger, MySqlDatabase mySqlDatabase)//, EmployeeContext employee)
        {
            _logger = logger;
            //db = employee;
            this.MySqlDatabase = mySqlDatabase;

        }

        public async Task<IActionResult> IndexAsync(string customerId)
        {
            string userName = Request.Cookies["UserName"];//cookies
            if (customerId==null || customerId.Trim()=="")
            {
               // var tt = db.listcustomers.ToList();
                customerId = "-1";
            }
            bool isInt = true;// int.TryParse(customerId, out int cid);
            if (!isInt)
            {
                List<customer> obj = new List<customer>();
                return View(obj);
            }
            else
            {
                var rrez = await this.GetTasks(customerId);

                return View(rrez);

            }

          
        }
        private async Task<List<customer>> GetTasks(string customerId)
        {
            var ret = new List<customer>();

             MySql.Data.MySqlClient.MySqlConnection Connection = new MySqlConnection("server=localhost;user id=root;password=123456;database=emplyeedb");
                Connection.Open();
                var cmd = Connection.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT customerId, username,password FROM customers WHERE customerId  ="+ customerId;

            using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    var t = new customer
                    {
                        customerId = int.Parse(reader["customerId"].ToString()),
                        username =  reader["username"].ToString() ,
                        password = reader["password"].ToString()
                    };

                    ret.Add(t);
                }
            return ret;
        }
        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";

        public IActionResult Privacy()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, "Programmer");
                HttpContext.Session.SetInt32(SessionKeyAge, 33); 
            }
            var name = HttpContext.Session.GetString(SessionKeyName);
            var age = HttpContext.Session.GetInt32(SessionKeyAge).ToString();

          ViewBag.name="Session Name: "+name;
          ViewBag.age = "Session Age: "+ age;
            return View();
        }
        [HttpGet]
        [Route("get-sessionId")]
        public IActionResult GetSessionId()
        {
            return Content(HttpContext.Session.Id);
        }

        [HttpGet]
        [Route("save-to-session")]
        public IActionResult SaveToSession(string name, string age)
        {
            HttpContext.Session.SetString(SessionKeyName, name);
            HttpContext.Session.SetString(SessionKeyAge, age);

            //CookieOptions option = new CookieOptions();
            //option.Expires = DateTime.Now.AddMinutes(5);
            //Response.Cookies.Append("UserName", name, option);
            //Response.Cookies.Append("Age", age , option);

            return Content($"{name} and {age} save to session");
        }
        [HttpGet]
        [Route("fetch-from-session")]
        public IActionResult FetchFromSession()
        {
            string name = HttpContext.Session.GetString(SessionKeyName);
            string age = HttpContext.Session.GetString(SessionKeyAge);
            return Content($"{name} and {age} save to session");
        }

        //  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
