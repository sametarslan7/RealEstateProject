using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RealEstateProject.Models.Classes
{
    public class BuildType
    {
        [Key]
        public int buildTypeid { get; set; }
        public string buildType { get; set; }

        public ICollection<Build> Builds { get; set; }
    }
}