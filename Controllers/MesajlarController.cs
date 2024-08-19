using InsanKaynaklarıProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace InsanKaynaklarıProje.Controllers
{
    public class MesajlarController : Controller
    {
         DbIKProjeEntities db = new DbIKProjeEntities();
        public ActionResult Index()
        {         
            var degerler = db.View_Mesajlar.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult MesajEkle()
        {          
            var list = new MesajlarViewModel()
            {
             mesaj = db.IK_Mesaj.ToList(),
             Departman =db.IK_Departman.ToList(),
             YeniMesaj = true,
            };           
            return View("MesajIslemleri",list);
        }
        [HttpPost]
        public ActionResult MesajKaydet(MesajlarViewModel p)
        {
            p.IK_Mesaj.MesajGonderenKisi = User.Identity.Name;
            var dprtm = db.IK_Personel.FirstOrDefault(x => x.PersonelAd == User.Identity.Name).PersonelDepartmanId;
            var dprtmn = db.IK_Departman.FirstOrDefault(x => x.DepartmanID == dprtm).DepartmanID;
            p.IK_Mesaj.MesajGonderenDepartman = dprtmn;
            if (p.IK_Mesaj.MesajID == 0) // Yeni bir mesaj ekle
            {
                db.IK_Mesaj.Add(p.IK_Mesaj);
            }
            else // Var olan mesajı güncelle
            {
                var mevcutMesaj = db.IK_Mesaj.FirstOrDefault(x => x.MesajID == p.IK_Mesaj.MesajID);
                if (mevcutMesaj != null)
                {
                    mevcutMesaj.MesajGonderenKisi = p.IK_Mesaj.MesajGonderenKisi;
                    mevcutMesaj.MesajGonderenDepartman = p.IK_Mesaj.MesajGonderenDepartman;
                    mevcutMesaj.MesajAlanKişi = p.IK_Mesaj.MesajAlanKişi;
                    mevcutMesaj.MesajAlanDepartman = p.IK_Mesaj.MesajAlanDepartman;
                    mevcutMesaj.Mesaj = p.IK_Mesaj.Mesaj;
                    // Gerekli diğer alanları da güncelleyebilirsiniz
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MesajGuncelle(int id)
        {
            var degerler = new MesajlarViewModel()
            {
              IK_Mesaj =db.IK_Mesaj.FirstOrDefault(x=>x.MesajID==id),
              YeniMesaj = false,   
            };
            var usr = db.IK_Mesaj.FirstOrDefault(x=>x.MesajID ==id).MesajGonderenKisi;
            var dpr = db.IK_Mesaj.FirstOrDefault(x=>x.MesajID ==id).MesajGonderenDepartman;
            degerler.IK_Mesaj.MesajGonderenKisi=db.IK_Mesaj.FirstOrDefault(x=>x.MesajID ==id).MesajAlanKişi;
            degerler.IK_Mesaj.MesajGonderenDepartman =db.IK_Mesaj.FirstOrDefault(x=>x.MesajID == id).MesajAlanDepartman;
            degerler.IK_Mesaj.MesajAlanKişi = usr;
            degerler.IK_Mesaj.MesajAlanDepartman = dpr;
            degerler.IK_Mesaj.Mesaj = ""; 
            return View("MesajIslemleri", degerler);
        }
        public ActionResult MesajSil(int id)
        {
            var dgr = db.IK_Mesaj.FirstOrDefault(x=>x.MesajID == id);
            db.IK_Mesaj.Remove(dgr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
   
}