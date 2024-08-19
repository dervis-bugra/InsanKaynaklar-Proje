using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsanKaynaklarıProje.Models.Entity
{
    public class MesajlarViewModel
    {
        public IEnumerable<IK_Personel> Personel { get; set; }
        public IEnumerable<IK_Departman> Departman { get; set; }

        public IEnumerable<IK_Mesaj> mesaj { get; set; }

        public IK_Mesaj IK_Mesaj { get; set; }

        public IK_Personel IK_Personel { get; set; }
        public IK_Departman IK_Departman { get; set; }

        public bool YeniMesaj { get; set; }
    }
}