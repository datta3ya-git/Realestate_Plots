using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class ProjectReview
    {
        public int?  PRID { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public int Rating { get; set; }
        public string RevirwTitle { get; set; }
        public string ReviewDesc { get; set; }
        public List<ProjectsReviewAttachments> Images { get; set; }
        public DateTime Date { get; set; }
    }


    public class ProjectsReviewAttachments
    {
        public string PhotoTitle { get; set; }
        public string PhotoDecription { get; set; }
        public string PhotoName { get; set; }
    }
}