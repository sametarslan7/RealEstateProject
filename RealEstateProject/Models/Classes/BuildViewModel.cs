using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateProject.Models.Classes
{
    public class BuildViewModel
    {
        public Build Build { get; set; }
        public string BuildTypeName { get; set; }
        public List<Image> Images { get; set; }
    }
}