using RE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RE.Filters
{

    public class ApiKeyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string x_api_keyVal = ConfigurationManager.AppSettings["x_api_key"];
            bool checkAuth = Convert.ToBoolean(ConfigurationManager.AppSettings["checkAuth"]);

            if (checkAuth)
            {
                HttpResponseMessage objCom = new HttpResponseMessage();
                ResponseCommon objres = new ResponseCommon();
                objres.StatusCode = 401;
                objres.Message = "Unauthorized";
                try
                {
                    string x_api_key = actionContext.Request.Headers.SingleOrDefault(x => x.Key == "x-api-key").Value?.First();
                    if (string.IsNullOrEmpty(x_api_key) || x_api_key != x_api_keyVal)
                    {

                        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(objres);
                        objCom.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                        objCom.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        actionContext.Response = objCom;
                    }

                }
                catch (Exception)
                {
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(objres);
                    objCom.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    objCom.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    actionContext.Response = objCom;
                }
            }        
        }
    }
}