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
        public PartialViewResult Contact()
        {
            var value=c.Contacts.ToList();
            return PartialView(value);
        }
        public PartialViewResult Office() 
        {
            var value=c.Offices.ToList();
            return PartialView(value);
        }
        public PartialViewResult Listing(string buildtype)
        {
            var buildsQuery = c.Builds
                .Include(c => c.buildType)
                .Include(c => c.Images)
                .AsQueryable();
            var builds=buildsQuery.ToList();

            var buildViewModels = builds.Select(build => new BuildViewModel
            {
                Build=build,
                BuildTypeName=build.buildType.buildType,
                Images=build.Images.ToList(),
            }).ToList();
            return PartialView(buildViewModels);
        }
    }
}