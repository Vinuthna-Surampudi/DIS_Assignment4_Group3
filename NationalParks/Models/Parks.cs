using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalParks.Models
{
    // Model for parks data. These classes can be obtained by either using 
    // the Visual Studio editor (right-click -> Paste Special -> Paste Json as Classes)
    // or sites such as JsonToCSHarp

    public class Class2
    {
        public ICollection<Class1> class1 { get; set; }
    }

    public class Class1
    {
        public int id { get; set; }
        public ICollection<Class1> class1 { get; set; }
        public string dr_no { get; set; }
        public DateTime date_rptd { get; set; }
        public DateTime date_occ { get; set; }
        public string time_occ { get; set; }
        public string area { get; set; }
        public string area_name { get; set; }
        public string rpt_dist_no { get; set; }
        public string crm_cd { get; set; }
        public string crm_cd_desc { get; set; }
        public string mocodes { get; set; }
        public string vict_age { get; set; }
        public string vict_sex { get; set; }
        public string vict_descent { get; set; }
        public string premis_cd { get; set; }
        public string premis_desc { get; set; }
        public string location { get; set; }
        public string cross_street { get; set; }
        //public Location_1 location_1 { get; set; }
    }

    public class Location_1
    {
        public int id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string human_address { get; set; }
    }
    public class Update
    {
        [Key]
        public string id { get; set; }
        [Required]
        
        public string dr_no { get; set; }
        [Required]
        public string vict_age { get; set; }
        [Required]
        public string area_name { get; set; }
        [Required]
        public string vict_sex { get; set; }
        [Required]
        public DateTime date_occ { get; set; }
        [Required]
        public string crm_cd_desc { get; set; }

        public string location { get; set; }

        public string premis_desc { get; set; }
    }

    public class AddNewPark
    {
        public AddNewPark()
        {
            ID = new Guid();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }
        [Required]
        
        public string dr_no { get; set; }
        [Required]
        public string area_name { get; set; }
        [Required]
        public string vict_age { get; set; }
        [Required]
        public string vict_sex { get; set; }
        [Required]
        public DateTime date_occ { get; set; }
        [Required]
        public string crm_cd_desc { get; set; }

        public string location { get; set; }

        public string premis_desc { get; set; }
    }
}













