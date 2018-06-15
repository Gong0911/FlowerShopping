using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace FlowerShopping.Models
{
    public class UserInfoModel
    {
      
        [Key]   
        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

       
        [Required]
        public string UserPwd { get; set; }

     
        [Required]
        [StringLength(50)]
        public string UserPhone { get; set; }

        [Required]
        [StringLength(50)]
        public string UeserImg { get; set; }
        //public Nullable<System.DateTime> CreateTime { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}