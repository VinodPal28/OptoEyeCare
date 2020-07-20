using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OptoEyeCare.Models
{
    public class frames
    {
        public int Id { get; set; }
        public string frameName { get; set; }
        public string model { get; set; }
        public int flag { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class frameModel
    {
        public List<frames> frameData { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
    }
}