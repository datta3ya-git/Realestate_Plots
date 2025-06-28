using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace RE.Models
{
    public class UserOtp
    {
        public string Mobile { get; set; }
        public int UserID { get; set; }
        public string OTP { get; set; }
        public bool IsFirstLogin { get; set; }
    }
    public class InputUserOtp
    {
        public string Mobile { get; set; }
        public int Role { get; set; }
        public string DeviceId { get; set; }
    }

    public class UserSignupOtp
    {
        public string Mobile { get; set; }
        public string OTP { get; set; }
        public string Message { get; set; }
    }
    public class InputUserSignupOtp
    {
        public string Mobile { get; set; }
        public int Role { get; set; }
    }
    public class UsersSignupOTPResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public UserSignupOtp userInfo { get; set; }
    }

    public class UsersOTPResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public UserOtp userInfo { get; set; }
    }

    public class OTPGeneration
    {
        // Start OTP Generation function
        public string Generate_otp()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < 6; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }
        // End OTP Generation function

        // SMS Sending function
        public bool SendSMS(string MblNo, string Msg, string OTP)
        {
            string MainUrl = "SMSAPIURL"; //Here need to give SMS API URL
            string UserName = "username"; //Here need to give username
            string Password = "Password"; //Here need to give Password
            string SenderId = "SenderId";
            string strMobileno = MblNo;
            string URL = "";
            string SMS_Gateway = ConfigurationManager.AppSettings["SMS_Gateway"];
            string SMS_Gateway_Auth = ConfigurationManager.AppSettings["SMS_Gateway_Auth"];
            //URL = MainUrl + "username=" + UserName + "&msg_token=" + Password + "&sender_id=" + SenderId + "&message=" + HttpUtility.UrlEncode(Msg).Trim() + "&mobile=" + strMobileno.Trim() + "";
            URL = SMS_Gateway + SMS_Gateway_Auth +"&route=otp&variables_values="+ OTP + "&flash=0&numbers="+ MblNo;


            string strResponce = GetResponse(URL);
            bool msg = false;
            if (strResponce.Equals("Fail"))
            {
                msg = false;
            }
            else
            {
                msg = true;
            }
            return msg;
        }
        // End SMS Sending function
        // Get Response function
        public string GetResponse(string smsURL)
        {
            try
            {
                WebClient objWebClient = new WebClient();
                System.IO.StreamReader reader = new System.IO.StreamReader(objWebClient.OpenRead(smsURL));
                string ResultHTML = reader.ReadToEnd();
                return ResultHTML;
            }
            catch (Exception)
            {
                return "Fail";
            }
        }
        // End Get Response function
    }

}