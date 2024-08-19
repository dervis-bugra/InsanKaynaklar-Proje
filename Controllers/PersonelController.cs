
using InsanKaynaklarıProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;
using PagedList;
using PagedList.Mvc;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace InsanKaynaklarıProje.Controllers
{

    public class PersonelController : Controller
    {
        DbIKProjeEntities db = new DbIKProjeEntities();
        [Authorize(Roles = "1,3")]
        public ActionResult Index(int sayfa = 1)
        {

            // user name ve ıstedıgımız verılerı burada bulup viewbag ıle layout sayfasına atmam gerekıyor
            var degerler = db.View_PersonelDepartman.ToList();


            string personelad = User.Identity.Name;
            var personeldprtmn = db.View_PersonelDepartman.Where(m => m.PersonelAd == personelad).Select(m => m.DepartmanID).FirstOrDefault();
            ViewBag.personel = degerler;
            ViewBag.personeldprtmn = personeldprtmn;

            return View(degerler);
        }
        [HttpGet]
        public ActionResult PrsnlEkle()
        {
            //var model = new PersonellerDepartmanlarViewModel();
            //model.IK_Departmen = db.IK_Departman.ToList();

            var p = new PersonellerDepartmanlarViewModel()
            {
                IK_Personel = new IK_Personel(),
                IK_Departmen = db.IK_Departman.ToList(),
                ıK_Rols = db.IK_Rol.ToList()

            };


            return View("PersonelIslemleri", p);



        }


        [HttpPost]
        public ActionResult PersonelKaydet(PersonellerDepartmanlarViewModel p, HttpPostedFileBase File)
        {
            // burada image dosyasında klasore ıd numarası ıle bırlıkte klasor ekleyıp oraya kaydetmek amacımız


            string klasor = Server.MapPath("~/Content/image/personel/" + p.IK_Personel.PersonelID);
            if (p.IK_Personel.PersonelID == 0)
            {
                p.IK_Personel.Aktiflik = true;
                var personel = db.IK_Personel.FirstOrDefault(x => x.PersonelID == p.IK_Personel.PersonelID);
                db.IK_Personel.Add(p.IK_Personel);
                var personelSicil = db.IK_Personel.FirstOrDefault(x => x.PersonelSicilNo == p.IK_Personel.PersonelSicilNo);

                if (personelSicil != null && personelSicil.PersonelSicilNo != 0)
                {

                }
                else
                {
                    var maxSicil = db.IK_Personel.Max(x => x.PersonelSicilNo).ToString();
                    int pIntSicil = Convert.ToInt32(maxSicil);
                    p.IK_Personel.PersonelSicilNo = pIntSicil + 1;
                }
                db.SaveChanges();

                IK_Personel per = db.IK_Personel.FirstOrDefault(x => x.PersonelID == p.IK_Personel.PersonelID);
                string klasor2 = Server.MapPath("~/Content/image/personel/" + p.IK_Personel.PersonelID);
                if (File != null)
                {




                    if (Directory.Exists(klasor2) == false) //klasor var mı bakıyoruz varsa girme yoksa olustur
                    {
                        Directory.CreateDirectory(klasor2);
                    }


                    string path = Path.Combine("~/Content/image/personel/" + p.IK_Personel.PersonelID + "/" + File.FileName); //ıd adı ile klasor acıyoruz 
                    string imgpath = "/Content/image/personel/" + p.IK_Personel.PersonelID + "/" + File.FileName;
                    File.SaveAs(Server.MapPath(path));
                    per.PersonelResim = imgpath;

                }
                else
                {
                    string path = Path.Combine("~/Content/image/personel/" + p.IK_Personel.PersonelID + "/img_man.jpg"); //ıd adı ile klasor acıyoruz 
                    string imgpath = "/Content/image/personel/" + p.IK_Personel.PersonelID + "/img_man.jpg";
                    per.PersonelResim = "/Content/image/img_man.jpg";

                }


            }
            else
            {

                var personel = db.IK_Personel.FirstOrDefault(x => x.PersonelID == p.IK_Personel.PersonelID);
                var personelSicil = db.IK_Personel.FirstOrDefault(x => x.PersonelSicilNo == p.IK_Personel.PersonelSicilNo);

                if (personel != null)
                {
                    personel.PersonelAd = p.IK_Personel.PersonelAd;
                    personel.PersonelSoyad = p.IK_Personel.PersonelSoyad;

                    if (personelSicil != null && personelSicil.PersonelSicilNo != 0)
                    {
                        personel.PersonelSicilNo = p.IK_Personel.PersonelSicilNo;
                    }
                    else
                    {
                        var maxSicil = db.IK_Personel.Max(x => x.PersonelSicilNo).ToString();
                        int pIntSicil = Convert.ToInt32(maxSicil);
                        personel.PersonelSicilNo = pIntSicil + 1;
                    }
                    personel.PersonelID = p.IK_Personel.PersonelID;
                    personel.PersonelDogumTarihi = p.IK_Personel.PersonelDogumTarihi;
                    personel.PersonelCinsiyet = p.IK_Personel.PersonelCinsiyet;
                    personel.PersonelSifre = p.IK_Personel.PersonelSifre;
                    personel.PersonelDepartmanId = p.IK_Personel.PersonelDepartmanId;
                    personel.PersonelRol = p.IK_Personel.PersonelRol;
                    if (File != null)
                    {


                        if (Directory.Exists(klasor) == false)
                        {
                            Directory.CreateDirectory(klasor);
                        }

                        p.IK_Personel.PersonelResim = File.FileName.ToString();

                        string path = Path.Combine("~/Content/image/personel/" + p.IK_Personel.PersonelID + "/" + File.FileName); //ıd adı ile klasor acıyoruz 
                        string imgpath = "/Content/image/personel/" + p.IK_Personel.PersonelID + "/" + File.FileName;
                        personel.PersonelResim = imgpath;

                        File.SaveAs(Server.MapPath(path));


                        //string path = Path.Combine("~/Content/image/personel/" + p.IK_Personel.PersonelID + "/" + personel.PersonelResim);

                        //File.SaveAs(Server.MapPath(path));

                    }


                }

            }

            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult PrsnlSil(int id)
        {
            var degerler = db.IK_Personel.FirstOrDefault(x=>x.PersonelID == id);
            degerler.CalısmaDurumu = false;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult PrsnlGuncelle(int id) // id sine bakıyoruz burada x => x ile sonra id yi personelguncelle wiewına yolluyoruz 
        {

            var p = new PersonellerDepartmanlarViewModel()
            {

                IK_Personel = db.IK_Personel.FirstOrDefault(x => x.PersonelID == id),

                IK_Departmen = db.IK_Departman.ToList(),
                ıK_Rols = db.IK_Rol.ToList(),
                ıK_Personels = db.IK_Personel.ToList(),





            };

            return View("PersonelIslemleri", p);


        }
        public ActionResult ExportToExcel()
        {
            // Verilerinizi alın (örneğin, veritabanından)
            var data = db.View_PersonelDepartman.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Personeller");

                // Başlıkları ekleyin
                worksheet.Cell(1, 1).Value = "PersonelID";
                worksheet.Cell(1, 2).Value = "Departman";
                worksheet.Cell(1, 3).Value = "PersonelAd";
                worksheet.Cell(1, 4).Value = "PersonelSoyad";
                worksheet.Cell(1, 5).Value = "Sicil No";
                worksheet.Cell(1, 6).Value = "Cinsiyet";
                worksheet.Cell(1, 7).Value = "Doğum Tarihi";

                worksheet.Cell(1, 8).Value = " Rol";


                // Verileri ekleyin
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cell(row, 1).Value = item.PersonelID;
                    worksheet.Cell(row, 2).Value = item.DepartmanIsim;
                    worksheet.Cell(row, 3).Value = item.PersonelAd;
                    worksheet.Cell(row, 4).Value = item.PersonelSoyad;
                    worksheet.Cell(row, 5).Value = item.PersonelSicilNo;
                    worksheet.Cell(row, 6).Value = item.PersonelCinsiyet;
                    worksheet.Cell(row, 7).Value = item.PersonelDogumTarihi;
                    worksheet.Cell(row, 8).Value = item.RolIsim;



                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Personel.xlsx");
                }
            }
        }
        
      public ActionResult PrsnlAktiflik(int id)
        {
            var degerler =db.IK_Personel.FirstOrDefault(x=>x.PersonelID == id);
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