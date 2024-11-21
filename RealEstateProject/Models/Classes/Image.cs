using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealEstateProject.Models.Classes
{
    public class Image
    {
        [Key]
        public int imageId { get; set; } //Primary Key
        public string imageUrl { get; set; }

        [ForeignKey("Build")] // with connection Build 
        public int BuildId { get; set; } // foreign key to Builds

        public Build Build { get; set; }

    }
}