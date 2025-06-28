using RE.BusinesLogic;
using RE.Filters;
using RE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace RE.Controllers
{
    [ApiKeyAttribute]
    public class CustomerController : ApiController
    {
        DBLogic dblogic = new DBLogic();
        [Route("api/Customer/AddProject")]
        [Route("api/Customer/UpdateProject")]
        [HttpPost]
        public HttpResponseMessage AddProject(Projects project)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.AddProjects(project);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = project.ProjectID == 0 ? "Project Added Successfully" : "Project Updated Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Customer/GetProjects")]
        [HttpPost]
        public HttpResponseMessage GetProjects(Projects project)
        {
            ProjectsResponce res = new ProjectsResponce();
            List<ProjectsMini> proj = new List<ProjectsMini>();
            proj = dblogic.GetProject(project);
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Project Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }

        [Route("api/Customer/GetProjectByID")]
        [HttpPost]
        public HttpResponseMessage GetProjectbyID(Projects project)
        {
            ProjectsSpecResponce res = new ProjectsSpecResponce();
            Projects proj = new Projects();
            proj = dblogic.GetProjectbyID(project);
            if (proj.ProjectID != 0)
            {
                res.StatusCode = 200;
                res.Message = "Project Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }

        [Route("api/Customer/AddPlots")]
        [Route("api/Customer/UpdatePlots")]
        [HttpPost]
        public HttpResponseMessage AddPlots(List<Plots> Plots)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            foreach (var Plot in Plots)
            {
                string responce = dblogic.AddPlots(Plot);
                if (responce == "200")
                {
                    res.StatusCode = 200;
                    res.Message = Plot.PlotID == 0 ? "Plots Added Successfully" : "Plots Updated Successfully";
                }
                else
                {
                    res.StatusCode = 500;
                    res.Message = "Internal Server Error";
                }
            }
            return jsonconvert(res);
        }

        [Route("api/Customer/GetPlots")]
        [Route("api/Customer/GetPlotsByID")]
        [HttpPost]
        public HttpResponseMessage GetPlots(Plots Plot)
        {
            PlotsResponce res = new PlotsResponce();
            List<Plots> plot = new List<Plots>();
            plot = dblogic.GetPlots(Plot);
            if (plot.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Plots Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Plots = plot;
            return jsonconvert(res);
        }

        [Route("api/Customer/AssignProjects")]
        [Route("api/Customer/AssignPlots")]
        [HttpPost]
        public HttpResponseMessage AssignProjectsandPlots(ProjectAssign proj)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.AssignProjectsandPlots(proj);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = proj != null && proj.PlotID != null && proj.PlotID.Count > 0 ? "Plot Assigned Successfully" : "Project Assigned Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Customer/SaveImages")]
        [HttpPost]
        public async Task<HttpResponseMessage> saveImages()
        {
            string RoutePath = ConfigurationManager.AppSettings["RoutePath"];
            ImageFiles img = new ImageFiles();
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var uploadPath = HostingEnvironment.MapPath("/") + @"/Images";
                Directory.CreateDirectory(uploadPath);
                var provider = new MultipartFormDataStreamProvider(uploadPath);
                await Request.Content.ReadAsMultipartAsync(provider);

                // Files
                //
                string ImageNames = "";
                foreach (MultipartFileData file in provider.FileData)
                {
                    // Debug.WriteLine(file.Headers.ContentDisposition.FileName);
                    //  Debug.WriteLine("File path: " + file.LocalFileName);

                    if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                    {
                        // return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                    }
                    string fileName = file.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    Guid id = Guid.NewGuid();
                    string guid = id.ToString();
                    string FileNameStr = guid + "_" + fileName;

                    File.Move(file.LocalFileName, Path.Combine(uploadPath, FileNameStr));
                    // FileNameStr = RoutePath + "/Images/" + FileNameStr;
                    if (ImageNames == "")
                    {
                        ImageNames = FileNameStr;
                    }
                    else
                    {
                        ImageNames = ImageNames + "," + FileNameStr;
                    }
                }

                // Form data
                //
                //string UniqueID = "";
                //string CheckList = "";
                //string Comments = "";
                //foreach (var key in provider.FormData.AllKeys)
                //{
                //    foreach (var val in provider.FormData.GetValues(key))
                //    {
                //        // Debug.WriteLine(string.Format("{0}: {1}", key, val));
                //        if (key == "UniqueID")
                //        {
                //            UniqueID = val;
                //        }
                //        if (key == "CheckList")
                //        {
                //            CheckList = val;
                //        }
                //        if (key == "Comments")
                //        {
                //            Comments = val;
                //        }
                //    }
                //}

                img.ImageName = ImageNames;
                img.StatusCode = 200;
                img.Message = "File Uploaded Successfully.";
            }
            catch (Exception ex)
            {
                img = new ImageFiles();
                img.StatusCode = 500;
                img.Message = "File Uploaded Failed.";
            }
            return jsonconvert(img);
        }


        [Route("api/Customer/PlotSold")]
        [HttpPost]
        public HttpResponseMessage PlotSold(Plots Plot)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.PlotSold(Plot);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Plot Sold Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Customer/GetPlotsforApprovel")]
        [HttpPost]
        public HttpResponseMessage GetPlotsforApprovel(Projects project)
        {
            PlotsWithUserResponce res = new PlotsWithUserResponce();
            List<PlotsWithUser> proj = new List<PlotsWithUser>();
            proj = dblogic.GetPlotsforApprovel(project);
            res.StatusCode = 200;
            res.Message = "Success";
            res.PlotsWithUser = proj;
            return jsonconvert(res);
        }

        [Route("api/Customer/ApprovePlots")]
        [HttpPost]
        public HttpResponseMessage ApprovePlots(PlotsApprove Plots)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.AssignPlotsApprove(Plots);

            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Plots Approved Success";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }


        [Route("api/Customer/GetPendingAgents")]
        [HttpPost]
        public HttpResponseMessage GetPendingAgents(Users User)
        {
            AgentResponce res = new AgentResponce();
            List<Agent> Agent = new List<Agent>();
            Agent = dblogic.GetPendingAgents(User);
            res.StatusCode = 200;
            res.Message = "Success";
            res.Agents = Agent;
            return jsonconvert(res);
        }

        [Route("api/Customer/ApproveAgents")]
        [HttpPost]
        public HttpResponseMessage ApproveAgents(List<AgentApprove> Agents)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.ApproveAgents(Agents);

            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Agents Approved Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
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

        [Route("api/Customer/LikeProject")]
        [HttpPost]
        public HttpResponseMessage LikeProject(LikeProjects Proj)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.SaveLikeProjects(Proj);

            if (responce == "200")
            {
                res.StatusCode = 200;
                if (Proj.isLiked)
                    res.Message = "Project liked Successfully";
                else
                    res.Message = "Project un-liked Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        [Route("api/Customer/GetLikedProjects")]
        [HttpPost]
        public HttpResponseMessage GetLikedProjects(LikeProjects project)
        {
            ProjectsResponce res = new ProjectsResponce();
            List<ProjectsMini> proj = new List<ProjectsMini>();
            proj = dblogic.GetLikedProject(project);
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Liked Project Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }

        [Route("api/Customer/EnquiryProjects")]
        [HttpPost]
        public HttpResponseMessage SaveEnquiryProjects(LikeProjects Proj)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.SaveEnquiryProjects(Proj);

            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Project Enquiried Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        [Route("api/Customer/CompleteEnquiryProjects")]
        [HttpPost]
        public HttpResponseMessage CompleteEnquiryProjects(LikeProjects Proj)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.CompleteEnquiryProjects(Proj);

            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Project Enquiry Completed Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        [Route("api/Customer/GetEnquiryProjects")]
        [HttpPost]
        public HttpResponseMessage GetEnquiryProjects(LikeProjects project)
        {
            EnquiredProjectsResponce res = new EnquiredProjectsResponce();
            List<EnquiredProject> proj = new List<EnquiredProject>();
            proj = dblogic.GetEnquiryProjects(project);
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Enquired Project Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }


        [Route("api/Customer/uploadPhoto")]
        [HttpPost]
        public HttpResponseMessage uploadPhoto(FileUploadDetails fileUpload)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.InsertProjectsPlotsUploadFiles(fileUpload);

            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Uploaded file details added successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        [Route("api/Customer/ChangeCoverPhoto")]
        [HttpPost]
        public HttpResponseMessage ChangeCoverPhoto(FileUploadDetails fileUpload)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.ChangeCoverPhoto(fileUpload);

            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Cover photo changed successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        [Route("api/Customer/AddProjectRoadsInfo")]
        [HttpPost]
        public HttpResponseMessage AddProjectRoadsInfo(RoadsInfo project)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.AddProjectRoadsInfo(project);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Project RoadsInfo Added Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Customer/AddRecentVisitedProjects")]
        [HttpPost]
        public HttpResponseMessage AddRecentVisitedProjects(Projects project)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.AddRecentVisitedProjects(project);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Project Recent Visited Added Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Customer/GetRecentVisitedProjects")]
        [HttpPost]
        public HttpResponseMessage GetRecentVisitedProjects(Projects project)
        {
            ProjectsResponce res = new ProjectsResponce();
            List<ProjectsMini> proj = new List<ProjectsMini>();
            proj = dblogic.GetRecentVisitedProjects(project);
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Recent Visited Projects Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }

        [Route("api/Customer/GetProjectsListBasedOnRange")]
        [HttpPost]
        public HttpResponseMessage GetProjectsListBasedOnRange(Coordinates objCoord)
        {
            ProjectsResponce res = new ProjectsResponce();
            List<ProjectsMini> proj = new List<ProjectsMini>();
            proj = dblogic.GetProjectsListBasedOnRange(objCoord);
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Projects Details based on the given Range";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }






        [Route("api/Customer/AddUser")]
        [Route("api/Customer/AddEndUser")]
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
        [Route("api/Customer/GetUsers")]
        [HttpPost]
        public HttpResponseMessage GetUsers(Users user)
        {
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

        [Route("api/Customer/GetRoles")]
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


        [Route("api/Customer/GetEndUsers")]
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

        [Route("api/Customer/GetUsersByMobile")]
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

        [Route("api/Customer/CreateUser")]
        [Route("api/Customer/CreateEndUser")]
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


        [Route("api/Customer/GetUsersHierarchy")]
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

        [Route("api/Customer/GetAgents")]
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
    }
}
