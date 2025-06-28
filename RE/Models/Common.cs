using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class Common
    {
        public bool isALL { get; set; }
    }

    public class ResponseCommon
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}