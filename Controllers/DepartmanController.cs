using InsanKaynaklarıProje.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using PagedList.Mvc;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.IO;

namespace InsanKaynaklarıProje.Controllers
{
    [Authorize(Roles = "2,3")]
    public class DepartmanController : Controller
    {
        DbIKProjeEntities db = new DbIKProjeEntities();


        public ActionResult Index()
        {



            var dprtmn = db.IK_Departman.ToList();
         

            return View(dprtmn);


        }

        [HttpGet]
        public ActionResult DprtmnEkle()
        {
            
            return View("DepartmanIslemleri");

        }
        [HttpPost]
        public ActionResult DepartmanKaydet(IK_Departman p1)
        {
            if (p1.DepartmanID == 0)
            {
          
                p1.Aktiflik = true;
                p1.Calısmadurumu = true;
                db.IK_Departman.Add(p1);
                var dprtmnno = db.IK_Departman.FirstOrDefault(x => x.Departman_No == p1.Departman_No);

                if (dprtmnno == null || dprtmnno.Departman_No == 0)
                {

                }
                else
                {
                    var maxno = db.IK_Departman.Max(x => x.Departman_No).ToString();
                    int intno = Convert.ToInt32(maxno);
                    p1.Departman_No = intno + 1;
                }
                var dprt = db.IK_Departman.FirstOrDefault(x => x.Departman_No == p1.Departman_No);
             
                db.SaveChanges();
            }
            else
            {
                var departman = db.IK_Departman.FirstOrDefault(x => x.DepartmanID == p1.DepartmanID);
                var dprtmnno = db.IK_Departman.FirstOrDefault(x => x.Departman_No == p1.Departman_No);
                if (departman != null)
                {
                    departman.DepartmanID = p1.DepartmanID;
                    departman.DepartmanIsim = p1.DepartmanIsim;
                    if (dprtmnno == null || dprtmnno.Departman_No == 0)
                    {
                        departman.Departman_No = p1.Departman_No;
                    }
                    else
                    {
                        var maxno = db.IK_Departman.Max(x => x.Departman_No).ToString();
                        int intno = Convert.ToInt32(maxno);
                        departman.Departman_No = intno + 1;
                        
                    }


                }

            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int id)
        {
            {
                var kategori = db.IK_Departman.FirstOrDefault(x => x.DepartmanID == id);
                kategori.Calısmadurumu = false;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        public ActionResult DprtmnGunelle(int id)
        {
            var deger = db.IK_Departman.FirstOrDefault(x => x.DepartmanID == id);
            db.IK_Departman.ToList();
            return View("DepartmanIslemleri", deger);

        }
        public ActionResult ExportToExcel()
        {
            // Verilerinizi alın (örneğin, veritabanından)
            var data = db.IK_Departman.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Departmanlar");

                // Başlıkları ekleyin
                worksheet.Cell(1, 1).Value = "DepartmanID";
                worksheet.Cell(1, 2).Value = "Departmna İsim";
                worksheet.Cell(1, 3).Value = "Departman No";


                // Verileri ekleyin
                int row = 2;
                foreach (var item in data)
                {
                    if (item.Aktiflik == true) { 
                    worksheet.Cell(row, 1).Value = item.DepartmanID;
                    worksheet.Cell(row, 2).Value = item.DepartmanIsim;
                    worksheet.Cell(row, 3).Value = item.Departman_No;

                    row++;
                }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Departmanlar.xlsx");
                }
            }

        }
        public ActionResult DepartmanAktiflik(int id) { 
        
            var degerler = db.IK_Departman.FirstOrDefault(x=>x.DepartmanID == id);
            if (degerler.Aktiflik == true) { 
             degerler .Aktiflik = false;
            }
            else
            {
                degerler.Aktiflik=true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}