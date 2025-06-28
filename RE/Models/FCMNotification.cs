using Google.Apis.Auth.OAuth2;
using RE.BusinesLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace RE.Models
{
    public class Notifications
    {
        public int UserID { get; set; }
        public string DeviceId { get; set; }
    }
    public class FCMNotification
    {
        public bool PushNotification(string title, string body)
        {
            bool isSuccess = true; 
            try
            {
                DBLogic dblogic = new DBLogic();
                List<Notifications> fcmDetails = dblogic.GetFCMDetails();

                foreach (Notifications objNotify in fcmDetails)
                {
                    //Notify(title, body, objNotify.DeviceId);

                    GenerateFCM_Auth_SendNotifcn(title, body, objNotify.DeviceId);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                string str = ex.Message;
            }
            return isSuccess;
        }

        public bool Notify(string title, string body, string deviceID)
        {
            bool isSuccess = true;
            try
            {
                var applicationID = ConfigurationManager.AppSettings["FCMApplicationID"];
                var senderId = ConfigurationManager.AppSettings["FCMSenderId"];
                string deviceId = deviceID;

                WebRequest tRequest = WebRequest.Create(ConfigurationManager.AppSettings["FCM_URL"]);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = body,
                        title = title
                        //icon = "myicon"
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                string str = ex.Message;
            }
            return isSuccess;
        }


        public void GenerateFCM_Auth_SendNotifcn(string title, string body, string deviceID)

        {
            var FCM_URL = ConfigurationManager.AppSettings["FCM_URL"];
            //----------Generating Bearer token for FCM---------------

            //string fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/bookyourplot-firebase-adminsdk-k4ltx-cb75ac31fe.json"); //Download from Firebase Console ServiceAccount
            string fileName = System.Web.Hosting.HostingEnvironment.MapPath("~/manaplots-9d9b7-firebase-adminsdk-snikx-85829c01d7.json"); //Download from Firebase Console ServiceAccount

            string scopes = "https://www.googleapis.com/auth/firebase.messaging";
            var bearertoken = ""; // Bearer Token in this variable
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))

            {

                bearertoken = GoogleCredential
                  .FromStream(stream) // Loads key file
                  .CreateScoped(scopes) // Gathers scopes requested
                  .UnderlyingCredential // Gets the credentials
                  .GetAccessTokenForRequestAsync().Result; // Gets the Access Token

            }

            ///--------Calling FCM-----------------------------

            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);

            client.BaseAddress = new Uri(FCM_URL); // FCM HttpV1 API

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //client.DefaultRequestHeaders.Accept.Add("Authorization", "Bearer " + bearertoken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearertoken); // Authorization Token in this variable

            //---------------Assigning Of data To Model --------------


            Root rootObj = new Root();
            rootObj.message = new Message();

            rootObj.message.token = deviceID; //FCM Token id

            rootObj.message.data = new Data();
            rootObj.message.data.title = title;
            rootObj.message.data.body = body;
            rootObj.message.data.key_1 = "Key";
            rootObj.message.data.key_2 = "Key2";
            rootObj.message.notification = new Notification();
            rootObj.message.notification.title = title;
            rootObj.message.notification.body = body;
            //rootObj.message.sender_id = "137053865114";
            //-------------Convert Model To JSON ----------------------

            var jsonObj = new JavaScriptSerializer().Serialize(rootObj);

            //------------------------Calling Of FCM Notify API-------------------

            var data = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = client.PostAsync(FCM_URL, data).Result; // Calling The FCM httpv1 API

            //---------- Deserialize Json Response from API ----------------------------------

            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var responseObj = new JavaScriptSerializer().DeserializeObject(jsonResponse);

        }
    }





    #region FCM Auth & Send Notification To Mobile //notify FCM Code

    public class Data
    {

        public string body
        {
            get;
            set;
        }

        public string title
        {
            get;
            set;
        }

        public string key_1
        {
            get;
            set;
        }

        public string key_2
        {
            get;
            set;
        }

    }

    public class Message
    {

        public string token
        {
            get;
            set;
        }

        public Data data
        {
            get;
            set;
        }

        public Notification notification
        {
            get;
            set;
        }
    }

    public class Notification
    {

        public string title
        {
            get;
            set;
        }

        public string body
        {
            get;
            set;
        }

    }

    public class Root
    {

        public Message message
        {
            get;
            set;
        }

    }

   

    #endregion
}