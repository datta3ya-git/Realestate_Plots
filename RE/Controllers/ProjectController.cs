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
using System.Web.Script.Serialization;

namespace RE.Controllers
{
    [ApiKeyAttribute]
    public class ProjectController : ApiController
    {
        DBLogic dblogic = new DBLogic();
        [Route("api/Project/AddProject")]
        [Route("api/Project/UpdateProject")]
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

        [Route("api/Project/GetProjects")]
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

        [Route("api/Project/GetProjectByID")]
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

        [Route("api/Project/AddPlots")]
        [Route("api/Project/UpdatePlots")]
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

        [Route("api/Project/GetPlots")]
        [Route("api/Project/GetPlotsByID")]
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

        //[Route("api/Project/AssignProjects")]
        [Route("api/Project/AssignPlots")]
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

        [Route("api/Project/SaveImages")]
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


        [Route("api/Project/DeleteImages")]
        [HttpPost]
        public HttpResponseMessage deleteImages(ProjectFileName objProjectFileName)
        {
            string RoutePath = ConfigurationManager.AppSettings["RoutePath"];
            ImageFiles img = new ImageFiles();
            try
            {
                var uploadPath = HostingEnvironment.MapPath("/") + @"/Images";

                if (File.Exists(Path.Combine(uploadPath, objProjectFileName.FileName)))
                {
                    string responce = dblogic.deleteImages(objProjectFileName);
                    File.Delete(Path.Combine(uploadPath, objProjectFileName.FileName));
                    img.Message = "File Deleted Successfully.";


                }
                else
                {
                    img.Message = "File Not Exists.";
                }

                img.ImageName = objProjectFileName.FileName;
                img.StatusCode = 200;
            }
            catch (Exception ex)
            {
                img = new ImageFiles();
                img.StatusCode = 500;
                img.Message = "File Deleted Failed.";
            }
            return jsonconvert(img);
        }


