using InsanKaynaklarıProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InsanKaynaklarıProje.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        // GET: Security
        DbIKProjeEntities db = new DbIKProjeEntities();
        [HttpGet]
     
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(IK_Personel prsnl)
        {
            var lgn = db.IK_Personel.FirstOrDefault(x=>x.PersonelAd == prsnl.PersonelAd && x.PersonelSifre == prsnl.PersonelSifre);
            
            if (lgn != null) { 

                //var dpt = db.View_PersonelDepartman.FirstOrDefault(m => m.PersonelAd == prsnl.PersonelAd).RolIsim;
                
                Session["depID"] = db.View_PersonelDepartman.FirstOrDefault(m=>m.PersonelAd==prsnl.PersonelAd).DepartmanID;
                Session["dep"]= db.View_PersonelDepartman.FirstOrDefault(m => m.PersonelAd == prsnl.PersonelAd).DepartmanIsim;
                Session["img"] = db.View_PersonelDepartman.FirstOrDefault(m => m.PersonelAd == prsnl.PersonelAd).PersonelResim;
                Session["name"] = db.View_PersonelDepartman.FirstOrDefault(m => m.PersonelAd == prsnl.PersonelAd).PersonelAd;
                Session["srname"] =db.View_PersonelDepartman.FirstOrDefault(m=>m.PersonelAd == prsnl.PersonelAd).PersonelSoyad;
                Session["id"] = db.View_PersonelDepartman.FirstOrDefault(m=>m.PersonelAd == prsnl.PersonelAd).PersonelID;
                var dpt = db.View_PersonelDepartman.FirstOrDefault(x => x.PersonelAd == prsnl.PersonelAd).RolIsim;
                
                Session["dprtmn"] = dpt;

                FormsAuthentication.SetAuthCookie(lgn.PersonelAd, false);
                return RedirectToAction("Index","Home");


                
            }
            else
            {
                ViewBag.Mesaj = "gecersiz sıfre veya kullanıcı adı";
                return View();
            }
        }
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}