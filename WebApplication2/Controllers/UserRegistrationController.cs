using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UserRegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegistrationViewModel registerModel)
        {
            if (string.IsNullOrEmpty(registerModel.FirstName))
            {
                ModelState.AddModelError("FirstName", "Please enter your first name");
            }
            if (!string.IsNullOrEmpty(registerModel.TelNo))
            {
                Regex telNoRegex = new Regex(@"^9\d{9}$");
                if (!telNoRegex.IsMatch(registerModel.TelNo))
                    ModelState.AddModelError("TelNo", "Ju lutem shenojeni vleren e Telephone Number");
            }
            if (ModelState.IsValid)
            {
                return View("Sucess"); //Returns user to success page  
            }
            else
            {
                return View(); //Returns user to the same page back again  
            }
        }
    }
}
