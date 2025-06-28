using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class FileUploadDetails
    {
        public string imagepaths { get; set; }
        public string photoTitle { get; set; }
        public string photoDescription { get; set; }
        public int isCoverPhoto { get; set; }
        public int projectID { get; set; }
        public int plotID { get; set; }
        public int UserID { get; set; }
        public int photoID { get; set; }
    }
}