        [Route("api/Project/PlotSold")]
        [Route("api/Project/PlotReserve")]
        [Route("api/Project/PlotResell")]
        [HttpPost]
        public HttpResponseMessage PlotSold(Plots Plot)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.PlotSold(Plot);
            if (responce == "200")
            {
                res.StatusCode = 200;
                if (Plot.IsSold == 1)
                    res.Message = "Plot Sold Successfully";
                else if (Plot.IsSold == 2)
                    res.Message = "Plot Reserved Successfully";
                else if (Plot.IsSold == 3)
                    res.Message = "Plot Reselled Successfully";
                else
                    res.Message = "";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/CancelPlotSold")]
        [Route("api/Project/CancelPlotReserve")]
        [Route("api/Project/CancelPlotResell")]
        [HttpPost]
        public HttpResponseMessage CancelPlotSold(Plots Plot)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.CancelPlotSold(Plot);
            if (responce == "200")
            {
                res.StatusCode = 200;
                if (Plot.IsSold == 1)
                    res.Message = "Plot Sold Cancelled";
                else if (Plot.IsSold == 2)
                    res.Message = "Plot Reserved Cancelled";
                else if (Plot.IsSold == 3)
                    res.Message = "Plot Resealed Cancelled";
                else
                    res.Message = "";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/GetPlotsforApprovel")]
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

        [Route("api/Project/ApprovePlots")]
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


        [Route("api/Project/GetPendingAgents")]
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

        [Route("api/Project/ApproveAgents")]
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

        [Route("api/Project/BookmarkProject")]
        [HttpPost]
        public HttpResponseMessage SaveBookmarkProjects(LikeProjects Proj)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.SaveBookmarkProjects(Proj);

            if (responce == "200")
            {
                res.StatusCode = 200;
                if (Proj.isLiked)
                    res.Message = "Project bookmarked Successfully";
                else
                    res.Message = "Project un-bookmarked Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        [Route("api/Project/GetBookmarkProjects")]
        [HttpPost]
        public HttpResponseMessage GetBookmarkProjects(LikeProjects project)
        {
            ProjectsResponce res = new ProjectsResponce();
            List<ProjectsMini> proj = new List<ProjectsMini>();
            proj = dblogic.GetBookmarkProjects(project);
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Bookmark Project Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }

        [Route("api/Project/EnquiryProjects")]
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

        [Route("api/Project/CompleteEnquiryProjects")]
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

        [Route("api/Project/GetEnquiryProjects")]
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


        [Route("api/Project/uploadPhoto")]
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

        [Route("api/Project/ChangeCoverPhoto")]
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

        [Route("api/Project/AddProjectRoadsInfo")]
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

        [Route("api/Project/AddRecentVisitedProjects")]
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

        [Route("api/Project/GetRecentVisitedProjects")]
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

        [Route("api/Project/GetProjectsListBasedOnRange")]
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


        [Route("api/Project/GetLatestProjects")]
        [HttpPost]
        public HttpResponseMessage GetLatestProjects(Common objCommon)
        {
            Projects objProj = new Projects();
            objProj.UserID = 0;
            ProjectsResponce res = new ProjectsResponce();
            List<ProjectsMini> proj = new List<ProjectsMini>();
            proj = dblogic.GetRecentVisitedProjects(objProj, objCommon.isALL ? "" : "LATEST5PROJ");
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Latest Projects Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }

        [Route("api/Project/GetProjectAmenities")]
        [HttpPost]
        public HttpResponseMessage GetProjectAmenities()
        {
            ProjectAmenitiesResponce res = new ProjectAmenitiesResponce();
            List<ProjectAmenities> ProjectAmenities = new List<ProjectAmenities>();
            ProjectAmenities = dblogic.GetProjectAmenities();
            if (ProjectAmenities.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Project Amenities Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.ProjectAmenities = ProjectAmenities;
            return jsonconvert(res);
        }

        #region reviews
        [Route("api/Project/saveRevirewImages")]
        [HttpPost]
        public async Task<HttpResponseMessage> saveRevirewImages()
        {
            string RoutePath = ConfigurationManager.AppSettings["RoutePath"];
            ImageFiles img = new ImageFiles();
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var uploadPath = HostingEnvironment.MapPath("/") + @"/ReviewImages";
                Directory.CreateDirectory(uploadPath);
                var provider = new MultipartFormDataStreamProvider(uploadPath);
                await Request.Content.ReadAsMultipartAsync(provider);

                // Files
                //
                string ImageNames = "";
                foreach (MultipartFileData file in provider.FileData)
                {

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


        [Route("api/Project/AddProjectReview")]
        [Route("api/Project/UpdateProjectReview")]
        [HttpPost]
        public HttpResponseMessage InsertProjectReview(ProjectReview project)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.InsertProjectReview(project);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Project review Added Successfully";
                if (project.PRID != null && project.PRID > 0)
                {
                    res.Message = "Project review updated Successfully";
                }
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/DeleteProjectReview")]
        [HttpPost]
        public HttpResponseMessage DeleteProjectReview(ProjectReview project)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.DeleteProjectReview(project);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Project review deleted Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/getProjectReviews")]
        [HttpPost]
        public HttpResponseMessage getProjectReview(ProjectReview project)
        {
            ProjectReviewResponce res = new ProjectReviewResponce();
            List<ProjectReview> ProjectAmenities = new List<ProjectReview>();
            ProjectAmenities = dblogic.getProjectReview(project);
            if (ProjectAmenities.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Project review Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.ProjectReview = ProjectAmenities;
            return jsonconvert(res);
        }

        #endregion

        [Route("api/Project/UnassignedPlots")]
        [HttpPost]
        public HttpResponseMessage UnassignedPlots(PlotsUnAssigned Plot)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.UnassignedPlots(Plot);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Plot unassigned Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        #region Get In Touch
        [Route("api/Project/AddGetInTouchDetails")]
        [HttpPost]
        public HttpResponseMessage InsertGetInTouchDetails(GetInTouch git)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.InsertGetInTouchDetails(git);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Get In Touch details addess successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/AssignGetInTouchDetails")]
        [HttpPost]
        public HttpResponseMessage AssignGetInTouchDetails(GetInTouch git)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.AssignGetInTouchDetails(git);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Get In Touch details assigned successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/getInTouchDetails")]
        [HttpPost]
        public HttpResponseMessage getInTouchDetails(GetInTouch git)
        {
            GetInTouchResponce res = new GetInTouchResponce();
            List<GetInTouch> GetInTouch = new List<GetInTouch>();
            GetInTouch = dblogic.getInTouchDetails(git);
            if (GetInTouch.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Get In Touch Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.GetInTouch = GetInTouch;
            return jsonconvert(res);
        }

        #endregion

        #region Terms & Conditions
        [Route("api/Project/addTermsAndConditions")]
        [HttpPost]
        public HttpResponseMessage AddTermsAndConditions(Terms_Conditions tc)
        {
            Terms_ConditionsResponce res = new Terms_ConditionsResponce();
            Terms_ConditionsInfo Terms_Conditions = new Terms_ConditionsInfo();
            Terms_Conditions = dblogic.AddTermsAndConditions(tc);
            if (Terms_Conditions.NewTermsId > 0)
            {
                res.StatusCode = 200;
                res.Message = "Terms & Conditions Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Terms_Conditions = Terms_Conditions;
            return jsonconvert(res);
        }

        [Route("api/Project/recordUserTermsApproval")]
        [HttpPost]
        public HttpResponseMessage RecordUserTermsApproval(Terms_Conditions tc)
        {
            Terms_ConditionsResponceAccept res = new Terms_ConditionsResponceAccept();
            bool responce = dblogic.RecordUserTermsApproval(tc);
            if (responce)
            {
                res.IsAccepted = true;
                res.StatusCode = 200;
                res.Message = "Terms & Conditions Added Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/checkUserTermsStatus")]
        [HttpPost]
        public HttpResponseMessage CheckUserTermsStatus(Terms_Conditions tc)
        {
            Terms_ConditionsResponceAccept res = new Terms_ConditionsResponceAccept();
            bool responce = dblogic.CheckUserTermsStatus(tc);

            res.StatusCode = 200;
            res.Message = "Terms & Conditions check";
            res.IsAccepted = responce;
            return jsonconvert(res);
        }

        #endregion


        [Route("api/Project/GetProjectsMostViewedBasedOnRange")]
        [HttpPost]
        public HttpResponseMessage GetProjectsMostViewedBasedOnRange(Coordinates objCoord)
        {
            ProjectsResponce res = new ProjectsResponce();
            List<ProjectsMini> proj = new List<ProjectsMini>();
            proj = dblogic.GetProjectsListBasedOnRange(objCoord, true);
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "MostViewed Projects Details based on the given Range";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }


        [Route("api/Project/LikeProject")]
        [HttpPost]
        public HttpResponseMessage LikeProject(LikeProjects Proj)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            string responce = dblogic.LikeProject(Proj);

            if (responce == "200")
            {
                res.StatusCode = 200;
                if (Proj.isLiked)
                    res.Message = "Project Liked Successfully";
                else
                    res.Message = "Project un-Like Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        #region Organization
        [Route("api/Project/AddOrganization")]
        [HttpPost]
        public HttpResponseMessage AddOrganization(Organization organization)
        {
            OrganizationStaticResponse res = new OrganizationStaticResponse();

            res.Organization = dblogic.ManageOrganization(organization, "INSERT");

            if (res.Organization.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Organization Added Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        [Route("api/Project/UpdateOrganization")]
        [HttpPost]
        public HttpResponseMessage UpdateOrganization(Organization organization)
        {
            OrganizationStaticResponse res = new OrganizationStaticResponse();

            res.Organization = dblogic.ManageOrganization(organization, "UPDATE");

            if (res.Organization.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Organization Updated Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }

            return jsonconvert(res);
        }

        [Route("api/Project/GetOrganizations")]
        [HttpPost]
        public HttpResponseMessage GetOrganizations(Organization organization)
        {
            OrganizationStaticResponse res = new OrganizationStaticResponse();

            res.Organization = dblogic.ManageOrganization(organization, "GET");
            if (res.Organization.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Organizations Data";
            }
            else
            {
                res.StatusCode = 404;
                res.Message = "No Data found";
            }

            return jsonconvert(res);
        }


        [Route("api/Project/GetProjectbyOrgID")]
        [HttpPost]
        public HttpResponseMessage GetProjectbyOrgID(Projects proj)
        {
            OrgProjectResponce res = new OrgProjectResponce();

            res.Projects = dblogic.GetProjectbyOrgID(proj);
            if (res.Projects.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Projects Data";
            }
            else
            {
                res.StatusCode = 404;
                res.Message = "No Data found";
            }

            return jsonconvert(res);
        }
        #endregion


        [Route("api/Project/UpdatePlotPrice")]
        [HttpPost]
        public HttpResponseMessage UpdatePlotPrice(PlotPrice price)
        {
            bool isSuccess = false;

            isSuccess = dblogic.UpdatePlotPrice(price);

            if (isSuccess)
            {
                return jsonconvert(new { StatusCode = 200, Message = "Plot Price Updated" });
            }
            else
            {
                return jsonconvert(new { StatusCode = 500, Message = "Internal Server Error" });
            }
        }

        [Route("api/Project/DeleteProjectPlots")]
        [HttpPost]
        public HttpResponseMessage DeleteProjectPlots(ProjectDelete proj)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();

            string responce = dblogic.DeleteProjectPlots(proj);
            if (responce == "200")
            {
                res.StatusCode = 200;
                if (proj.PlotID == 0)
                    res.Message = "Project and Plots Deleted successfully.";
                else if (proj.PlotID == 1)
                    res.Message = "Plots Deleted successfully.";
                else
                    res.Message = "";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/GetUserLikedProjects")]
        [HttpPost]
        public HttpResponseMessage GetLikedProjectForUser(Users user)
        {
            ProjectsLikedResponce res = new ProjectsLikedResponce();
            List<LikedProjects> proj = new List<LikedProjects>();
            proj = dblogic.GetLikedProjectForUser(user.UserID);
            if (proj.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Liked Projects Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.Projects = proj;
            return jsonconvert(res);
        }

        [Route("api/Project/AssignOrganizationAdmin")]
        [HttpPost]
        public HttpResponseMessage AssignOrganizationAdmin(orgAdmin org)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            org.Type = "ADD";
            string responce = dblogic.CURD_AssignOrganizationAdmin(org);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Org Admin assigned Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/UnAssignOrganizationAdmin")]
        [HttpPost]
        public HttpResponseMessage UnAssignOrganizationAdmin(orgAdmin org)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            org.Type = "DELETE";
            string responce = dblogic.CURD_AssignOrganizationAdmin(org);
            if (responce == "200")
            {
                res.StatusCode = 200;
                res.Message = "Org Admin unassigned Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/GetAdminByOrg")]
        [Route("api/Project/GetOrgByAdmins")]
        [HttpPost]
        public HttpResponseMessage GetOrganizationAdmins(orgAdmin org)
        {
            OrgsAdminsInfoResponse res = new OrgsAdminsInfoResponse();
            res.Organization = dblogic.GetOrgsAdminsInfo(org);
            if (res.Organization.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Organization Admins Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/GetUserAllPlots")]
        [HttpPost]
        public HttpResponseMessage GetUserAllPlots(orgAdmin org)
        {
            PlotsResponce res = new PlotsResponce();
            List<Plots> plot = new List<Plots>();
            plot = dblogic.GetUserAllPlots(org);
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
        [Route("api/Project/GetPlotHistory")]
        [HttpPost]
        public HttpResponseMessage GetPlotHistory(PlotsUnAssigned plot)
        {
            PlotsHistoryResponce res = new PlotsHistoryResponce();
            List<PlotsHistory> plots = new List<PlotsHistory>();
            plots = dblogic.GetPlotHistory(plot);
            if (plots.Count > 0)
            {
                res.StatusCode = 200;
                res.Message = "Plot History Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            res.PlotsHistory = plots;
            return jsonconvert(res);
        }

        [Route("api/Project/GetProjectStats")]
        [HttpPost]
        public HttpResponseMessage GetProjectStats(ProjectDelete project)
        {
            ProjectStatsResponce res = new ProjectStatsResponce();
            res.ProjectStats = dblogic.GetProjectStats(project);
            if (res.ProjectStats != null)
            {
                res.StatusCode = 200;
                res.Message = "Project Stats Details";
            }
            else
            {
                res.StatusCode = 204;
                res.Message = "No Data Found";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/AssignProjectsToUsers")]
        [HttpPost]
        public HttpResponseMessage AssignProjectsToUsers(assignProjectsToUsers userProject)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            bool responce = dblogic.AssignAndUnAssignProjectsToUsers(userProject, "assigned");
            if (responce)
            {
                res.StatusCode = 200;
                res.Message = "Projects assigned to user Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }
        [Route("api/Project/UnAssignProjectsToUsers")]
        [HttpPost]
        public HttpResponseMessage UnAssignProjectsToUsers(assignProjectsToUsers userProject)
        {
            ProjectStaticResponce res = new ProjectStaticResponce();
            bool responce = dblogic.AssignAndUnAssignProjectsToUsers(userProject, "unassigned");
            if (responce)
            {
                res.StatusCode = 200;
                res.Message = "Projects unassigned to user Successfully";
            }
            else
            {
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return jsonconvert(res);
        }

        [Route("api/Project/GetProjectsbyAgentID")]
        [HttpPost]
        public HttpResponseMessage GetProjectsbyAgentID(ProjectDelete project)
        {
            ProjectsResponce res = new ProjectsResponce();
            List<ProjectsMini> proj = new List<ProjectsMini>();
            proj = dblogic.GetProjectsbyAgentID(project);
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
    }
}