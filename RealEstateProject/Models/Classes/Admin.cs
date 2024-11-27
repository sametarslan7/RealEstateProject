using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateProject.Models.Classes
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string nickanme { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}