using RealEstateProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace RealEstateProject.Controllers
{
    public class MainPageController : Controller
    {
        Context c = new Context();
        // GET: MainPage
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Project()
        {
            var value = c.Projects.ToList();
            return PartialView(value);
        }
    }
}