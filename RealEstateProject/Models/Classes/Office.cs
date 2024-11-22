using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateProject.Models.Classes
{
    public class Office
    {
        [Key]
        public int officeId { get; set; }
        public string officeCity { get; set; }
        public string officeFullAdress { get; set; }
    }
}