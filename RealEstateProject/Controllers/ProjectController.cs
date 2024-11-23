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
        public ActionResult GetProject(int id)
        {
            var project = c.Projects.FirstOrDefault(p => p.projectId == id);
            if (project == null)
            {
                return HttpNotFound(); // Eğer proje bulunamazsa hata döndür
            }
            return View("GetProject", project);
        }

        public PartialViewResult Project()
        {
            var value=c.Projects.ToList();
            return PartialView(value);
        }
    }
}