using RealEstateProject.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

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
        [HttpGet]
        public ActionResult NewAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAdmin(Admin a)
        {
            c.Admins.Add(a);
            c.SaveChanges();
            return RedirectToAction("AdminList");
        }
        public ActionResult GetAdmin(int id)
        {
            var adm=c.Admins.Find(id);
            return View("GetAdmin",adm);
        }
        public ActionResult UpdateAdmin(Admin ad)
        {
            var update_admin = c.Admins.Find(ad.Id);
            update_admin.name = ad.name;
            update_admin.surname = ad.surname;
            update_admin.email = ad.email;
            update_admin.nickanme = ad.nickanme;
            update_admin.password = ad.password;

            c.SaveChanges();
            return RedirectToAction("AdminList");
        }
        // ----- BUILDTYPE -----
        public ActionResult BuildTypeList()
        {
            var value = c.BuildTypes.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult NewBuildType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewBuildType(BuildType b)
        {
            c.BuildTypes.Add(b);
            c.SaveChanges();
            return RedirectToAction("BuildTypeList");
        }
        public ActionResult GetBuildType(int id)
        {
            var buildtype=c.BuildTypes.Find(id);
            return View("GetBuildType", buildtype);
        }
        public ActionResult UpdateBuildType(BuildType bt)
        {
            var update_buildtype = c.BuildTypes.Find(bt.buildTypeid);

            update_buildtype.buildType = bt.buildType;
            c.SaveChanges();
            return RedirectToAction("BuildTypeList");
        }
        // ----- OFFICE -----
        public ActionResult OfficeList()
        {
            var value = c.Offices.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult NewOffice()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewOffice(Office o) 
        {
            c.Offices.Add(o);
            c.SaveChanges();
            return RedirectToAction("OfficeList");
        }
        public ActionResult GetOffice(int id)
        {
            var ofc=c.Offices.Find(id);
            return View("GetOffice", ofc);
        }
        public ActionResult UpdateOffice(Office o)
        {
            var update_office = c.Offices.Find(o.officeId);

            update_office.officeCity = o.officeCity;
            update_office.officeFullAdress = o.officeFullAdress;
            c.SaveChanges();
            return RedirectToAction("OfficeList");
        }
        // ----- PROEJCT -----
        public ActionResult Project()
        {
            var value=c.Projects.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult NewProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewProject(Project p, IEnumerable<HttpPostedFileBase> Files)
        {
            using (var transaction = c.Database.BeginTransaction())
            {
                try
                {
                    // 1. Proje bilgilerini kaydet
                    c.Projects.Add(p);
                    c.SaveChanges();

                    // Yeni eklenen projenin ID'sini al
                    var projectId = p.projectId;

                    // 2. Fotoğrafları kaydetme işlemi
                    if (Files != null && Files.Any())
                    {
                        foreach (var file in Files)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                // Dosya adını al
                                var fileName = Path.GetFileName(file.FileName);
                                var filePath = Path.Combine(Server.MapPath("~/images/projects"), fileName);

                                // Fotoğrafı sunucuya kaydet
                                file.SaveAs(filePath);

                                // Proje için ana görsel URL'si kaydet
                                if (string.IsNullOrEmpty(p.projectImageUrl))
                                {
                                    p.projectImageUrl = "/images/projects/" + fileName;
                                }
                            }
                        }

                        // Değişiklikleri kaydet
                        c.Entry(p).State = System.Data.Entity.EntityState.Modified;
                        c.SaveChanges();
                    }

                    // Her şey başarılıysa işlemi onayla
                    transaction.Commit();

                    return RedirectToAction("Project"); // Projeler listesine yönlendirme
                }
                catch (Exception ex)
                {
                    // Hata durumunda işlemi geri al
                    transaction.Rollback();

                    // Hata mesajını kullanıcıya ilet
                    ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin. Hata: " + ex.Message);
                    return View(p);
                }
            }
        }


        // ----- CONTACT -----
        public ActionResult ContactList()
        {
            var value=c.Contacts.ToList();
            return View(value);
        }
        public ActionResult GetContact(int id)
        {
            var cnt = c.Contacts.Find(id);
            return View("GetContact", cnt);
        }
        public ActionResult UpdateContact(Contact cont)
        {
            var update_contact = c.Contacts.Find(cont.contactId);

            update_contact.emailAdress= cont.emailAdress;
            update_contact.phoneNumber= cont.phoneNumber;
            update_contact.faxNumber= cont.faxNumber;
            c.SaveChanges();
            return RedirectToAction("ContactList");
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
        // GET: Admin/NewBuild
        [HttpGet]
        public ActionResult NewBuild()
        {
            // BuildType listesini ViewBag ile dolduruyoruz
            ViewBag.BuildTypes = new SelectList(c.BuildTypes.ToList(), "buildTypeid", "buildType");
            return View();
        }

        // POST: Admin/NewBuild
        [HttpPost]
        public ActionResult NewBuild(Build build, IEnumerable<HttpPostedFileBase> Files)
        {
            if (!ModelState.IsValid)
            {
                // BuildType listesini yeniden ViewBag'e ekliyoruz
                ViewBag.BuildTypes = new SelectList(c.BuildTypes.ToList(), "buildTypeid", "buildType");
                return View(build);
            }

            // Build kaydı
            build.buildType = c.BuildTypes.Find(build.buildTypeId); // BuildType ilişkisini kuruyoruz
            c.Builds.Add(build);
            c.SaveChanges();

            // Resimlerin kaydedilmesi
            if (Files != null && Files.Any())
            {
                foreach (var file in Files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Resim kaydederken benzersiz bir isim oluşturalım
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName);
                        var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                        // Resmi sunucuya kaydet
                        var path = Path.Combine(Server.MapPath("~/Uploads"), uniqueFileName);
                        file.SaveAs(path);

                        // Resim tablosuna kayıt ekle
                        var image = new Image
                        {
                            BuildId = build.buildId, // İlişkilendirme
                            imageUrl = $"/Uploads/{uniqueFileName}" // Kaydedilen yol
                        };
                        c.Images.Add(image);
                    }
                }
                c.SaveChanges();
            }

            // Başarılı ekleme sonrası geri yönlendirme
            return RedirectToAction("BuildList", "Admin");
        }

        // ----- IMAGES ------
        public ActionResult ImageList()
        {
            var buildsQuery = c.Builds
                .Include(c => c.Images)
                .AsQueryable();
            var builds = buildsQuery.ToList();

            var buildViewModels = builds.Select(build => new BuildViewModel
            {
                Build = build,
                Images = build.Images.ToList(),
            }).ToList();

            return View(buildViewModels);
        }

    }
}