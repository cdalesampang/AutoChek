using AutoChek.Data;
using AutoChek.Helpers;
using AutoChek.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoChek.Controllers
{
    [AdminSecurity]
    public class DropDownController : Controller
    {
        // GET: DropDown
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetProvinces(string keyword)
        {

            using (var db = new AutoCheckDbContext())
            {
                DropdownVM[] results = keyword == null ?
                     db.Provinces.OrderBy(x => x.Description).Select(x => new DropdownVM { id = x.Id, text = x.Description }).ToArray():
                    db.Provinces.Where(x => x.Description.Contains(keyword)).OrderBy(x => x.Description).Select(x => new DropdownVM {id = x.Id, text = x.Description }).ToArray();

                return Json(new { items = results }, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpGet]
        public ActionResult GetMunicipalities(string keyword, int provinceId)
        {

            using (var db = new AutoCheckDbContext())
            {
                DropdownVM[] results = keyword == null ?
                    db.Municipalities.Where(x => x.ProvinceId == provinceId).OrderBy(x => x.Description).Select(x => new DropdownVM { id = x.Id, text = x.Description }).ToArray() :
                db.Municipalities.Where(x => x.Description.Contains(keyword) && x.ProvinceId == provinceId).OrderBy(x => x.Description).Select(x => new DropdownVM { id = x.Id, text = x.Description }).ToArray();

                return Json(new { items = results }, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpGet]
        public ActionResult GetBrgys(string keyword, int cityId)
        {

            using (var db = new AutoCheckDbContext())
            {
                DropdownVM[] results = keyword == null ?
                    db.Barangays.Where(x => x.MunicipalityId == cityId).OrderBy(x => x.Description).Select(x => new DropdownVM { id = x.Id, text = x.Description }).ToArray() :
                db.Barangays.Where(x => x.Description.Contains(keyword) && x.MunicipalityId == cityId).OrderBy(x => x.Description).Select(x => new DropdownVM { id = x.Id, text = x.Description }).ToArray();

                return Json(new { items = results }, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public ActionResult GetCarMakers(string keyword)
        {

            using (var db = new AutoCheckDbContext())
            {
                DropdownVM[] results = keyword == null ?
                    db.Brands.Take(5).OrderBy(x => x.Name).Select(x => new DropdownVM { id = x.Id, text = x.Name }).ToArray() :
                db.Brands.Where(x => x.Name.Contains(keyword) ).OrderBy(x => x.Name).Select(x => new DropdownVM { id = x.Id, text = x.Name }).ToArray();

                return Json(new { items = results }, JsonRequestBehavior.AllowGet);

            }

        }

    }
}