using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OptoEyeCare.Models
{
    public class CYLClass
    {
        public int Id { get; set; }
        public string CYL { get; set; }
        public int flag { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }
}