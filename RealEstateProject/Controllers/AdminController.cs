using RealEstateProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace RealEstateProject.Controllers
{
    public class AdminController : Controller
    {
        Context c=new Context();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        // ----- ADMIN -----
        public ActionResult AdminList()
        {
            var value = c.Admins.ToList();
            return View(value);
        }
        // ----- BUILDTYPE -----
        public ActionResult BuildTypeList()
        {
            var value = c.BuildTypes.ToList();
            return View(value);
        }
        // ----- OFFICE -----
        public ActionResult OfficeList()
        {
            var value = c.Offices.ToList();
            return View(value);
        }
        // ----- PROEJCT -----
        public ActionResult Project()
        {
            var value=c.Projects.ToList();
            return View(value);
        }
        // ----- CONTACT -----
        public ActionResult ContactList()
        {
            var value=c.Contacts.ToList();
            return View(value);
        }
        // ----- MESSAGE -----
        public ActionResult MessageList()
        {
            var value=c.Messages.ToList();
            return View(value);
        }
        // ----- BUILD -----
        public ActionResult BuildList(string type)
        {       
            var buildsQuery = c.Builds
                .Include(c => c.buildType)
                .Include(c => c.Images)
                .AsQueryable();
            var builds = buildsQuery.ToList();
            var buildViewModels=builds.Select(build=>new BuildViewModel
            {
                Build=build,
                BuildTypeName=build.buildType.buildType,
                Images=build.Images.ToList(),
            }).ToList();
            return View(buildViewModels);

        }
    }
}