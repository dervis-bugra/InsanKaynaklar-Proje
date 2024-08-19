
using InsanKaynaklarıProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using System.IO;
using System.Web.Mvc;

namespace InsanKaynaklarıProje.Controllers
{
    [Authorize(Roles = "4,3")]
    public class MaliyeController : Controller
    {

        // GET: Maliye
        DbIKProjeEntities db = new DbIKProjeEntities();
        public ActionResult Index()
        {  
            var degerler = db.Maliye.ToList();
            foreach(var mly in degerler)
            {
                int? a = mly.satis - mly.maliyet;
                mly.kar = a;
            }
           
            return View(degerler);
        }
        [HttpGet]
        public ActionResult MaliyeEkle() { 
            
            
            return View("MaliyeKayıt"); }

        [HttpGet]
        public ActionResult MaliyeGuncelle(int id) { 
         var dgr = db.Maliye.FirstOrDefault(x=>x.MaliyeID == id);

            return View("MaliyeKayıt" ,dgr);
        }
        public ActionResult MaliyeSil(int id) { 
        
        var dgr = db.Maliye.FirstOrDefault(x=>x.MaliyeID==id);
           dgr.CalismaDurumu= false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult MaliyeKayıt(Maliye p) { 
        
            var mly = db.Maliye.FirstOrDefault(x=>x.MaliyeID == p.MaliyeID);

            if (mly == null)
            {
                p.CalismaDurumu = true;
                p.Aktiflik = true;
                db.Maliye.Add(p);
                db.SaveChanges();
            }
            else
            {
                mly.maliyet = p.maliyet;
                mly.Urun = p.Urun;  
                mly.satis = p.satis;

                mly.kar = p.satis - p.maliyet;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult ExportToExcel()
        {
            // Verilerinizi alın (örneğin, veritabanından)
            var data = db.Maliye.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Maliyetler");

                // Başlıkları ekleyin
                worksheet.Cell(1, 1).Value = "MaliyeID";
                worksheet.Cell(1, 2).Value = "Ürün";
                worksheet.Cell(1, 3).Value = "Maliyet";
                worksheet.Cell(1, 4).Value = "Satış";
                worksheet.Cell(1, 5).Value = "Kar";

                // Verileri ekleyin
                int row = 2;
                int? toplamkar = 0;
                foreach (var item in data)
                {
                    if(item.Aktiflik == true) {
                        int? kar = toplamkar;
                    worksheet.Cell(row, 1).Value = item.MaliyeID;
                    worksheet.Cell(row, 2).Value = item.Urun;
                    worksheet.Cell(row, 3).Value = item.maliyet;
                    worksheet.Cell(row, 4).Value = item.satis;
                    worksheet.Cell(row, 5).Value = item.satis - item.maliyet; // Kar hesaplama
                    toplamkar = (item.satis-item.maliyet) + kar;
                    row++;
                    }
                }
                worksheet.Cell(row+1, 6).Value = "Toplam Kar =";
                worksheet.Cell(row+1 , 7 ).Value = toplamkar;


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Maliyetler.xlsx");
                }
            }
        }
        public ActionResult MaliyeAktiflik(int id)
        {
            var degerler = db.Maliye.FirstOrDefault(x=>x.MaliyeID == id);

            if(degerler.Aktiflik == false)
            {
                degerler.Aktiflik = true;
            }
            else
            {
                degerler.Aktiflik = false;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
    }
    
   



