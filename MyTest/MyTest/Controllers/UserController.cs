using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Data;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using MyTest.Models;
using Microsoft.Data.SqlClient;

namespace MyTest.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDBContent dBContent;
        public UserController(AppDBContent content)
        {
            dBContent = content;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Verify() 
        {
            return View();
        }
        [HttpGet]
        public IActionResult SuccessVerify() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(string name, string email)
        {
            #region Generate Code
            Random random = new Random();
            int randomfirstNumber = random.Next(0, 9);
            int randomsecondNumber = random.Next(0, 9);
            int randomthirdNumber = random.Next(0, 9);
            int randomfourthNumber = random.Next(0, 9);
            string randomCode = "" + randomfirstNumber + randomsecondNumber + randomthirdNumber + randomfourthNumber;
            int generatedCode = int.Parse(randomCode);
            ViewBag.RandomCode = generatedCode;
            #endregion

            SendEmail(email, generatedCode);

            var emailVerify = new Message
            {
                Email = email,
                Code = generatedCode,
                Date = DateTime.Now
            };
            dBContent.Messages.Add(emailVerify);
            dBContent.SaveChanges();
            ViewBag.Email = email;
            return View("Verify");
        }
        private void SendEmail(string email, int code)
        {
            var emailMessage = new Message
            {
                Email = email,
                Code = code,
                Date = DateTime.Now
            };
            dBContent.Messages.Add(emailMessage);
            dBContent.SaveChanges();
        }
        public IActionResult Verify(int code, string email)
        {
            var verification = dBContent.Messages.FirstOrDefault(ev => ev.Email == email && ev.Code == code);

            if (verification != null)
            {
                return RedirectToAction("SuccesVerify", "User");
            }
            else
            {
                ViewBag.ErrorMessage = "Неверный код. Попробуйте снова.";
                ViewBag.Email = email;
                return RedirectToAction("Verify", "User");
            }
        }
    }
}
