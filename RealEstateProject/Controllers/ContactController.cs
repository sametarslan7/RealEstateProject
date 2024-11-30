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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Message m)
        {
            m.messageDetail = "not read";
            c.Messages.Add(m);
            c.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public PartialViewResult Office()
        {
            var value=c.Offices.ToList();
            return PartialView(value);
        }
    }
}