using AutoChek.Data;
using AutoChek.Helpers;
using AutoChek.Models;
using AutoChek.ViewModel;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoChek.Controllers
{


    [AdminSecurity]
    public class ManageController : Controller
    {
        private int AdminId = Helper.GetAdminId();
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShopInformation()
        {
            using (var db = new AutoCheckDbContext())
            {
                ShopVM model = new ShopVM();

                model.Shop = db.Shops.Include(x => x.Province).Include(x => x.City).Include(x => x.Barangay).FirstOrDefault();

                if (model.Shop == null)
                {
                    TempData["msg"] = "<div class='alert alert-info'>Please update your shop information!</div>";
                    model.Shop = new Shop();
                }

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShopInfo(string shop, string contactno, string email, int province, int city, int brgy, string street, string unitno, string postal)
        {
            bool complete = true;
            string error = String.Empty;
            if (String.IsNullOrEmpty(shop))
            {
                complete = false;
                error = "Shop";
            }
            if (String.IsNullOrEmpty(contactno))
            {
                complete = false;
                error = "Contact Number";
            }
            if (String.IsNullOrEmpty(email))
            {
                complete = false;
                error = "Email";
            }
            if (province < 1 || city < 1 || brgy < 1 )
            {
                complete = false;
                error = "Complete Address";
            }

            if (complete)
            {
                using (var db = new AutoCheckDbContext())
                {
                    Shop model = new Shop
                    {
                        Name = shop,
                        ContactNo = contactno,
                        Email = email,
                        ProvinceId = province,
                        CityId = city,
                        BrgyId = brgy,
                        Street = street,
                        UnitNo = unitno,
                        ZipCode = postal,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        UpdatedBy= AdminId,
                        CreatedBy = AdminId

                    };
                    db.Shops.Add(model);
                    db.SaveChanges();
                    TempData["msg"] = $"<div class='alert alert-success'>Changes Saved!</div>";
                }

                return RedirectToAction("ShopInformation");

            }
            TempData["msg"] = $"<div class='alert alert-danger'>{error} is required</div>";


            return RedirectToAction("ShopInformation");

        }

        public ActionResult CarMakers()
        {
            using (var db = new AutoCheckDbContext())
            {
                CarMakerVM result = new CarMakerVM();
                result.Makers = db.Brands.ToList();
                result.Models = db.Models.Include(x => x.Brand).ToList();
                return View(result);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarMaker(int makerId,string name)
        {
            using (var db = new AutoCheckDbContext())
            {
                if (makerId < 1)
                {
                    var result = db.Brands.FirstOrDefault(b => b.Name == name);
                    if (result != null)
                    {
                        TempData["msg"] = "<div class='alert alert-danger'>Car Maker Already Exist!</div>";
                        return RedirectToAction("CarMakers");
                    }

                    Brand brand = new Brand
                    {
                        Name = name,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        CreatedBy = AdminId,
                        UpdatedBy = AdminId,
                    };
                    db.Brands.Add(brand);
                    db.SaveChanges();
                    TempData["msg"] = "<div class='alert alert-success'>Car Maker Added!</div>";
                    return RedirectToAction("CarMakers");
                }
                else
                {
                    var result = db.Brands.FirstOrDefault(b => b.Id == makerId);
                    if (result != null)
                    {
                        result.Name = name;
                        result.DateUpdated = DateTime.Now;
                        result.UpdatedBy = AdminId;
                        db.SaveChanges();
                        TempData["msg"] = "<div class='alert alert-success'>Car Maker Updated!</div>";
                        return RedirectToAction("CarMakers");
                    }
                }
                return RedirectToAction("CarMakers");
            }
        }


        [HttpPost]
        public ActionResult DeleteMaker(int id)
        {
            using (var db = new AutoCheckDbContext())
            {
                var result = db.Brands.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    db.Brands.Remove(result);
                    db.SaveChanges();
                   
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult DeleteModel(int id)
        {
            using (var db = new AutoCheckDbContext())
            {
                var result = db.Models.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    db.Models.Remove(result);
                    db.SaveChanges();

                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCarModel(int id, int makerId, string name)
        {
            using (var db = new AutoCheckDbContext())
            {
                if (id > 0)
                {
                    //update it
                    var result = db.Models.FirstOrDefault(x => x.Id == id);
                    if (result != null)
                    {
                        result.BrandId = makerId;
                        result.Name = name;
                        result.DateUpdated = DateTime.Now;
                        result.UpdatedBy = AdminId;
                        db.SaveChanges();
                        TempData["msg"] = "<div class='alert alert-success'>Car Model Updated!</div>";
                        return RedirectToAction("CarMakers");
                    }
                }
                else
                {
                    Model model = new Model
                    {
                        Name = name,
                        BrandId = makerId,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        CreatedBy = AdminId,
                        UpdatedBy = AdminId,
                    };
                    db.Models.Add(model);
                    db.SaveChanges();
                   
                }
                TempData["msg"] = "<div class='alert alert-success'>Car Model Saved!</div>";
                return RedirectToAction("CarMakers");

            }
        }

    }
}