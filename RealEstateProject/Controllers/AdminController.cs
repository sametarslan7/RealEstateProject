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