using RealEstateProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Data.Entity;
using System.Windows.Forms;


namespace RealEstateProject.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string nickname, string password)
        {
            // Veritabanından kullanıcıyı kontrol et
            var user = c.Admins.FirstOrDefault(a => a.nickanme == nickname && a.password == password);

            if (user != null)
            {
                // Kullanıcı doğrulandı
                FormsAuthentication.SetAuthCookie(user.nickanme, false);
                Session["nickanme"] = user.nickanme;
                TempData["SuccessMessage"] = "Login successful. Welcome!";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // Geçersiz giriş
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Admin a)
        {
            c.Admins.Add(a);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","MainPage");
        }
    }
}