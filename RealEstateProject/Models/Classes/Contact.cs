using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateProject.Models.Classes
{
    public class Contact
    {
        [Key]
        public int contactId { get; set; }
        public string emailAdress { get; set; }
        public string phoneNumber { get; set; }
        public string faxNumber { get; set; }
    }
}