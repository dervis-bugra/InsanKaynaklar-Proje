using InsanKaynaklarıProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace InsanKaynaklarıProje.Controllers
{
    public class HomeController : Controller
    {
     
        
        DbIKProjeEntities db = new DbIKProjeEntities();
      
        [AllowAnonymous]
        public ActionResult Index()
        {
            

            PersonellerDepartmanlarViewModel model = new PersonellerDepartmanlarViewModel() {

            

                IK_Departmen = db.IK_Departman.ToList(),
                ıK_Personels = db.IK_Personel.ToList(),
                prs_dprtmn_rl=db.Personel_departman_rol_model_view.ToList(),
                IK_Duyurus = db.IK_Duyuru.ToList(), 
    
             
            };

            return View(model);
         
        }

     
        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
           

            return View();
        }
     
          [AllowAnonymous]
        public ActionResult adresler()
        {
            return View();
        }
        public ActionResult Duyurular()
        {
            DuyurularResimlerViewModel model = new DuyurularResimlerViewModel()
            {
         
               
                IK_Duyurus = db.IK_Duyuru.ToList(),
                
            };
            return RedirectToAction("Index", "Duyurular",model);

        }
        [HttpGet]
        public JsonResult GetDuyurular()
        {
            var duyurular = db.IK_Duyuru.Select(d => new
            {
                d.DuyuruId,
                d.DuyuruMetin
            }).ToList();

            return Json(duyurular, JsonRequestBehavior.AllowGet);
        }
    }

}

