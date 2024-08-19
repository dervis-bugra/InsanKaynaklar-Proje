using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace InsanKaynaklarıProje.Models.Entity
{
    
      

        // Parametresiz oluşturucu ekleyin
       
    
    public class PersonellerDepartmanlarViewModel
    {


        public IEnumerable<Personel_departman_rol_model_view>  prs_dprtmn_rl {  get; set; }
        public IEnumerable<IK_Departman> IK_Departmen { get; set; }

        public IEnumerable <IK_Personel> ıK_Personels { get; set; }
        public IEnumerable<IK_Rol> ıK_Rols { get; set; }

       public IEnumerable <IK_Duyuru> IK_Duyurus { get; set; }

     
        public IEnumerable <PersonellerDepartmanlarViewModel> personellerDepartmanlarViewModels { get; set; }

        public IK_Duyuru IK_Duyuru { get; set; }
        public IK_Personel IK_Personel { get; set; }

        public IK_Departman dprtmn { get; set; }

        public IK_Rol IK_Rol { get; set; }

        public virtual bool BeniHatırla {  get; set; }

        

    }
}