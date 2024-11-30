using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateProject.Models.Classes
{
    public class Message
    {
        [Key]
        public int messageId { get; set; }
        public string  fullName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public string messageDetail { get; set; }
    }
}