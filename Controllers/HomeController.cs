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


        public class Records
        {
            public List<RegionVM> Regions { get; set; }
        }
      
      
        public ActionResult Index()
        {
            StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/refbrgy.json"));

            string data = streamReader.ReadToEnd();
            var results = JsonConvert.DeserializeObject<dynamic>(data);

            var records = results["RECORDS"].ToString();
            List<BarangayVM> regions = JsonConvert.DeserializeObject<List<BarangayVM>>(records);

            using (var db = new AutoCheckDbContext())
            {
                foreach (var region in regions)
                {
                    var region1 = db.Municipalities.FirstOrDefault(x => x.Code == region.citymunCode);

                    Barangay data1 = new Barangay
                    {
                        Code = region.brgyCode,
                        Description = region.brgyDesc,
                        Id = region.Id,
                        MunicipalityId = region1.Id
                    };
                    db.Barangays.Add(data1);
                }
                db.SaveChanges();
            }

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