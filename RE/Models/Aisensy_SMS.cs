using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RE.Models
{
    public class Aisensy_SMS
    {

        public async Task<bool> SendWhatsApp(string MblNo, string otp, string CampaignName, string Username)
        {
            bool res = false;
            try
            {
                res = true;

                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, ConfigurationManager.AppSettings["SMS_aisensy_URL"]);

                string sms_Key = ConfigurationManager.AppSettings["SMS_aisensy_KEY"];


                var payload = new
                {

                    OtpPayload = new
                    {
                        apiKey = sms_Key,
                        campaignName = CampaignName,
                        destination = "91" + MblNo,
                        userName = Username,
                        source = "organic",
                        templateParams = new[] { otp },
                        buttons = new[]
                            {
                                new {
                                    type = "button",
                                    sub_type = "url",
                                    index = "0",
                                    parameters = new[]
                                    {
                                        new { type = "text", text = otp }
                                    }
                                }
                            }
                    },
                };


                string jsonBody = string.Empty;

                // Replace this line:
                // jsonBody = JsonSerializer.Serialize(payload.OtpPayload);
                // With the following line:


                if (CampaignName == ConfigurationManager.AppSettings["SMS_aisensy_LoginOTPTemplet"])
                    jsonBody = JsonConvert.SerializeObject(payload.OtpPayload);



                //else if (CampaignName == door_otp_link_before15mins)
                //    jsonBody = JsonSerializer.Serialize(payload.BookingHappened);
                //else if (CampaignName == door_otp_link)
                //    jsonBody = JsonSerializer.Serialize(payload.Before15Mins);
                //else if (CampaignName == booking_Going_To_EndIn_5Min)
                //    jsonBody = JsonSerializer.Serialize(payload.BookingGoingToEndIn5Min);

                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("✅ Success:");
                    Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine($"❌ Error: {response.StatusCode}");
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(error);
                }
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
    }
}