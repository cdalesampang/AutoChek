using AutoChek.Data;
using AutoChek.Helpers;
using AutoChek.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace AutoChek.Controllers
{
    public class HomeController : Controller
    {

      
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {

            using (var db = new AutoCheckDbContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()));
                if (user != null)
                {
                    //Verify Password
                    if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    {
                        //Success
                        //create cookie
                        string userCookie = $"{user.Id},{user.FirstName},{user.LastName}";

                        Helper.EncryptUserCookie(userCookie, Helper.adminCookie);

                       return RedirectToAction("Index", "Manage");
                    }
                    TempData["msg"] = "<div class='alert alert-danger'>Incorrect Password!</div>";
                    return RedirectToAction("Index", "Home");
                }
                TempData["msg"] = "<div class='alert alert-danger'>User is not registered!</div>";
            }

            return RedirectToAction("Index", "Home");

        }


    }
}