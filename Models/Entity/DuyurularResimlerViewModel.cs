using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsanKaynaklarıProje.Models.Entity
{
    public class DuyurularResimlerViewModel
    {
       
        public IEnumerable <IK_Duyuru> IK_Duyurus { get; set; }

        
        public IK_Duyuru IK_Duyuru { get; set; }
    }
}