//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsanKaynaklarıProje.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class IK_Personel
    {
        public int PersonelID { get; set; }
        public Nullable<int> PersonelDepartmanId { get; set; }
        public string PersonelDepartmanIsim { get; set; }
        public int PersonelSicilNo { get; set; }
        public string PersonelKullanıcıAdı { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set; }
        public Nullable<System.DateTime> PersonelDogumTarihi { get; set; }
        public string PersonelCinsiyet { get; set; }
        public int PersonelRol { get; set; }
        public string PersonelSifre { get; set; }
        public string PersonelResim { get; set; }
        public Nullable<bool> Aktiflik { get; set; }
        public Nullable<bool> CalısmaDurumu { get; set; }
    }
}
