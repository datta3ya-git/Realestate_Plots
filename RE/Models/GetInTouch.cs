using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class GetInTouch
    {
        public int GITID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int AssignedUserID { get; set; }
        public string Comments { get; set; }
        public string AssignStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CommentedDate { get; set; }
        public string AssignedName { get; set; }
    }

    public class GetInTouchResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<GetInTouch> GetInTouch { get; set; }
    }
}