using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateProject.Models.Classes
{
    public class Project
    {
        [Key]
        public int projectId { get; set; }
        public string projectTitle { get; set; }
        public string projectDescription { get; set; }
        public string projectImageUrl { get; set; }
    }
}