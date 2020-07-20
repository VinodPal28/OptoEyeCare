using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace OptoEyeCare.Models
{
    public class users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int isActive { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int flag { get; set; }
    }
}