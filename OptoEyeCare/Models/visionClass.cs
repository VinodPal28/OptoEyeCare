﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OptoEyeCare.Models
{
    public class visionClass
    {
        public int Id { get; set; }
        public string visionValue { get; set; }
        public int flag { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }
}