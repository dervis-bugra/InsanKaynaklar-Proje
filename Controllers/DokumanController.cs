using InsanKaynaklarıProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WebGrease.Activities;

namespace InsanKaynaklarıProje.Controllers
{
    public class DokumanController : Controller
    {
        // GET: Dokuman
        DbIKProjeEntities db = new DbIKProjeEntities();
        public ActionResult Index()
        {
            var degerler = db.View_DOKUMANLARDEPARTMANLAR.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult DokumanEkle() {

            DokumanlarDepartmanlarViewModel dkmn = new DokumanlarDepartmanlarViewModel()
            {
                Dokumanlar = new Dokumanlar(),
                Dkmnlr = db.Dokumanlar.ToList(),
                IK_Dprtmn = db.IK_Departman.ToList(),



            };

            return View("DokumanIslemleri",dkmn); }
        public ActionResult DokumanSil(int id)
        {
            var silinen = db.Dokumanlar.FirstOrDefault(x => x.DokumanID == id);
            db. Dokumanlar.Remove(silinen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public ActionResult DokumanGuncelle(int id)
        {
            var p  = new DokumanlarDepartmanlarViewModel()
            {
                Dokumanlar = db.Dokumanlar.FirstOrDefault(x=>x.DokumanID == id),
                Dkmnlr = db.Dokumanlar.ToList(),
                IK_Dprtmn = db.IK_Departman.ToList(),
            };
            return View("DokumanIslemleri",p);
        }
        [HttpPost]
        public ActionResult DokumanKaydet(DokumanlarDepartmanlarViewModel p, HttpPostedFileBase File)
        {
           
            if (p. Dokumanlar.DokumanID == 0)

            { Dokumanlar d = new Dokumanlar();
                
              

                d.DokumanAcıklama = p.Dokumanlar.DokumanAcıklama;
                d.DokumanGelenDepartman = p.Dokumanlar.DokumanGelenDepartman;
               
                var deger = User.Identity.Name;
                var degerler = db.IK_Personel.FirstOrDefault(x => x.PersonelAd == deger).PersonelDepartmanId;
                var departman = db.IK_Departman.FirstOrDefault(x => x.DepartmanID == degerler).DepartmanIsim;
                d.DokumanGonderenDepartman = departman;
                d.DokumanGonderenKisi = deger;
               db.Dokumanlar.Add(d);
                db.SaveChanges();


                string klasor = Server.MapPath("~/Content/Dokumanlar/" + p.Dokumanlar.DokumanID);

                if (!Directory.Exists(klasor))
                {
                    Directory.CreateDirectory(klasor);
                }

                string path = Path.Combine(klasor, File.FileName); // `Path.Combine` ile tam yol oluşturuluyor
                d.Dokuman = path;
                File.SaveAs(path); 

              
                

                
                db.SaveChanges();
            }
            else
            {

                var degerler = db.Dokumanlar.FirstOrDefault(x => x.DokumanID == p.Dokumanlar.DokumanID);

              
               

                string klasor = Server.MapPath("~/Content/Dokumanlar/" + p.Dokumanlar.DokumanID);

                if (!Directory.Exists(klasor))
                {
                    Directory.CreateDirectory(klasor);
                }

                string path = Path.Combine(klasor, File.FileName);
                degerler.Dokuman = path;
                File.SaveAs(path); 


                db.SaveChanges();
            }

            
            return RedirectToAction("Index");
        }
        public ActionResult Indir(int id)
        {
            var dokumanlar = db.Dokumanlar.FirstOrDefault(x=>x.DokumanID == id);
            string filePath = dokumanlar.Dokuman;
            string fileName  = Path.GetFileName(filePath);
            byte[] filebytes = System.IO.File.ReadAllBytes(filePath);
            return File(filebytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }    
    }
}