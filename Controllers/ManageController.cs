using AutoChek.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoChek.Controllers
{


    [AdminSecurity]
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShopInformation()
        {
            return View();
        }

    }
}