using InsanKaynaklarıProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsanKaynaklarıProje.Controllers
{
    public class DuyurularController : Controller
    {
        // GET: Duyurular
        DbIKProjeEntities db = new DbIKProjeEntities();
        public ActionResult Index()
        {
            var bugun = DateTime.Today;
            var duyurular = db.IK_Duyuru.ToList();
            var silinecek = duyurular.Where(m => m.DuyuruSilinmeTarihi == bugun).ToList();
            if (silinecek.Any())
            {
                db.IK_Duyuru.RemoveRange(silinecek);
                db.SaveChanges();
            }
            var degerler = db.IK_Duyuru.ToList();
            return View(degerler);
        }
        [HttpPost]
        public ActionResult DuyuruKaydet(IK_Duyuru p, HttpPostedFileBase File)
        {
            if (p.DuyuruId == 0)
            {
                IK_Duyuru d = new IK_Duyuru();
                d.DuyuruMetin = p.DuyuruMetin;
                d.DuyuruSilinmeTarihi = p.DuyuruSilinmeTarihi;

                if (File != null)
                {
                    string path = Path.Combine("~/Content/image/duyurular/", File.FileName); // Klasör açıyoruz 
                    string imgpath = "/Content/image/duyurular/" + File.FileName;
                    File.SaveAs(Server.MapPath(path));
                    d.DuyuruResim = imgpath;
                }
                else
                {
                    d.DuyuruResim = "/Content/image/duyurular/img_man.jpg"; // Varsayılan resim
                }

                db.IK_Duyuru.Add(d);
            }
            else
            {
                var dyr = db.IK_Duyuru.FirstOrDefault(x => x.DuyuruId == p.DuyuruId);
                if (dyr != null)
                {
                    if (File != null)
                    {
                        string path = Path.Combine("~/Content/image/duyurular/", File.FileName);
                        string imgpath = "/Content/image/duyurular/" + File.FileName;
                        File.SaveAs(Server.MapPath(path));
                        dyr.DuyuruResim = imgpath;
                    }

                    dyr.DuyuruMetin = p.DuyuruMetin;
                    dyr.DuyuruSilinmeTarihi = p.DuyuruSilinmeTarihi;
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    
    [HttpGet]
        public ActionResult DuyuruEkle() {

           
            
         
           
        return View ("DuyuruIslemleri");
        }
        public ActionResult DuyuruGuncelle(int id) {

           

           var p = db.IK_Duyuru.FirstOrDefault(x=>x.DuyuruId==id);
            return View("DuyuruIslemleri", p);
        }
        public ActionResult DuyuruSil(int id) { 
        
            var degerler = db.IK_Duyuru.FirstOrDefault(x=>x.DuyuruId == id);

            db.IK_Duyuru.Remove(degerler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
    
}