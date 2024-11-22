using RealEstateProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace RealEstateProject.Controllers
{
    public class ProjectController : Controller
    {
        Context c=new Context();
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
    }
}