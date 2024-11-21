using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RealEstateProject.Models.Classes
{
    public class Build
    {
        [Key]
        public int buildId { get; set; }
        public string buildTitle { get; set; }
        public int buildPrice { get; set; }
        public int buildYear { get; set; }
        public string buildCity { get; set; }
        public string buildDistrict { get; set; }
        public string buildStatus { get; set; }
        public int buildTypeId { get; set; }
        public string buildDescription { get; set; }

        public BuildType buildType {  get; set; }
        public ICollection<Image> Images { get; set; }
    }
}