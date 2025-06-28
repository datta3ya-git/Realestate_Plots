using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }
        public string ProjectID { get; set; }

        public string IdProofNo { get; set; }

        public string IdProofType { get; set; }

        public bool IsPaid { get; set; }

        public string VentureIDs { get; set; }

        public string RoleName { get; set; }
        public string VentureName { get; set; }
        public string VentureAddress { get; set; }

        public int Level { get; set; }
        public int VentureID { get; set; }
        public List<string> AssignedVentureIDs { get; set; }
        public bool isApproved { get; set; }
        public string PIN { get; set; }

    }

    public class UserResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
    public class UsersResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<Users> users { get; set; }
    }

    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class RolesResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<Role> Role { get; set; }
    }

    public class checkUsersStatus
    {
        public string Exists { get; set; }
        public string NotExists { get; set; }
    }
}