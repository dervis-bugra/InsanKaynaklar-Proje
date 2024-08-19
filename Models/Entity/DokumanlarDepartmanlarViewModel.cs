using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsanKaynaklarıProje.Models.Entity
{
    public class DokumanlarDepartmanlarViewModel
    {
        public IEnumerable<IK_Departman> IK_Dprtmn { get; set; }   
        public IEnumerable<Dokumanlar> Dkmnlr { get; set; }
        public Dokumanlar Dokumanlar { get; set; }

        public IK_Departman dprtmn { get; set; }
    }
}