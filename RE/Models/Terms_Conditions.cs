using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class Terms_Conditions
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string IPAddress { get; set; }
        public string DeviceInfo { get; set; }
    }
    public class Terms_ConditionsInfo
    {
        public int NewTermsId { get; set; }
        public int VersionNumber { get; set; }
    }
    public class Terms_ConditionsResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Terms_ConditionsInfo Terms_Conditions { get; set; }
    }

    public class Terms_ConditionsResponceAccept
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsAccepted { get; set; }
    }
}