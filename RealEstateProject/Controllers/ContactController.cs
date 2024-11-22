using RealEstateProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace RealEstateProject.Controllers
{
    public class ContactController : Controller
    {
        Context c=new Context();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Office()
        {
            var value=c.Offices.ToList();
            return PartialView(value);
        }
    }
}