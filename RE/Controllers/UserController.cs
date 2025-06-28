using RE.BusinesLogic;
using RE.Filters;
using RE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace RE.Controllers
{
    [ApiKeyAttribute]
    public class UserController : ApiController
    {

        DBLogic dblogic = new DBLogic();
        [Route("api/User/AddUser")]
        [Route("api/User/AddEndUser")]
        [HttpPost]
        public HttpResponseMessage AddUser(Users user)
        {
            UserResponce res = new UserResponce();

            string responce = dblogic.AddUsers(user);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "User Added Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/User/DeactivateUser")]
        [HttpPost]
        public HttpResponseMessage DeactivateUser(Users user)
        {
            UserResponce res = new UserResponce();

            string responce = dblogic.DeactivateUser(user);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "User Deactivated Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/User/GetUsers")]
        [HttpPost]
        public HttpResponseMessage GetUsers(Users user)
        {
            FCMNotification objFCM = new FCMNotification();
            bool isSuccess = objFCM.PushNotification(ConfigurationManager.AppSettings["FCM_Project_Title"], ConfigurationManager.AppSettings["FCM_Project_Body"]);
            UsersResponce res = new UsersResponce();
            List<Users> users = new List<Users>();
            users = dblogic.GetUsers(user);
            if (users.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Users Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.users = users;
            return jsonconvert(res);
        }

        [Route("api/User/GetRoles")]
        [HttpPost]
        public HttpResponseMessage GetRoles()
        {
            RolesResponce res = new RolesResponce();
            List<Role> Role = new List<Role>();
            Role = dblogic.GetRoles();
            if (Role.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Role Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Role = Role;
            return jsonconvert(res);
        }


        [Route("api/User/GetEndUsers")]
        [HttpPost]
        public HttpResponseMessage GetEndUsers(Users user)
        {
            UsersResponce res = new UsersResponce();
            List<Users> users = new List<Users>();
            users = dblogic.GetEndUsers(user);
            if (users.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Users Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.users = users;
            return jsonconvert(res);
        }

        [Route("api/User/GetUsersByMobile")]
        [HttpPost]
        public HttpResponseMessage GetUsersByMobile(Users user)
        {
            UsersResponce res = new UsersResponce();
            List<Users> users = new List<Users>();
            users = dblogic.GetUsersByMobile(user);
            if (users.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Users Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.users = users;
            return jsonconvert(res);
        }

        [Route("api/User/CreateUser")]
        [Route("api/User/CreateEndUser")]
        [HttpPost]
        public HttpResponseMessage CreateUser(Users user)
        {
            UserResponce res = new UserResponce();
            checkUsersStatus res1 = new checkUsersStatus();
            res1 = dblogic.CreateUsers(user);

            if (res1.Exists == "" && res1.NotExists == "")
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            else if (res1.Exists == "" && res1.NotExists != "")
            {
                res.StatusCode = 200;
                res.Message = "Users Added Successfully";
            }
            else if (res1.Exists != "" && res1.NotExists == "")
            {
                res.StatusCode = 201;
                res.Message = "Users already assigned to projects";
            }
            else if (res1.Exists != "" && res1.NotExists != "")
            {
                res.StatusCode = 202;
                res.Message = "Some of the projects already assigned with the User. We have added un assigned projects to the User.";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }


        [Route("api/User/GetUsersHierarchy")]
        [HttpPost]
        public HttpResponseMessage GetUsersHierarchy(Users user)
        {
            UsersResponce res = new UsersResponce();
            List<Users> users = new List<Users>();
            users = dblogic.GetUsersHierarchy(user);
            if (users.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Users Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.users = users;
            return jsonconvert(res);
        }

        [Route("api/User/GetAgents")]
        [HttpPost]
        public HttpResponseMessage GetAgents(ProjectAssign proj)
        {
            UsersResponce res = new UsersResponce();
            List<Users> users = new List<Users>();
            users = dblogic.GetAgents(proj);
            if (users.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Agent Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.users = users;
            return jsonconvert(res);
        }


        [Route("api/User/OtpAutenticatoionAndAuthorization")]
        [HttpPost]
        public HttpResponseMessage OtpAutenticatoionAndAuthorization(InputUserOtp users)
        {
            UsersOTPResponce res = new UsersOTPResponce();
            UserOtp objUserOtp = new UserOtp();
            objUserOtp = dblogic.OtpAutenticatoionAndAuthorization(users);
            if (!string.IsNullOrWhiteSpace(objUserOtp.Mobile) && objUserOtp.Mobile == users.Mobile)
            {
                res.StatusCode = 200;
                res.Message = "Valid User and OTP sent to User Mobile.";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No User Data Found";
            }
            res.userInfo = objUserOtp;
            return jsonconvert(res);
        }

        [Route("api/User/verifySignupWithOTP")]
        [HttpPost]
        public HttpResponseMessage verifySignupWithOTP(InputUserSignupOtp users)
        {
            UsersSignupOTPResponce res = new UsersSignupOTPResponce();
            UserSignupOtp objUserOtp = new UserSignupOtp();
            objUserOtp = dblogic.verifySignupWithOTP(users);
            if (!string.IsNullOrWhiteSpace(objUserOtp.OTP))
            {
                res.StatusCode = 200;
                res.Message = objUserOtp.Message;
            }
            else
            {
                res.StatusCode = 204;
                res.Message = objUserOtp.Message;
            }
            res.userInfo = objUserOtp;
            return jsonconvert(res);
        }


        [Route("api/User/UpdateUserPIN")]
        [HttpPost]
        public HttpResponseMessage UpdateUserPIN(Users user)
        {
            UserResponce res = new UserResponce();
            string strRes = "";
            strRes = dblogic.UpdateUserPIN(user);

            if (strRes == "200")
            {
                res.StatusCode = 200;
                res.Message = "PIN updated successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }


        [Route("api/User/ValidateOTP")]
        [HttpPost]
        public HttpResponseMessage ValidateOTP(OTPCls otp)
        {
            UserResponce res = new UserResponce();

            string responce = dblogic.ValidateOTP(otp);
            if (responce != "")
            {
                res.StatusCode = 200;
                res.Message = responce;
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "InValid";
            }
            return jsonconvert(res);
        }
        public HttpResponseMessage jsonconvert(object Values)
        {
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(Values);
            response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
