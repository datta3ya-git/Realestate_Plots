using Newtonsoft.Json;
using RE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RE.BusinesLogic
{
    public class DBLogic
    {
        SqlConnection sqlCon = null;
        String SqlconString = ConfigurationManager.ConnectionStrings["REentity"].ConnectionString;
        string RoutePath = ConfigurationManager.AppSettings["RoutePath"];
        string SMS_OTP_Message = ConfigurationManager.AppSettings["SMS_OTP_Message"];
        bool SMS_OTP_Stop = Convert.ToBoolean(ConfigurationManager.AppSettings["SMS_OTP_Stop"]);
        public string AddUsers(Users user)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_User", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = user.Name;
                    sql_cmnd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = user.Email;
                    sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = user.Mobile;
                    sql_cmnd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = user.Password;
                    sql_cmnd.Parameters.AddWithValue("@Role", SqlDbType.Int).Value = user.Role;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.VarChar).Value = user.ProjectID == null ? "" : user.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = (int)Enums.Enums.ActionTypes.Add;
                    sql_cmnd.Parameters.AddWithValue("@IdProofNo", SqlDbType.VarChar).Value = user.IdProofNo == null ? "" : user.IdProofNo;
                    sql_cmnd.Parameters.AddWithValue("@IdProofType", SqlDbType.VarChar).Value = user.IdProofType == null ? "" : user.IdProofType;
                    sql_cmnd.Parameters.AddWithValue("@IsPaid", SqlDbType.VarChar).Value = user.IsPaid;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string DeactivateUser(Users user)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_DeactivateUser", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = user.UserID;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }
        public string AddProjects(Projects project)
        {
            string responce = string.Empty;
            try
            {
                string Geoinfo = "";
                Geoinfo = JsonConvert.SerializeObject(project.GEOInfo);
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_Project", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = project.Name == null ? "" : project.Name;
                    sql_cmnd.Parameters.AddWithValue("@Address", SqlDbType.VarChar).Value = project.Address == null ? "" : project.Address;
                    sql_cmnd.Parameters.AddWithValue("@District", SqlDbType.VarChar).Value = project.District == null ? "" : project.District;
                    sql_cmnd.Parameters.AddWithValue("@State", SqlDbType.VarChar).Value = project.State == null ? "" : project.State;
                    sql_cmnd.Parameters.AddWithValue("@PostalCode", SqlDbType.VarChar).Value = project.PostalCode == null ? "" : project.PostalCode;
                    sql_cmnd.Parameters.AddWithValue("@Landmark", SqlDbType.VarChar).Value = project.Landmark == null ? "" : project.Landmark;
                    sql_cmnd.Parameters.AddWithValue("@ContactPerson1", SqlDbType.VarChar).Value = project.ContactPerson1 == null ? "" : project.ContactPerson1;
                    sql_cmnd.Parameters.AddWithValue("@ConctactPerson2", SqlDbType.VarChar).Value = project.ContactPerson2 == null ? "" : project.ContactPerson2;
                    sql_cmnd.Parameters.AddWithValue("@Person1Mobile1", SqlDbType.VarChar).Value = project.Person1Mobile1 == null ? "" : project.Person1Mobile1;
                    sql_cmnd.Parameters.AddWithValue("@Person1Mobile2", SqlDbType.VarChar).Value = project.Person1Mobile2 == null ? "" : project.Person1Mobile2;
                    sql_cmnd.Parameters.AddWithValue("@Person2Mobile1", SqlDbType.VarChar).Value = project.Person2Mobile1 == null ? "" : project.Person2Mobile1;
                    sql_cmnd.Parameters.AddWithValue("@Person2Mobile2", SqlDbType.VarChar).Value = project.Person2Mobile2 == null ? "" : project.Person2Mobile2;
                    sql_cmnd.Parameters.AddWithValue("@Emails", SqlDbType.VarChar).Value = project.Emails == null ? "" : project.Emails;
                    sql_cmnd.Parameters.AddWithValue("@Description", SqlDbType.VarChar).Value = project.Description == null ? "" : project.Description;
                    sql_cmnd.Parameters.AddWithValue("@Photos", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(project.Photos);
                    sql_cmnd.Parameters.AddWithValue("@Documents", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(project.Documents);
                    sql_cmnd.Parameters.AddWithValue("@Brocher", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(project.Brocher);
                    sql_cmnd.Parameters.AddWithValue("@GEOInfo", SqlDbType.VarChar).Value = Geoinfo;
                    sql_cmnd.Parameters.AddWithValue("@Amenities", SqlDbType.VarChar).Value = project.Amenities == null ? "" : project.Amenities;
                    sql_cmnd.Parameters.AddWithValue("@Phase", SqlDbType.VarChar).Value = project.Phase == null ? "" : project.Phase;
                    sql_cmnd.Parameters.AddWithValue("@Blocks", SqlDbType.VarChar).Value = project.Blocks == null ? "" : project.Blocks;
                    sql_cmnd.Parameters.AddWithValue("@Faces", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(project.Faces);
                    sql_cmnd.Parameters.AddWithValue("@Naksha", SqlDbType.VarChar).Value = project.Naksha == null ? "" : project.Naksha;
                    sql_cmnd.Parameters.AddWithValue("@RoadsInfo", SqlDbType.VarChar).Value = project.RoadsInfo == null ? "" : project.RoadsInfo;
                    sql_cmnd.Parameters.AddWithValue("@NearByFeatures", SqlDbType.VarChar).Value = project.NearByFeatures == null ? "" : project.NearByFeatures;
                    sql_cmnd.Parameters.AddWithValue("@Directions", SqlDbType.VarChar).Value = project.Directions == null ? "" : project.Directions;
                    sql_cmnd.Parameters.AddWithValue("@Disclamier", SqlDbType.VarChar).Value = project.Disclamier == null ? "" : project.Disclamier;
                    sql_cmnd.Parameters.AddWithValue("@TotalArea", SqlDbType.VarChar).Value = project.TotalArea == null ? "" : project.TotalArea;
                    sql_cmnd.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = project.Type == null ? "" : project.Type;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = project.UserID == 0 ? 1 : project.UserID;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.VarChar).Value = project.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@Borders", SqlDbType.VarChar).Value = project.Borders == null ? "" : JsonConvert.SerializeObject(project.Borders);
                    sql_cmnd.Parameters.AddWithValue("@RoadNumber", SqlDbType.VarChar).Value = project.RoadNumber == null || project.RoadNumber == "" ? "" : project.RoadNumber;
                    sql_cmnd.Parameters.AddWithValue("@ModeType", SqlDbType.Int).Value = project.ProjectID == 0 ? (int)Enums.Enums.ActionTypes.Add : (int)Enums.Enums.ActionTypes.Modify;
                    sql_cmnd.Parameters.AddWithValue("@ventureLocation", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(project.ventureLocation);
                    sql_cmnd.Parameters.AddWithValue("@SurveyNumber", SqlDbType.VarChar).Value = project.SurveyNumber == null ? "" : project.SurveyNumber;
                    sql_cmnd.Parameters.AddWithValue("@AuthoritiesInfo", SqlDbType.VarChar).Value = project.AuthoritiesInfo == null ? "" : project.AuthoritiesInfo;
                    sql_cmnd.Parameters.AddWithValue("@SQYDPrice", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(project.sqydPrice);
                    sql_cmnd.Parameters.AddWithValue("@ProjectHighlights", SqlDbType.VarChar).Value = project.ProjectHighlights == null ? "" : project.ProjectHighlights;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    responce = "200";

                    if (project.ProjectID == 0)
                    {
                        FCMNotification objFCM = new FCMNotification();
                        bool isSuccess = objFCM.PushNotification(ConfigurationManager.AppSettings["FCM_Project_Title"], ConfigurationManager.AppSettings["FCM_Project_Body"]);
                    }
                }
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<ProjectsMini> GetProject(Projects proj)
        {
            List<ProjectsMini> projects = new List<ProjectsMini>();
            var ds = new DataSet();
            try
            {
                int projID = 0;
                if (proj != null && !string.IsNullOrWhiteSpace(proj.ProjectID.ToString()))
                {
                    projID = proj.ProjectID;
                }
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = projID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = proj.UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ProjectsMini objProj = new ProjectsMini();
                            objProj.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objProj.Address = dr["Address"] != null ? dr["Address"].ToString() : "";
                            objProj.District = dr["District"] != null ? dr["District"].ToString() : "";
                            objProj.State = dr["State"] != null ? dr["State"].ToString() : "";
                            objProj.PostalCode = dr["PostalCode"] != null ? dr["PostalCode"].ToString() : "";
                            objProj.Landmark = dr["Landmark"] != null ? dr["Landmark"].ToString() : "";
                            if (dr["Photos"] != null && dr["Photos"].ToString() != "")
                                objProj.Photos = JsonConvert.DeserializeObject<List<files>>(dr["Photos"] != null ? dr["Photos"].ToString() : "");


                            objProj.Phase = dr["Phase"] != null ? dr["Phase"].ToString() : "";
                            objProj.Blocks = dr["Blocks"] != null ? dr["Blocks"].ToString() : "";

                            if (dr["Faces"] != null && dr["Faces"].ToString() != "")
                                objProj.Faces = JsonConvert.DeserializeObject<DirectionFaces>(dr["Faces"] != null ? dr["Faces"].ToString() : "");

                            objProj.RoadNumber = dr["RoadNumber"] != null ? dr["RoadNumber"].ToString() : "";

                            if (dr["Borders"] != null && dr["Borders"].ToString() != "")
                                objProj.Borders = JsonConvert.DeserializeObject<DirectionFaces>(dr["Borders"] != null ? dr["Borders"].ToString() : "");

                            objProj.ProjectID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 0;

                            if (dr["ventureLocation"] != null && dr["ventureLocation"].ToString() != "")
                                objProj.ventureLocation = JsonConvert.DeserializeObject<Geos>(dr["ventureLocation"] != null ? dr["ventureLocation"].ToString() : "");

                            objProj.CoverPhotoTitle = dr["CoverPhotoTitle"] != null ? dr["CoverPhotoTitle"].ToString() : "";
                            objProj.CoverPhoto = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? RoutePath + "/Images/" + dr["CoverPhoto"].ToString() : "";
                            objProj.CoverPhotoDecription = dr["CoverPhotoDecription"] != null ? dr["CoverPhotoDecription"].ToString() : "";
                            objProj.CoverPhotoName = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? dr["CoverPhoto"].ToString() : "";
                            objProj.PhotoID = Convert.ToInt32(dr["PhotoID"].ToString());

                            objProj.BrochurePhotoTitle = dr["BrochurePhotoTitle"] != null ? dr["BrochurePhotoTitle"].ToString() : "";
                            objProj.BrochurePhoto = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? RoutePath + "/Images/" + dr["BrochurePhoto"].ToString() : "";
                            objProj.BrochurePhotoDecription = dr["BrochurePhotoDecription"] != null ? dr["BrochurePhotoDecription"].ToString() : "";
                            objProj.BrochurePhotoName = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? dr["BrochurePhoto"].ToString() : "";

                            objProj.LogoPhotoTitle = dr["LogoPhotoTitle"] != null ? dr["LogoPhotoTitle"].ToString() : "";
                            objProj.LogoPhoto = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? RoutePath + "/Images/" + dr["LogoPhoto"].ToString() : "";
                            objProj.LogoPhotoDecription = dr["LogoPhotoDecription"] != null ? dr["LogoPhotoDecription"].ToString() : "";
                            objProj.LogoPhotoName = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? dr["LogoPhoto"].ToString() : "";

                            objProj.AuthoritiesInfo = dr["AuthoritiesInfo"] != null ? dr["AuthoritiesInfo"].ToString() : "";
                            if (dr["SQYDPrice"] != null && dr["SQYDPrice"].ToString() != "")
                                objProj.sqydPrice = JsonConvert.DeserializeObject<SqydPrice>(dr["SQYDPrice"] != null ? dr["SQYDPrice"].ToString() : "");
                            projects.Add(objProj);

                            objProj.ContactPerson1 = dr["ContactPerson1"] != null ? dr["ContactPerson1"].ToString() : "";
                            objProj.ContactPerson2 = dr["ConctactPerson2"] != null ? dr["ConctactPerson2"].ToString() : "";
                            objProj.Person1Mobile1 = dr["Person1Mobile1"] != null ? dr["Person1Mobile1"].ToString() : "";
                            objProj.Person1Mobile2 = dr["Person1Mobile2"] != null ? dr["Person1Mobile2"].ToString() : "";
                            objProj.Person2Mobile1 = dr["Person2Mobile1"] != null ? dr["Person2Mobile1"].ToString() : "";
                            objProj.Person2Mobile2 = dr["Person2Mobile2"] != null ? dr["Person2Mobile2"].ToString() : "";
                            objProj.ProjectHighlights = dr["ProjectHighlights"] != null ? dr["ProjectHighlights"].ToString() : "";
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return projects;
        }
        public List<Users> GetUsers(Users User)
        {
            List<Users> Users = new List<Users>();
            var ds = new DataSet();
            try
            {
                int UserID = 0;
                if (User != null && !string.IsNullOrWhiteSpace(User.UserID.ToString()))
                {
                    UserID = User.UserID;
                }
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetUsers", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Users objusers = new Users();
                            objusers.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objusers.Email = dr["Email"] != null ? dr["Email"].ToString() : "";
                            objusers.Mobile = dr["Mobile"] != null ? dr["Mobile"].ToString() : "";
                            objusers.Role = dr["RoleID"] != null ? Convert.ToInt32(dr["RoleID"].ToString()) : 1;
                            objusers.UserID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 1;
                            objusers.PIN = dr["PIN"] != null ? dr["PIN"].ToString() : "";
                            Users.Add(objusers);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return Users;
        }


        public Projects GetProjectbyID(Projects proj)
        {

            Projects objProj = new Projects();
            var ds = new DataSet();
            try
            {
                int projID = 0;
                if (proj != null && !string.IsNullOrWhiteSpace(proj.ProjectID.ToString()))
                {
                    projID = proj.ProjectID;
                }
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = projID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = proj.UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();
                    DataSet dsRoadsInfo = new DataSet();
                    dsRoadsInfo = getProjectRoadInfo(projID, proj.UserID);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            objProj.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objProj.Address = dr["Address"] != null ? dr["Address"].ToString() : "";
                            objProj.District = dr["District"] != null ? dr["District"].ToString() : "";
                            objProj.State = dr["State"] != null ? dr["State"].ToString() : "";
                            objProj.PostalCode = dr["PostalCode"] != null ? dr["PostalCode"].ToString() : "";
                            objProj.Landmark = dr["Landmark"] != null ? dr["Landmark"].ToString() : "";
                            objProj.ContactPerson1 = dr["ContactPerson1"] != null ? dr["ContactPerson1"].ToString() : "";
                            objProj.ContactPerson2 = dr["ConctactPerson2"] != null ? dr["ConctactPerson2"].ToString() : "";
                            objProj.Person1Mobile1 = dr["Person1Mobile1"] != null ? dr["Person1Mobile1"].ToString() : "";
                            objProj.Person1Mobile2 = dr["Person1Mobile2"] != null ? dr["Person1Mobile2"].ToString() : "";
                            objProj.Person2Mobile1 = dr["Person2Mobile1"] != null ? dr["Person2Mobile1"].ToString() : "";
                            objProj.Person2Mobile2 = dr["Person2Mobile2"] != null ? dr["Person2Mobile2"].ToString() : "";
                            objProj.ProjectHighlights = dr["ProjectHighlights"] != null ? dr["ProjectHighlights"].ToString() : "";
                            objProj.Emails = dr["Emails"] != null ? dr["Emails"].ToString() : "";
                            objProj.Description = dr["Description"] != null ? dr["Description"].ToString() : "";

                            if (dr["Photos"] != null && dr["Photos"].ToString() != "")
                                objProj.Photos = JsonConvert.DeserializeObject<List<files>>(dr["Photos"] != null ? dr["Photos"].ToString() : "");

                            if (dr["Documents"] != null && dr["Documents"].ToString() != "")
                                objProj.Documents = JsonConvert.DeserializeObject<List<files>>(dr["Documents"] != null ? dr["Documents"].ToString() : "");

                            if (dr["Brocher"] != null && dr["Brocher"].ToString() != "")
                                objProj.Brocher = JsonConvert.DeserializeObject<List<files>>(dr["Brocher"] != null ? dr["Brocher"].ToString() : "");

                            string json = dr["GEOInfo"] != null ? dr["GEOInfo"].ToString() : "";
                            objProj.GEOInfo = JsonConvert.DeserializeObject<List<Geos>>(json);

                            objProj.Amenities = dr["Amenities"] != null ? dr["Amenities"].ToString() : "";
                            objProj.Phase = dr["Phase"] != null ? dr["Phase"].ToString() : "";
                            objProj.Blocks = dr["Blocks"] != null ? dr["Blocks"].ToString() : "";

                            if (dr["Faces"] != null && dr["Faces"].ToString() != "")
                                objProj.Faces = JsonConvert.DeserializeObject<DirectionFaces>(dr["Faces"] != null ? dr["Faces"].ToString() : "");

                            objProj.Naksha = dr["Naksha"] != null ? dr["Naksha"].ToString() : "";
                            objProj.RoadsInfo = dr["RoadsInfo"] != null ? dr["RoadsInfo"].ToString() : "";
                            objProj.NearByFeatures = dr["NearByFeatures"] != null ? dr["NearByFeatures"].ToString() : "";
                            objProj.Directions = dr["Directions"] != null ? dr["Directions"].ToString() : "";
                            objProj.Disclamier = dr["Disclamier"] != null ? dr["Disclamier"].ToString() : "";
                            objProj.TotalArea = dr["TotalArea"] != null ? dr["TotalArea"].ToString() : "";
                            objProj.Type = dr["Type"] != null ? dr["Type"].ToString() : "";
                            objProj.ProjectID = dr["ID"] != null ? Convert.ToInt32(dr["ID"].ToString()) : 0;


                            objProj.RoadNumber = dr["RoadNumber"] != null ? dr["RoadNumber"].ToString() : "";

                            if (dr["Borders"] != null && dr["Borders"].ToString() != "")
                                objProj.Borders = JsonConvert.DeserializeObject<DirectionFaces>(dr["Borders"] != null ? dr["Borders"].ToString() : "");


                            if (dr["ventureLocation"] != null && dr["ventureLocation"].ToString() != "")
                                objProj.ventureLocation = JsonConvert.DeserializeObject<Geos>(dr["ventureLocation"] != null ? dr["ventureLocation"].ToString() : "");

                            objProj.SurveyNumber = dr["SurveyNumber"] != null ? dr["SurveyNumber"].ToString() : "";


                            objProj.CoverPhotoTitle = dr["CoverPhotoTitle"] != null ? dr["CoverPhotoTitle"].ToString() : "";
                            objProj.CoverPhoto = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? RoutePath + "/Images/" + dr["CoverPhoto"].ToString() : "";
                            objProj.CoverPhotoDecription = dr["CoverPhotoDecription"] != null ? dr["CoverPhotoDecription"].ToString() : "";
                            objProj.CoverPhotoName = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? dr["CoverPhoto"].ToString() : "";

                            objProj.BrochurePhotoTitle = dr["BrochurePhotoTitle"] != null ? dr["BrochurePhotoTitle"].ToString() : "";
                            objProj.BrochurePhoto = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? RoutePath + "/Images/" + dr["BrochurePhoto"].ToString() : "";
                            objProj.BrochurePhotoDecription = dr["BrochurePhotoDecription"] != null ? dr["BrochurePhotoDecription"].ToString() : "";
                            objProj.BrochurePhotoName = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? dr["BrochurePhoto"].ToString() : "";

                            objProj.LogoPhotoTitle = dr["LogoPhotoTitle"] != null ? dr["LogoPhotoTitle"].ToString() : "";
                            objProj.LogoPhoto = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? RoutePath + "/Images/" + dr["LogoPhoto"].ToString() : "";
                            objProj.LogoPhotoDecription = dr["LogoPhotoDecription"] != null ? dr["LogoPhotoDecription"].ToString() : "";
                            objProj.LogoPhotoName = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? dr["LogoPhoto"].ToString() : "";

                            objProj.AuthoritiesInfo = dr["AuthoritiesInfo"] != null ? dr["AuthoritiesInfo"].ToString() : "";
                            if (dr["SQYDPrice"] != null && dr["SQYDPrice"].ToString() != "")
                                objProj.sqydPrice = JsonConvert.DeserializeObject<SqydPrice>(dr["SQYDPrice"] != null ? dr["SQYDPrice"].ToString() : "");

                            List<ProjectsAttachments> ProjectPhotos = new List<ProjectsAttachments>();
                            if (ds.Tables.Count > 1)
                            {
                                DataRow[] drPhoto = ds.Tables[1].Select("ProjectID = " + objProj.ProjectID.ToString());
                                if (drPhoto.Length > 0)
                                {
                                    foreach (DataRow drp in drPhoto)
                                    {
                                        ProjectsAttachments projPhoto = new ProjectsAttachments();
                                        projPhoto.Photo = !string.IsNullOrEmpty(drp["Attachments"].ToString()) ? RoutePath + "/Images/" + drp["Attachments"].ToString() : "";
                                        projPhoto.PhotoName = !string.IsNullOrEmpty(drp["Attachments"].ToString()) ? drp["Attachments"].ToString() : "";
                                        projPhoto.PhotoTitle = drp["Title"] != null ? drp["Title"].ToString() : "";
                                        projPhoto.PhotoDecription = drp["Decription"] != null ? drp["Decription"].ToString() : "";
                                        projPhoto.IsCoverPhoto = drp["IsCoverPhoto"] != null ? Convert.ToInt32(drp["IsCoverPhoto"].ToString()) : 0;
                                        projPhoto.PhotoID = Convert.ToInt32(drp["ID"].ToString());
                                        ProjectPhotos.Add(projPhoto);
                                    }
                                }
                                objProj.ProjectPhotos = ProjectPhotos;
                            }

                            List<RoadsInfo> RoadsInfo = new List<RoadsInfo>();
                            if (dsRoadsInfo.Tables.Count > 1)
                            {

                                foreach (DataRow drI in dsRoadsInfo.Tables[0].Rows)
                                {

                                    int pID = Convert.ToInt32(drI["ProjectID"].ToString());
                                    string rID = drI["RoadNo"].ToString();

                                    RoadsInfo projRoadsInfo = new RoadsInfo();

                                    DataRow[] drRoadsInfo = dsRoadsInfo.Tables[1].Select("ProjectID = '" + pID.ToString() + "' AND RoadNo = '" + rID.ToString() + "'");

                                    projRoadsInfo.projectID = Convert.ToInt32(drRoadsInfo[0]["ProjectID"].ToString());
                                    projRoadsInfo.userID = Convert.ToInt32(drRoadsInfo[0]["CreatedBy"].ToString());
                                    projRoadsInfo.roadNo = drRoadsInfo[0]["RoadNo"] != null ? drRoadsInfo[0]["RoadNo"].ToString() : "";
                                    projRoadsInfo.roadWidth = drRoadsInfo[0]["RoadWidth"] != null ? drRoadsInfo[0]["RoadWidth"].ToString() : "";
                                    if (drRoadsInfo.Length > 0)
                                    {
                                        List<Geos> roadslagInfo = new List<Geos>();
                                        foreach (DataRow drp in drRoadsInfo)
                                        {
                                            string jsonR = drp["RoadGEOInfo"] != null ? drp["RoadGEOInfo"].ToString() : "";
                                            List<Geos> RGEOInfo = JsonConvert.DeserializeObject<List<Geos>>(jsonR);
                                            roadslagInfo.AddRange(RGEOInfo);

                                        }
                                        projRoadsInfo.RoadGEOInfo = roadslagInfo;
                                        RoadsInfo.Add(projRoadsInfo);
                                    }
                                }
                                objProj.AllRoadsInfo = RoadsInfo;
                            }
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return objProj;
        }

        public DataSet getProjectRoadInfo(int ProjID, int UserID)
        {
            var ds = new DataSet();
            try
            {

                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetProjectsRoadsInfo", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = ProjID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
                ds = new DataSet();
            }
            return ds;
        }

        public string AddPlots(Plots Plot)
        {
            int plotID = 0;
            if (Plot != null && !string.IsNullOrWhiteSpace(Plot.PlotID.ToString()))
            {
                plotID = Plot.PlotID;
            }
            string Geoinfo = "";
            Geoinfo = JsonConvert.SerializeObject(Plot.GEOInfo);
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_Plot", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.Int).Value = plotID;
                    sql_cmnd.Parameters.AddWithValue("@PlotNo", SqlDbType.VarChar).Value = Plot.PlotNo;
                    sql_cmnd.Parameters.AddWithValue("@Facings", SqlDbType.VarChar).Value = Plot.Facings;
                    sql_cmnd.Parameters.AddWithValue("@PlotSize", SqlDbType.VarChar).Value = Plot.PlotSize;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = Plot.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@UserId", SqlDbType.VarChar).Value = Plot.UserID;
                    sql_cmnd.Parameters.AddWithValue("@RoadsInfo", SqlDbType.Int).Value = JsonConvert.SerializeObject(Plot.RoadsInfo);
                    sql_cmnd.Parameters.AddWithValue("@PlotDocuments", SqlDbType.Int).Value = JsonConvert.SerializeObject(Plot.PlotDocuments);
                    sql_cmnd.Parameters.AddWithValue("@ModeType", SqlDbType.Int).Value = plotID == 0 ? (int)Enums.Enums.ActionTypes.Add : (int)Enums.Enums.ActionTypes.Modify;
                    sql_cmnd.Parameters.AddWithValue("@GEOInfo", SqlDbType.NVarChar).Value = Geoinfo;
                    sql_cmnd.Parameters.AddWithValue("@Borders", SqlDbType.VarChar).Value = Plot.Borders == null ? "" : JsonConvert.SerializeObject(Plot.Borders);
                    sql_cmnd.Parameters.AddWithValue("@RoadNumber", SqlDbType.VarChar).Value = Plot.RoadNumber == null || Plot.RoadNumber == "" ? "" : Plot.RoadNumber;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<Plots> GetPlots(Plots Plot)
        {
            List<Plots> plots = new List<Plots>();
            var ds = new DataSet();
            try
            {
                int plotID = 0;
                if (Plot != null && !string.IsNullOrWhiteSpace(Plot.PlotID.ToString()))
                {
                    plotID = Plot.PlotID;
                }
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetPlots", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = Plot.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.Int).Value = plotID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = Plot.UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Plots objPlots = new Plots();
                            objPlots.PlotID = dr["ID"] != null ? Convert.ToInt32(dr["ID"].ToString()) : 0;
                            objPlots.PlotNo = dr["PlotNo"] != null ? dr["PlotNo"].ToString() : "";
                            objPlots.Facings = dr["Facings"] != null ? dr["Facings"].ToString() : "";
                            objPlots.PlotSize = dr["PlotSize"] != null ? dr["PlotSize"].ToString() : "";
                            objPlots.ProjectID = dr["ProjectID"] != null ? Convert.ToInt32(dr["ProjectID"].ToString()) : 0;
                            objPlots.UserID = dr["UserID"] != null ? Convert.ToInt32(dr["UserID"].ToString()) : 0;

                            if (dr["PlotDocuments"] != null && dr["PlotDocuments"].ToString() != "")
                                objPlots.PlotDocuments = JsonConvert.DeserializeObject<List<files>>(dr["PlotDocuments"] != null ? dr["PlotDocuments"].ToString() : "");

                            if (dr["RoadsInfo"] != null && dr["RoadsInfo"].ToString() != "")
                                objPlots.RoadsInfo = JsonConvert.DeserializeObject<DirectionFaces>(dr["RoadsInfo"] != null ? dr["RoadsInfo"].ToString() : "");

                            string json = dr["GEOInfo"] != null ? dr["GEOInfo"].ToString() : "";
                            objPlots.GEOInfo = JsonConvert.DeserializeObject<List<Geos>>(json);
                            objPlots.IsSold = dr["isSold"] != null ? Convert.ToInt32(dr["isSold"].ToString()) : 0;


                            objPlots.RoadNumber = dr["RoadNumber"] != null ? dr["RoadNumber"].ToString() : "";

                            if (dr["Borders"] != null && dr["Borders"].ToString() != "")
                                objPlots.Borders = JsonConvert.DeserializeObject<DirectionFaces>(dr["Borders"] != null ? dr["Borders"].ToString() : "");

                            if (dr["isApproved"] != null && dr["isApproved"].ToString() != "")
                                objPlots.IsApproved = Convert.ToBoolean(dr["isApproved"]);

                            objPlots.ProjName = dr["ProjName"] != null ? dr["ProjName"].ToString() : "";
                            objPlots.ProjAddress = dr["ProjAddress"] != null ? dr["ProjAddress"].ToString() : "";
                            objPlots.ProjDistrict = dr["ProjDistrict"] != null ? dr["ProjDistrict"].ToString() : "";
                            objPlots.ProjState = dr["ProjState"] != null ? dr["ProjState"].ToString() : "";
                            objPlots.ProjPostalCode = dr["ProjPostalCode"] != null ? dr["ProjPostalCode"].ToString() : "";
                            objPlots.ProjLandmark = dr["ProjLandmark"] != null ? dr["ProjLandmark"].ToString() : "";

                            objPlots.CoverPhotoTitle = dr["CoverPhotoTitle"] != null ? dr["CoverPhotoTitle"].ToString() : "";
                            objPlots.CoverPhoto = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? RoutePath + "/Images/" + dr["CoverPhoto"].ToString() : "";
                            objPlots.CoverPhotoDecription = dr["CoverPhotoDecription"] != null ? dr["CoverPhotoDecription"].ToString() : "";
                            objPlots.PhotoID = Convert.ToInt32(dr["PhotoID"].ToString());

                            plots.Add(objPlots);
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return plots;
        }

        public string AssignProjectsandPlots(ProjectAssign proj)
        {

            string responce = string.Empty;
            try
            {
                if (proj != null && proj.PlotID != null && proj.PlotID.Count > 0)
                {
                    foreach (int plot in proj.PlotID)
                    {
                        int plotID = 0;
                        if (proj != null && !string.IsNullOrWhiteSpace(plot.ToString()))
                        {
                            plotID = plot;
                        }
                        using (sqlCon = new SqlConnection(SqlconString))
                        {
                            sqlCon.Open();
                            SqlCommand sql_cmnd = new SqlCommand("sp_AssignProjectandPlot", sqlCon);
                            sql_cmnd.CommandType = CommandType.StoredProcedure;
                            sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = proj.ProjectID;
                            sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.Int).Value = plotID;
                            sql_cmnd.Parameters.AddWithValue("@AssignedUserID", SqlDbType.Int).Value = proj.AssignedUserID;
                            sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = proj.UserID;
                            sql_cmnd.ExecuteNonQuery();
                            sqlCon.Close();
                        }
                    }
                }
                else
                {
                    using (sqlCon = new SqlConnection(SqlconString))
                    {
                        int plotID = 0;
                        sqlCon.Open();
                        SqlCommand sql_cmnd = new SqlCommand("sp_AssignProjectandPlot", sqlCon);
                        sql_cmnd.CommandType = CommandType.StoredProcedure;
                        sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = proj.ProjectID;
                        sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.Int).Value = plotID;
                        sql_cmnd.Parameters.AddWithValue("@AssignedUserID", SqlDbType.Int).Value = proj.AssignedUserID;
                        sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = proj.UserID;
                        sql_cmnd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<Role> GetRoles()
        {
            List<Role> Role = new List<Role>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_Roles", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Role objRole = new Role();
                            objRole.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objRole.ID = dr["ID"] != null ? Convert.ToInt32(dr["ID"].ToString()) : 1;

                            Role.Add(objRole);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return Role;
        }


        public List<Users> GetEndUsers(Users User)
        {
            List<Users> Users = new List<Users>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetEndUser", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = User.Mobile;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Users objusers = new Users();
                            objusers.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objusers.Email = dr["Email"] != null ? dr["Email"].ToString() : "";
                            objusers.Mobile = dr["Mobile"] != null ? dr["Mobile"].ToString() : "";
                            objusers.Role = dr["RoleID"] != null ? Convert.ToInt32(dr["RoleID"].ToString()) : 1;
                            objusers.UserID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 1;
                            objusers.IdProofNo = dr["IdProofNo"] != null ? dr["IdProofNo"].ToString() : "";
                            objusers.IdProofType = dr["IdProofType"] != null ? dr["IdProofType"].ToString() : "";
                            objusers.IsPaid = dr["IsPaid"] != null && dr["IsPaid"].ToString() != "" ? Convert.ToBoolean(dr["IsPaid"].ToString()) : false;
                            objusers.PIN = dr["PIN"] != null ? dr["PIN"].ToString() : "";
                            Users.Add(objusers);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return Users;
        }

        public List<Users> GetUsersByMobile(Users User)
        {
            List<Users> Users = new List<Users>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetUsersByMobile", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = User.Mobile;
                    sql_cmnd.Parameters.AddWithValue("@Role", SqlDbType.Int).Value = User.Role;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Users objusers = new Users();
                            objusers.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objusers.Email = dr["Email"] != null ? dr["Email"].ToString() : "";
                            objusers.Mobile = dr["Mobile"] != null ? dr["Mobile"].ToString() : "";
                            objusers.Role = dr["RoleID"] != null ? Convert.ToInt32(dr["RoleID"].ToString()) : 1;
                            objusers.UserID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 1;
                            objusers.IdProofNo = dr["IdProofNo"] != null ? dr["IdProofNo"].ToString() : "";
                            objusers.IdProofType = dr["IdProofType"] != null ? dr["IdProofType"].ToString() : "";
                            objusers.IsPaid = dr["IsPaid"] != null && dr["IsPaid"].ToString() != "" ? Convert.ToBoolean(dr["IsPaid"].ToString()) : false;
                            objusers.VentureIDs = dr["VentureIDs"] != null ? dr["VentureIDs"].ToString() : "";
                            List<string> AssignedVentureIDs = new List<string>();
                            foreach (var item in objusers.VentureIDs.Split(','))
                            {
                                AssignedVentureIDs.Add(item);
                            }
                            objusers.AssignedVentureIDs = AssignedVentureIDs;
                            objusers.PIN = dr["PIN"] != null ? dr["PIN"].ToString() : "";
                            Users.Add(objusers);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return Users;
        }

        public string PlotSold(Plots Plot)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_PlotSold", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = Plot.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.Int).Value = Plot.PlotID;
                    sql_cmnd.Parameters.AddWithValue("@IsSold", SqlDbType.Bit).Value = Plot.IsSold;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = Plot.UserID;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }


        public checkUsersStatus CreateUsers(Users user)
        {
            checkUsersStatus res = new checkUsersStatus();
            string responce = string.Empty;
            try
            {
                string venIDs = "";
                string ExistVenIDs = "";
                DataSet ds = VerifyUser(user);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (var item in user.VentureIDs.Split(','))
                    {
                        DataRow[] dr = ds.Tables[0].Select("VentureIDs = " + item.ToString());
                        if (dr.Length > 0)
                        {
                            if (ExistVenIDs == "")
                                ExistVenIDs = item;
                            else
                                ExistVenIDs = ExistVenIDs + "," + item;
                        }
                        else
                        {
                            if (venIDs == "")
                                venIDs = item;
                            else
                                venIDs = venIDs + "," + item;
                        }
                    }
                }
                else
                {
                    venIDs = user.VentureIDs;
                }
                res.Exists = ExistVenIDs;
                res.NotExists = venIDs;
                if (venIDs != "")
                {
                    using (sqlCon = new SqlConnection(SqlconString))
                    {
                        sqlCon.Open();
                        SqlCommand sql_cmnd = new SqlCommand("SP_CreateUsers", sqlCon);
                        sql_cmnd.CommandType = CommandType.StoredProcedure;
                        sql_cmnd.Parameters.AddWithValue("@CreatedBy", SqlDbType.Int).Value = user.UserID;
                        sql_cmnd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = user.Name;
                        sql_cmnd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = user.Email == null ? "" : user.Email;
                        sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = user.Mobile;
                        sql_cmnd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = user.Password == null ? "" : user.Password;
                        sql_cmnd.Parameters.AddWithValue("@Role", SqlDbType.Int).Value = user.Role;
                        sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.VarChar).Value = user.ProjectID == null ? "" : user.ProjectID;
                        sql_cmnd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = (int)Enums.Enums.ActionTypes.Add;
                        sql_cmnd.Parameters.AddWithValue("@IdProofNo", SqlDbType.VarChar).Value = user.IdProofNo == null ? "" : user.IdProofNo;
                        sql_cmnd.Parameters.AddWithValue("@IdProofType", SqlDbType.VarChar).Value = user.IdProofType == null ? "" : user.IdProofType;
                        sql_cmnd.Parameters.AddWithValue("@IsPaid", SqlDbType.VarChar).Value = user.IsPaid;
                        sql_cmnd.Parameters.AddWithValue("@VentureID", SqlDbType.VarChar).Value = venIDs;
                        sql_cmnd.ExecuteNonQuery();
                        sqlCon.Close();
                    }

                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return res;
        }

        public DataSet VerifyUser(Users User)
        {
            //bool isExists = false;
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_VerifyUser", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = User.Mobile;
                    sql_cmnd.Parameters.AddWithValue("@Role", SqlDbType.VarChar).Value = User.Role;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.VarChar).Value = User.VentureIDs;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();


                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return ds;
        }

        public List<Users> GetUsersHierarchy(Users User)
        {
            List<Users> Users = new List<Users>();
            var ds = new DataSet();
            try
            {
                int UserID = 0;
                if (User != null && !string.IsNullOrWhiteSpace(User.UserID.ToString()))
                {
                    UserID = User.UserID;
                }
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetUsersHierarchy", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = UserID;
                    sql_cmnd.Parameters.AddWithValue("@VentureID", SqlDbType.Int).Value = User.VentureID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Users objusers = new Users();
                            objusers.Name = dr["UserName"] != null ? dr["UserName"].ToString() : "";
                            objusers.Email = dr["UserEmail"] != null ? dr["UserEmail"].ToString() : "";
                            objusers.Mobile = dr["UserMobile"] != null ? dr["UserMobile"].ToString() : "";
                            objusers.Role = dr["RoleID"] != null ? Convert.ToInt32(dr["RoleID"].ToString()) : 1;
                            objusers.UserID = dr["UserID"] != null ? Convert.ToInt32(dr["UserID"].ToString()) : 1;

                            objusers.RoleName = dr["RoleName"] != null ? dr["RoleName"].ToString() : "";
                            objusers.IdProofNo = dr["IdProofNo"] != null ? dr["IdProofNo"].ToString() : "";
                            objusers.IdProofType = dr["IdProofType"] != null ? dr["IdProofType"].ToString() : "";
                            objusers.VentureName = dr["ProjectName"] != null ? dr["ProjectName"].ToString() : "";
                            objusers.VentureAddress = dr["ProjectAddress"] != null ? dr["ProjectAddress"].ToString() : "";
                            objusers.VentureID = dr["VentureID"] != null ? Convert.ToInt32(dr["VentureID"].ToString()) : 1;
                            objusers.ProjectID = objusers.VentureID.ToString();
                            objusers.IsPaid = dr["IsPaid"] != null ? Convert.ToBoolean(dr["IsPaid"].ToString()) : false;
                            objusers.Level = dr["Level"] != null ? Convert.ToInt32(dr["Level"].ToString()) : 0;

                            Users.Add(objusers);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return Users;
        }

        public List<PlotsWithUser> GetPlotsforApprovel(Projects User)
        {
            List<PlotsWithUser> plots = new List<PlotsWithUser>();
            var ds = new DataSet();
            try
            {
                int UserID = 0;
                if (User != null && !string.IsNullOrWhiteSpace(User.UserID.ToString()))
                {
                    UserID = User.UserID;
                }
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("GetPlotsforApprovel", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = User.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@Userid", SqlDbType.Int).Value = UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PlotsWithUser objplots = new PlotsWithUser();
                            objplots.CustomerID = dr["CustomerID"] != null ? Convert.ToInt32(dr["CustomerID"].ToString()) : 0;
                            objplots.ProjectID = dr["ProjectID"] != null ? Convert.ToInt32(dr["ProjectID"].ToString()) : 0;
                            objplots.PlotID = dr["PlotID"] != null ? Convert.ToInt32(dr["PlotID"].ToString()) : 0;
                            objplots.AgentID = dr["AgentID"] != null ? Convert.ToInt32(dr["AgentID"].ToString()) : 0;
                            objplots.ProjectName = dr["ProjectName"] != null ? dr["ProjectName"].ToString() : "";
                            objplots.CustomerName = dr["CustomerName"] != null ? dr["CustomerName"].ToString() : "";
                            objplots.AgentName = dr["AgentName"] != null ? dr["AgentName"].ToString() : "";
                            objplots.CustomerMobile = dr["CustomerMobile"] != null ? dr["CustomerMobile"].ToString() : "";
                            objplots.AgentMobile = dr["AgentMobile"] != null ? dr["AgentMobile"].ToString() : "";

                            Plots objPlotsInfo = new Plots();

                            objPlotsInfo.PlotID = dr["PlotID"] != null ? Convert.ToInt32(dr["PlotID"].ToString()) : 0;
                            objPlotsInfo.PlotNo = dr["PlotNo"] != null ? dr["PlotNo"].ToString() : "";
                            objPlotsInfo.Facings = dr["Facings"] != null ? dr["Facings"].ToString() : "";
                            objPlotsInfo.PlotSize = dr["PlotSize"] != null ? dr["PlotSize"].ToString() : "";
                            if (dr["PlotDocuments"] != null && dr["PlotDocuments"].ToString() != "")
                                objPlotsInfo.PlotDocuments = JsonConvert.DeserializeObject<List<files>>(dr["PlotDocuments"] != null ? dr["PlotDocuments"].ToString() : "");

                            if (dr["RoadsInfo"] != null && dr["RoadsInfo"].ToString() != "")
                                objPlotsInfo.RoadsInfo = JsonConvert.DeserializeObject<DirectionFaces>(dr["RoadsInfo"] != null ? dr["RoadsInfo"].ToString() : "");

                            string json = dr["GEOInfo"] != null ? dr["GEOInfo"].ToString() : "";
                            objPlotsInfo.GEOInfo = JsonConvert.DeserializeObject<List<Geos>>(json);
                            objPlotsInfo.IsSold = dr["isSold"] != null ? Convert.ToInt32(dr["isSold"].ToString()) : 0;


                            objPlotsInfo.RoadNumber = dr["RoadNumber"] != null ? dr["RoadNumber"].ToString() : "";

                            if (dr["Borders"] != null && dr["Borders"].ToString() != "")
                                objPlotsInfo.Borders = JsonConvert.DeserializeObject<DirectionFaces>(dr["Borders"] != null ? dr["Borders"].ToString() : "");


                            objplots.PlotInfo = objPlotsInfo;
                            plots.Add(objplots);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return plots;
        }


        public string AssignPlotsApprove(PlotsApprove Plot)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_AssignPlotsApprove", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = Plot.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.VarChar).Value = string.Join<int>(",", Plot.PlotID);
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = Plot.UserID;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<Agent> GetPendingAgents(Users User)
        {
            List<Agent> Agent = new List<Agent>();
            var ds = new DataSet();
            try
            {

                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetPendingAgents", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@VentureID", SqlDbType.Int).Value = User.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = User.UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Agent objAgent = new Agent();
                            objAgent.AgentName = dr["AgentName"] != null ? dr["AgentName"].ToString() : "";
                            objAgent.AgentEmail = dr["AgentEmail"] != null ? dr["AgentEmail"].ToString() : "";
                            objAgent.AgentMobile = dr["AgentMobile"] != null ? dr["AgentMobile"].ToString() : "";
                            objAgent.CreatedByName = dr["CreatedByName"] != null ? dr["CreatedByName"].ToString() : "";
                            objAgent.CreatedByEmail = dr["CreatedByEmail"] != null ? dr["CreatedByEmail"].ToString() : "";
                            objAgent.CreatedByMobile = dr["CreatedByMobile"] != null ? dr["CreatedByMobile"].ToString() : "";
                            objAgent.CreatedByID = dr["CreatedByID"] != null ? Convert.ToInt32(dr["CreatedByID"].ToString()) : 0;
                            objAgent.AgentID = dr["AgentID"] != null ? Convert.ToInt32(dr["AgentID"].ToString()) : 0;

                            Agent.Add(objAgent);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return Agent;
        }

        public string ApproveAgents(List<AgentApprove> Agents)
        {
            string responce = string.Empty;
            try
            {
                foreach (var agent in Agents)
                {
                    using (sqlCon = new SqlConnection(SqlconString))
                    {
                        sqlCon.Open();
                        SqlCommand sql_cmnd = new SqlCommand("sp_ApproveAgent", sqlCon);
                        sql_cmnd.CommandType = CommandType.StoredProcedure;
                        sql_cmnd.Parameters.AddWithValue("@AgentID", SqlDbType.Int).Value = agent.AgentID;
                        sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = agent.ProjectID;
                        sql_cmnd.Parameters.AddWithValue("@ApprovedBy", SqlDbType.Int).Value = agent.ApprovedBy;

                        sql_cmnd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<Users> GetAgents(ProjectAssign proj)
        {
            List<Users> Users = new List<Users>();
            var ds = new DataSet();
            try
            {
                int ProjectID = 0;
                if (proj != null && !string.IsNullOrWhiteSpace(proj.ProjectID.ToString()))
                {
                    ProjectID = proj.ProjectID;
                }
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetAgents", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = ProjectID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Users objusers = new Users();
                            objusers.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objusers.Email = dr["Email"] != null ? dr["Email"].ToString() : "";
                            objusers.Mobile = dr["Mobile"] != null ? dr["Mobile"].ToString() : "";
                            objusers.Role = dr["RoleID"] != null ? Convert.ToInt32(dr["RoleID"].ToString()) : 1;
                            objusers.UserID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 1;
                            objusers.isApproved = dr["isApproved"] != null ? Convert.ToBoolean(dr["isApproved"].ToString()) : false;
                            objusers.PIN = dr["PIN"] != null ? dr["PIN"].ToString() : "";
                            Users.Add(objusers);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return Users;
        }

        public string SaveLikeProjects(LikeProjects Proj)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_SaveLikeProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = Proj.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.Int).Value = Proj.PlotID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = Proj.UserID;
                    sql_cmnd.Parameters.AddWithValue("@Like", SqlDbType.Bit).Value = Proj.isLiked;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<ProjectsMini> GetLikedProject(LikeProjects proj)
        {
            List<ProjectsMini> projects = new List<ProjectsMini>();
            var ds = new DataSet();
            try
            {

                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_GetLikedProjectsOrPlots", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = proj.UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ProjectsMini objProj = new ProjectsMini();
                            objProj.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objProj.Address = dr["Address"] != null ? dr["Address"].ToString() : "";
                            objProj.District = dr["District"] != null ? dr["District"].ToString() : "";
                            objProj.State = dr["State"] != null ? dr["State"].ToString() : "";
                            objProj.PostalCode = dr["PostalCode"] != null ? dr["PostalCode"].ToString() : "";
                            objProj.Landmark = dr["Landmark"] != null ? dr["Landmark"].ToString() : "";
                            if (dr["Photos"] != null && dr["Photos"].ToString() != "")
                                objProj.Photos = JsonConvert.DeserializeObject<List<files>>(dr["Photos"] != null ? dr["Photos"].ToString() : "");


                            objProj.Phase = dr["Phase"] != null ? dr["Phase"].ToString() : "";
                            objProj.Blocks = dr["Blocks"] != null ? dr["Blocks"].ToString() : "";

                            if (dr["Faces"] != null && dr["Faces"].ToString() != "")
                                objProj.Faces = JsonConvert.DeserializeObject<DirectionFaces>(dr["Faces"] != null ? dr["Faces"].ToString() : "");

                            objProj.RoadNumber = dr["RoadNumber"] != null ? dr["RoadNumber"].ToString() : "";

                            if (dr["Borders"] != null && dr["Borders"].ToString() != "")
                                objProj.Borders = JsonConvert.DeserializeObject<DirectionFaces>(dr["Borders"] != null ? dr["Borders"].ToString() : "");

                            objProj.ProjectID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 0;

                            if (dr["ventureLocation"] != null && dr["ventureLocation"].ToString() != "")
                                objProj.ventureLocation = JsonConvert.DeserializeObject<Geos>(dr["ventureLocation"] != null ? dr["ventureLocation"].ToString() : "");

                            objProj.CoverPhotoTitle = dr["CoverPhotoTitle"] != null ? dr["CoverPhotoTitle"].ToString() : "";
                            objProj.CoverPhoto = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? RoutePath + "/Images/" + dr["CoverPhoto"].ToString() : "";
                            objProj.CoverPhotoDecription = dr["CoverPhotoDecription"] != null ? dr["CoverPhotoDecription"].ToString() : "";
                            objProj.CoverPhotoName = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? dr["CoverPhoto"].ToString() : "";
                            objProj.PhotoID = Convert.ToInt32(dr["PhotoID"].ToString());

                            objProj.BrochurePhotoTitle = dr["BrochurePhotoTitle"] != null ? dr["BrochurePhotoTitle"].ToString() : "";
                            objProj.BrochurePhoto = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? RoutePath + "/Images/" + dr["BrochurePhoto"].ToString() : "";
                            objProj.BrochurePhotoDecription = dr["BrochurePhotoDecription"] != null ? dr["BrochurePhotoDecription"].ToString() : "";
                            objProj.BrochurePhotoName = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? dr["BrochurePhoto"].ToString() : "";

                            objProj.LogoPhotoTitle = dr["LogoPhotoTitle"] != null ? dr["LogoPhotoTitle"].ToString() : "";
                            objProj.LogoPhoto = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? RoutePath + "/Images/" + dr["LogoPhoto"].ToString() : "";
                            objProj.LogoPhotoDecription = dr["LogoPhotoDecription"] != null ? dr["LogoPhotoDecription"].ToString() : "";
                            objProj.LogoPhotoName = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? dr["LogoPhoto"].ToString() : "";

                            objProj.AuthoritiesInfo = dr["AuthoritiesInfo"] != null ? dr["AuthoritiesInfo"].ToString() : "";
                            if (dr["SQYDPrice"] != null && dr["SQYDPrice"].ToString() != "")
                                objProj.sqydPrice = JsonConvert.DeserializeObject<SqydPrice>(dr["SQYDPrice"] != null ? dr["SQYDPrice"].ToString() : "");

                            objProj.ContactPerson1 = dr["ContactPerson1"] != null ? dr["ContactPerson1"].ToString() : "";
                            objProj.ContactPerson2 = dr["ConctactPerson2"] != null ? dr["ConctactPerson2"].ToString() : "";
                            objProj.Person1Mobile1 = dr["Person1Mobile1"] != null ? dr["Person1Mobile1"].ToString() : "";
                            objProj.Person1Mobile2 = dr["Person1Mobile2"] != null ? dr["Person1Mobile2"].ToString() : "";
                            objProj.Person2Mobile1 = dr["Person2Mobile1"] != null ? dr["Person2Mobile1"].ToString() : "";
                            objProj.Person2Mobile2 = dr["Person2Mobile2"] != null ? dr["Person2Mobile2"].ToString() : "";
                            objProj.ProjectHighlights = dr["ProjectHighlights"] != null ? dr["ProjectHighlights"].ToString() : "";

                            projects.Add(objProj);
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return projects;
        }

        public string SaveEnquiryProjects(LikeProjects Proj)
        {
            string responce = string.Empty;
            try
            {
                var ds = new DataSet();
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_SaveEnquiryProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = Proj.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = Proj.UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            var Notification = dr["Notification"] != null ? dr["Notification"].ToString() : "";
                            var VentureAdmin = dr["VentureAdmin"] != null ? dr["VentureAdmin"].ToString() : "";
                            var DeviceID = dr["DeviceID"] != null ? dr["DeviceID"].ToString() : "";
                           
                            if (Notification != "" && DeviceID !="")
                            {
                                FCMNotification objFCM = new FCMNotification();
                                objFCM.GenerateFCM_Auth_SendNotifcn(ConfigurationManager.AppSettings["FCM_Project_Title"], Notification, DeviceID);
                            }
                        }

                    }
                    //sql_cmnd.ExecuteNonQuery();
                    //sqlCon.Close();
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                ExceptionLogs("DBLogic", "SaveEnquiryProjects", Ex.ToString());
                   responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string CompleteEnquiryProjects(LikeProjects Proj)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_CompleteEnquiryProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@EnquiryID", SqlDbType.Int).Value = Proj.EnquiryID;
                    //sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = Proj.ProjectID;
                    //sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = Proj.UserID;
                    sql_cmnd.Parameters.AddWithValue("@EnquiryCompletedBY", SqlDbType.Int).Value = Proj.EnquiryCompletedBY;
                    sql_cmnd.Parameters.AddWithValue("@Comments", SqlDbType.NVarChar).Value = Proj.Comments;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                ExceptionLogs("DBLogic", "CompleteEnquiryProjects", Ex.ToString());
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<EnquiredProject> GetEnquiryProjects(LikeProjects proj)
        {
            List<EnquiredProject> projects = new List<EnquiredProject>();
            var ds = new DataSet();
            try
            {

                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_GetEnquiryProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = proj.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = proj.UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            EnquiredProject objProj = new EnquiredProject();
                            objProj.EnquiryID = dr["EnquiryID"] != null ? Convert.ToInt32(dr["EnquiryID"].ToString()) : 0;
                            objProj.ProjectID = dr["ProjectID"] != null ? Convert.ToInt32(dr["ProjectID"].ToString()) : 0;
                            objProj.ProjectName = dr["ProjectName"] != null ? dr["ProjectName"].ToString() : "";
                            objProj.ProjectAddress = dr["ProjectAddress"] != null ? dr["ProjectAddress"].ToString() : "";
                            objProj.ProjectDistrict = dr["ProjectDistrict"] != null ? dr["ProjectDistrict"].ToString() : "";
                            objProj.ProjectState = dr["ProjectState"] != null ? dr["ProjectState"].ToString() : "";
                            objProj.ProjectPostalCode = dr["ProjectPostalCode"] != null ? dr["ProjectPostalCode"].ToString() : "";
                            objProj.ProjectLandmark = dr["ProjectLandmark"] != null ? dr["ProjectLandmark"].ToString() : "";
                            objProj.UserID = dr["UserID"] != null ? Convert.ToInt32(dr["UserID"].ToString()) : 0;
                            objProj.UserName = dr["UserName"] != null ? dr["UserName"].ToString() : "";
                            objProj.UserMobile = dr["UserMobile"] != null ? dr["UserMobile"].ToString() : "";
                            objProj.UserEmail = dr["UserEmail"] != null ? dr["UserEmail"].ToString() : "";
                            objProj.EnquiredDate = Convert.ToDateTime(dr["EnquiredDate"].ToString());


                            projects.Add(objProj);
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogs("DBLogic", "GetEnquiryProjects", Ex.ToString());
                sqlCon.Close();
            }

            return projects;
        }


        public string InsertProjectsPlotsUploadFiles(FileUploadDetails fileUpload)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_InsertProjectsPlotsUploadFiles", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = fileUpload.projectID;
                    sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.Int).Value = fileUpload.plotID < 0 ? 0 : fileUpload.plotID;
                    sql_cmnd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = fileUpload.photoTitle;
                    sql_cmnd.Parameters.AddWithValue("@Attachments", SqlDbType.VarChar).Value = fileUpload.imagepaths;
                    sql_cmnd.Parameters.AddWithValue("@IsCoverPhoto", SqlDbType.Int).Value = fileUpload.isCoverPhoto;
                    sql_cmnd.Parameters.AddWithValue("@Decription", SqlDbType.VarChar).Value = fileUpload.photoDescription;
                    sql_cmnd.Parameters.AddWithValue("@CreatedBy", SqlDbType.Int).Value = fileUpload.UserID;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string ChangeCoverPhoto(FileUploadDetails fileUpload)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("usp_ChangeCoverPhoto", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = fileUpload.projectID;
                    sql_cmnd.Parameters.AddWithValue("@photoID", SqlDbType.Int).Value = fileUpload.photoID;
                    sql_cmnd.Parameters.AddWithValue("@IsCoverPhoto", SqlDbType.Int).Value = fileUpload.isCoverPhoto;
                    sql_cmnd.Parameters.AddWithValue("@CreatedBy", SqlDbType.Int).Value = fileUpload.UserID;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string AddProjectRoadsInfo(RoadsInfo project)
        {
            string responce = string.Empty;
            try
            {
                string RoadGEOInfo = "";
                RoadGEOInfo = JsonConvert.SerializeObject(project.RoadGEOInfo);
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_ProjectsRoadsInfo", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = project.projectID;
                    sql_cmnd.Parameters.AddWithValue("@RoadNo", SqlDbType.VarChar).Value = project.roadNo == null ? "" : project.roadNo;
                    sql_cmnd.Parameters.AddWithValue("@RoadWidth", SqlDbType.VarChar).Value = project.roadWidth == null ? "" : project.roadWidth;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = project.userID;
                    sql_cmnd.Parameters.AddWithValue("@RoadGEOInfo", SqlDbType.VarChar).Value = RoadGEOInfo;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    responce = "200";
                }
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }
        public string AddRecentVisitedProjects(Projects project)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_RecentVisitedProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = project.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = project.UserID;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    responce = "200";
                }
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }


        public List<ProjectsMini> GetRecentVisitedProjects(Projects proj, string isALL = "")
        {
            List<ProjectsMini> projects = new List<ProjectsMini>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetRecentVisitedProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = proj.UserID;
                    sql_cmnd.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = isALL;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ProjectsMini objProj = new ProjectsMini();
                            objProj.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objProj.Address = dr["Address"] != null ? dr["Address"].ToString() : "";
                            objProj.District = dr["District"] != null ? dr["District"].ToString() : "";
                            objProj.State = dr["State"] != null ? dr["State"].ToString() : "";
                            objProj.PostalCode = dr["PostalCode"] != null ? dr["PostalCode"].ToString() : "";
                            objProj.Landmark = dr["Landmark"] != null ? dr["Landmark"].ToString() : "";
                            if (dr["Photos"] != null && dr["Photos"].ToString() != "")
                                objProj.Photos = JsonConvert.DeserializeObject<List<files>>(dr["Photos"] != null ? dr["Photos"].ToString() : "");


                            objProj.Phase = dr["Phase"] != null ? dr["Phase"].ToString() : "";
                            objProj.Blocks = dr["Blocks"] != null ? dr["Blocks"].ToString() : "";

                            if (dr["Faces"] != null && dr["Faces"].ToString() != "")
                                objProj.Faces = JsonConvert.DeserializeObject<DirectionFaces>(dr["Faces"] != null ? dr["Faces"].ToString() : "");

                            objProj.RoadNumber = dr["RoadNumber"] != null ? dr["RoadNumber"].ToString() : "";

                            if (dr["Borders"] != null && dr["Borders"].ToString() != "")
                                objProj.Borders = JsonConvert.DeserializeObject<DirectionFaces>(dr["Borders"] != null ? dr["Borders"].ToString() : "");

                            objProj.ProjectID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 0;

                            if (dr["ventureLocation"] != null && dr["ventureLocation"].ToString() != "")
                                objProj.ventureLocation = JsonConvert.DeserializeObject<Geos>(dr["ventureLocation"] != null ? dr["ventureLocation"].ToString() : "");

                            objProj.CoverPhotoTitle = dr["CoverPhotoTitle"] != null ? dr["CoverPhotoTitle"].ToString() : "";
                            objProj.CoverPhoto = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? RoutePath + "/Images/" + dr["CoverPhoto"].ToString() : "";
                            objProj.CoverPhotoDecription = dr["CoverPhotoDecription"] != null ? dr["CoverPhotoDecription"].ToString() : "";
                            objProj.CoverPhotoName = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? dr["CoverPhoto"].ToString() : "";
                            objProj.PhotoID = Convert.ToInt32(dr["PhotoID"].ToString());

                            objProj.BrochurePhotoTitle = dr["BrochurePhotoTitle"] != null ? dr["BrochurePhotoTitle"].ToString() : "";
                            objProj.BrochurePhoto = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? RoutePath + "/Images/" + dr["BrochurePhoto"].ToString() : "";
                            objProj.BrochurePhotoDecription = dr["BrochurePhotoDecription"] != null ? dr["BrochurePhotoDecription"].ToString() : "";
                            objProj.BrochurePhotoName = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? dr["BrochurePhoto"].ToString() : "";

                            objProj.LogoPhotoTitle = dr["LogoPhotoTitle"] != null ? dr["LogoPhotoTitle"].ToString() : "";
                            objProj.LogoPhoto = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? RoutePath + "/Images/" + dr["LogoPhoto"].ToString() : "";
                            objProj.LogoPhotoDecription = dr["LogoPhotoDecription"] != null ? dr["LogoPhotoDecription"].ToString() : "";
                            objProj.LogoPhotoName = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? dr["LogoPhoto"].ToString() : "";

                            objProj.AuthoritiesInfo = dr["AuthoritiesInfo"] != null ? dr["AuthoritiesInfo"].ToString() : "";
                            if (dr["SQYDPrice"] != null && dr["SQYDPrice"].ToString() != "")
                                objProj.sqydPrice = JsonConvert.DeserializeObject<SqydPrice>(dr["SQYDPrice"] != null ? dr["SQYDPrice"].ToString() : "");
                            projects.Add(objProj);

                            objProj.ContactPerson1 = dr["ContactPerson1"] != null ? dr["ContactPerson1"].ToString() : "";
                            objProj.ContactPerson2 = dr["ConctactPerson2"] != null ? dr["ConctactPerson2"].ToString() : "";
                            objProj.Person1Mobile1 = dr["Person1Mobile1"] != null ? dr["Person1Mobile1"].ToString() : "";
                            objProj.Person1Mobile2 = dr["Person1Mobile2"] != null ? dr["Person1Mobile2"].ToString() : "";
                            objProj.Person2Mobile1 = dr["Person2Mobile1"] != null ? dr["Person2Mobile1"].ToString() : "";
                            objProj.Person2Mobile2 = dr["Person2Mobile2"] != null ? dr["Person2Mobile2"].ToString() : "";
                            objProj.ProjectHighlights = dr["ProjectHighlights"] != null ? dr["ProjectHighlights"].ToString() : "";
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return projects;
        }


        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }


        public List<ProjectsMini> GetProjectsListBasedOnRange(Coordinates objCoord)
        {
            List<ProjectsMini> projects = new List<ProjectsMini>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetAllProjects", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            double distance = 0;
                            var venGepLocation = JsonConvert.DeserializeObject<Geos>(dr["ventureLocation"] != null ? dr["ventureLocation"].ToString() : "");
                            if (venGepLocation != null)
                            {
                                distance = new Coordinates(objCoord.Latitude, objCoord.Longitude, objCoord.Range)
                                        .DistanceTo(
                                            new Coordinates(Convert.ToDouble(venGepLocation.lat), Convert.ToDouble(venGepLocation.lag), objCoord.Range),
                                            UnitOfLength.Kilometers
                                        );
                            }
                            if (venGepLocation != null && distance <= objCoord.Range)
                            {
                                ProjectsMini objProj = new ProjectsMini();
                                objProj.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                                objProj.Address = dr["Address"] != null ? dr["Address"].ToString() : "";
                                objProj.District = dr["District"] != null ? dr["District"].ToString() : "";
                                objProj.State = dr["State"] != null ? dr["State"].ToString() : "";
                                objProj.PostalCode = dr["PostalCode"] != null ? dr["PostalCode"].ToString() : "";
                                objProj.Landmark = dr["Landmark"] != null ? dr["Landmark"].ToString() : "";
                                if (dr["Photos"] != null && dr["Photos"].ToString() != "")
                                    objProj.Photos = JsonConvert.DeserializeObject<List<files>>(dr["Photos"] != null ? dr["Photos"].ToString() : "");


                                objProj.Phase = dr["Phase"] != null ? dr["Phase"].ToString() : "";
                                objProj.Blocks = dr["Blocks"] != null ? dr["Blocks"].ToString() : "";

                                if (dr["Faces"] != null && dr["Faces"].ToString() != "")
                                    objProj.Faces = JsonConvert.DeserializeObject<DirectionFaces>(dr["Faces"] != null ? dr["Faces"].ToString() : "");

                                objProj.RoadNumber = dr["RoadNumber"] != null ? dr["RoadNumber"].ToString() : "";

                                if (dr["Borders"] != null && dr["Borders"].ToString() != "")
                                    objProj.Borders = JsonConvert.DeserializeObject<DirectionFaces>(dr["Borders"] != null ? dr["Borders"].ToString() : "");

                                objProj.ProjectID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 0;

                                if (dr["ventureLocation"] != null && dr["ventureLocation"].ToString() != "")
                                    objProj.ventureLocation = JsonConvert.DeserializeObject<Geos>(dr["ventureLocation"] != null ? dr["ventureLocation"].ToString() : "");

                                objProj.CoverPhotoTitle = dr["CoverPhotoTitle"] != null ? dr["CoverPhotoTitle"].ToString() : "";
                                objProj.CoverPhoto = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? RoutePath + "/Images/" + dr["CoverPhoto"].ToString() : "";
                                objProj.CoverPhotoDecription = dr["CoverPhotoDecription"] != null ? dr["CoverPhotoDecription"].ToString() : "";
                                objProj.CoverPhotoName = !string.IsNullOrEmpty(dr["CoverPhoto"].ToString()) ? dr["CoverPhoto"].ToString() : "";
                                objProj.PhotoID = Convert.ToInt32(dr["PhotoID"].ToString());

                                objProj.BrochurePhotoTitle = dr["BrochurePhotoTitle"] != null ? dr["BrochurePhotoTitle"].ToString() : "";
                                objProj.BrochurePhoto = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? RoutePath + "/Images/" + dr["BrochurePhoto"].ToString() : "";
                                objProj.BrochurePhotoDecription = dr["BrochurePhotoDecription"] != null ? dr["BrochurePhotoDecription"].ToString() : "";
                                objProj.BrochurePhotoName = !string.IsNullOrEmpty(dr["BrochurePhoto"].ToString()) ? dr["BrochurePhoto"].ToString() : "";

                                objProj.LogoPhotoTitle = dr["LogoPhotoTitle"] != null ? dr["LogoPhotoTitle"].ToString() : "";
                                objProj.LogoPhoto = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? RoutePath + "/Images/" + dr["LogoPhoto"].ToString() : "";
                                objProj.LogoPhotoDecription = dr["LogoPhotoDecription"] != null ? dr["LogoPhotoDecription"].ToString() : "";
                                objProj.LogoPhotoName = !string.IsNullOrEmpty(dr["LogoPhoto"].ToString()) ? dr["LogoPhoto"].ToString() : "";

                                objProj.AuthoritiesInfo = dr["AuthoritiesInfo"] != null ? dr["AuthoritiesInfo"].ToString() : "";
                                if (dr["SQYDPrice"] != null && dr["SQYDPrice"].ToString() != "")
                                    objProj.sqydPrice = JsonConvert.DeserializeObject<SqydPrice>(dr["SQYDPrice"] != null ? dr["SQYDPrice"].ToString() : "");

                                objProj.ContactPerson1 = dr["ContactPerson1"] != null ? dr["ContactPerson1"].ToString() : "";
                                objProj.ContactPerson2 = dr["ConctactPerson2"] != null ? dr["ConctactPerson2"].ToString() : "";
                                objProj.Person1Mobile1 = dr["Person1Mobile1"] != null ? dr["Person1Mobile1"].ToString() : "";
                                objProj.Person1Mobile2 = dr["Person1Mobile2"] != null ? dr["Person1Mobile2"].ToString() : "";
                                objProj.Person2Mobile1 = dr["Person2Mobile1"] != null ? dr["Person2Mobile1"].ToString() : "";
                                objProj.Person2Mobile2 = dr["Person2Mobile2"] != null ? dr["Person2Mobile2"].ToString() : "";
                                objProj.ProjectHighlights = dr["ProjectHighlights"] != null ? dr["ProjectHighlights"].ToString() : "";

                                projects.Add(objProj);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return projects;
        }


        public UserOtp OtpAutenticatoionAndAuthorization(InputUserOtp users)
        {
            bool smsResult = false;
            UserOtp objUserOtp = new UserOtp();
            List<Users> Users = new List<Users>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetUsersByMobile_New", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = users.Mobile;
                    sql_cmnd.Parameters.AddWithValue("@Role", SqlDbType.Int).Value = users.Role;
                    sql_cmnd.Parameters.AddWithValue("@DeviceId", SqlDbType.VarChar).Value = users.DeviceId;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        OTPGeneration objotpGen = new OTPGeneration();
                        string otp = SMS_OTP_Stop ? "000000" : objotpGen.Generate_otp();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            // Sent OTP
                            string SMSContents = "";
                            SMSContents = otp + SMS_OTP_Message;
                            smsResult = SMS_OTP_Stop ? true : objotpGen.SendSMS(users.Mobile, SMSContents, otp);
                            if (smsResult)
                            {
                                objUserOtp.Mobile = dr["Mobile"] != null ? dr["Mobile"].ToString() : "";
                                objUserOtp.UserID = dr["Id"] != null ? Convert.ToInt32(dr["Id"].ToString()) : 1;
                                objUserOtp.OTP = otp;
                                objUserOtp.IsFirstLogin = dr["IsFirstTime"] != null && dr["IsFirstTime"].ToString() != "" ? Convert.ToBoolean(dr["IsFirstTime"].ToString()) : true;
                            }

                            break;
                        }

                    }

                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return objUserOtp;
        }

        public string UpdateUserPIN(Users user)
        {
            checkUsersStatus res = new checkUsersStatus();
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_UpdatePIN", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = user.UserID;
                    sql_cmnd.Parameters.AddWithValue("@PIN", SqlDbType.VarChar).Value = user.PIN;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }



        public List<ProjectAmenities> GetProjectAmenities()
        {
            List<ProjectAmenities> ProjectAmenities = new List<ProjectAmenities>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetProjectAmenities", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ProjectAmenities objProjectAmenities = new ProjectAmenities();
                            objProjectAmenities.name = dr["name"] != null ? dr["name"].ToString() : "";
                            objProjectAmenities.id = dr["id"] != null ? Convert.ToInt32(dr["id"].ToString()) : 1;
                            objProjectAmenities.description = dr["description"] != null ? dr["description"].ToString() : "";

                            ProjectAmenities.Add(objProjectAmenities);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return ProjectAmenities;
        }

        public List<Notifications> GetFCMDetails()
        {
            List<Notifications> userNotification = new List<Notifications>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_Get_Users_FCM", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Notifications objNotification = new Notifications();
                            objNotification.DeviceId = dr["DeviceId"] != null ? dr["DeviceId"].ToString() : "";
                            objNotification.UserID = dr["UserID"] != null ? Convert.ToInt32(dr["UserID"].ToString()) : 1;

                            userNotification.Add(objNotification);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return userNotification;
        }

        public UserSignupOtp verifySignupWithOTP(InputUserSignupOtp users)
        {
            bool smsResult = false;
            UserSignupOtp objUserOtp = new UserSignupOtp();
            List<Users> Users = new List<Users>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetUsersByMobileForSignUp", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = users.Mobile;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    bool sendOTP = false;

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if(Convert.ToInt32(dr["RoleID"].ToString()) == users.Role)
                            {
                                sendOTP = false;
                            }
                            else
                            {
                                sendOTP = true;
                            }
                            break;
                        }
                    }
                    else
                    {
                        sendOTP = true;
                    }

                    objUserOtp.Mobile = users.Mobile;
                    if (sendOTP)
                    {
                        OTPGeneration objotpGen = new OTPGeneration();
                        string otp = SMS_OTP_Stop ? "000000" : objotpGen.Generate_otp();
                        // Sent OTP
                        string SMSContents = "";
                        SMSContents = otp + SMS_OTP_Message;
                        smsResult = SMS_OTP_Stop ? true : objotpGen.SendSMS(users.Mobile, SMSContents, otp);
                        if (smsResult)
                        {
                            OTPCls objOTP = new OTPCls();
                            objOTP.OTP = otp;
                            objOTP.Mobile = users.Mobile;
                            objOTP.Type = "SignUP";
                            objOTP.Role = users.Role;

                            OTPRequestAndLog(objOTP);
                            objUserOtp.OTP = otp;
                            objUserOtp.Message = "OTP Sent successfully.";
                        }
                    }
                    else
                    {
                        objUserOtp.OTP = "";
                        objUserOtp.Message = "User Already exists with the same Mobile and Role.";
                    }                    
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogs("DBLogic", "verifySignupWithOTP", Ex.ToString());
                sqlCon.Close();
            }

            return objUserOtp;
        }

        public string deleteImages(ProjectFileName objProjectFileName)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_deleteImages", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@FileName", SqlDbType.VarChar).Value = objProjectFileName.FileName;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string OTPRequestAndLog(OTPCls otp)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_OTPLogs", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Type", SqlDbType.VarChar).Value = otp.Type;
                    sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = otp.Mobile;
                    sql_cmnd.Parameters.AddWithValue("@OTP", SqlDbType.VarChar).Value = otp.OTP;
                    sql_cmnd.Parameters.AddWithValue("@Role", SqlDbType.Int).Value = otp.Role;
                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string ValidateOTP(OTPCls otp)
        {
            string OTPValidate = "";

            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_ValidateOTP", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = otp.Mobile;
                    sql_cmnd.Parameters.AddWithValue("@OTP", SqlDbType.VarChar).Value = otp.OTP;
                    sql_cmnd.Parameters.AddWithValue("@Role", SqlDbType.Int).Value = otp.Role;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        OTPValidate = dr["Message"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogs("DBLogic", "ValidateOTP", Ex.ToString());
                sqlCon.Close();
            }

            return OTPValidate;
        }

        public string ExceptionLogs(string Page, string Method, string Exception)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_SaveExceptionLogs", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Page", SqlDbType.VarChar).Value = Page;
                    sql_cmnd.Parameters.AddWithValue("@Method", SqlDbType.VarChar).Value = Method;
                    sql_cmnd.Parameters.AddWithValue("@Exception", SqlDbType.VarChar).Value = Exception;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }

                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }


        public string InsertProjectReview(ProjectReview project)
        {
            string responce = string.Empty;
            try
            {
                string reviewImages = "";
                reviewImages = JsonConvert.SerializeObject(project.Images);
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_InsertProjectReview", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;

                    sql_cmnd.Parameters.AddWithValue("@PRID", SqlDbType.Int).Value = project.PRID.HasValue ? project.PRID : 0; 
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = project.UserID;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = project.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@RevirwTitle", SqlDbType.VarChar).Value = string.IsNullOrWhiteSpace(project.RevirwTitle)? "" : project.RevirwTitle;
                    sql_cmnd.Parameters.AddWithValue("@ReviewDesc", SqlDbType.VarChar).Value = string.IsNullOrWhiteSpace(project.ReviewDesc) ? "" : project.ReviewDesc;
                    sql_cmnd.Parameters.AddWithValue("@Rating", SqlDbType.Int).Value = project.Rating;
                    sql_cmnd.Parameters.AddWithValue("@Images", SqlDbType.VarChar).Value = reviewImages;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    responce = "200";

                }
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string DeleteProjectReview(ProjectReview project)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_DeleteProjectReview", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;


                    sql_cmnd.Parameters.AddWithValue("@PRID", SqlDbType.Int).Value = project.PRID.HasValue ? project.PRID : 0;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = project.UserID;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    responce = "200";

                }
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<ProjectReview> getProjectReview(ProjectReview project)
        {
            List<ProjectReview> projects = new List<ProjectReview>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetProjectReviews", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = project.UserID;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = project.ProjectID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            ProjectReview objProj = new ProjectReview();
                            objProj.PRID = Convert.ToInt32(dr["PRID"].ToString());
                            objProj.UserID = Convert.ToInt32(dr["UserID"].ToString());
                            objProj.ProjectID = Convert.ToInt32(dr["ProjectID"].ToString());
                            objProj.RevirwTitle = dr["RevirwTitle"] != null ? dr["RevirwTitle"].ToString() : "";
                            objProj.ReviewDesc = dr["ReviewDesc"] != null ? dr["ReviewDesc"].ToString() : "";
                            objProj.Rating = Convert.ToInt32(dr["Rating"].ToString());

                            string json = dr["Images"] != null ? dr["Images"].ToString() : "";
                            objProj.Images = JsonConvert.DeserializeObject<List<ProjectsReviewAttachments>>(json);

                            objProj.Date = Convert.ToDateTime(dr["UpdatedDate"].ToString());
                            if(objProj.Images.Count > 0)
                            {
                                objProj.Images.ForEach(x => x.PhotoName = RoutePath + "/ReviewImages/" + x.PhotoName);

                            }

                            projects.Add(objProj);
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return projects;
        }

        public string UnassignedPlots(PlotsUnAssigned Plot)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_UnassignPlots", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@ProjectID", SqlDbType.Int).Value = Plot.ProjectID;
                    sql_cmnd.Parameters.AddWithValue("@PlotID", SqlDbType.VarChar).Value = Plot.PlotID;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = Plot.UserID;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                responce = "200";
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string InsertGetInTouchDetails(GetInTouch git)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_InsertGetInTouchDetails", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;

                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = git.UserID;
                    sql_cmnd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = git.Name;
                    sql_cmnd.Parameters.AddWithValue("@Message", SqlDbType.VarChar).Value = string.IsNullOrWhiteSpace(git.Message) ? "" : git.Message;
                    sql_cmnd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = string.IsNullOrWhiteSpace(git.Email) ? "" : git.Email;
                    sql_cmnd.Parameters.AddWithValue("@Phone", SqlDbType.VarChar).Value = string.IsNullOrWhiteSpace(git.Phone) ? "" : git.Phone;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    responce = "200";
                }
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public string AssignGetInTouchDetails(GetInTouch git)
        {
            string responce = string.Empty;
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_AssignGIT", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;

                    sql_cmnd.Parameters.AddWithValue("@AssignedUserID", SqlDbType.Int).Value = git.AssignedUserID;
                    sql_cmnd.Parameters.AddWithValue("@GITID", SqlDbType.Int).Value = git.GITID;
                    sql_cmnd.Parameters.AddWithValue("@Comments", SqlDbType.VarChar).Value = string.IsNullOrWhiteSpace(git.Comments) ? "" : git.Comments;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    responce = "200";
                }
            }
            catch (Exception Ex)
            {
                responce = "500";
                sqlCon.Close();
            }

            return responce;
        }

        public List<GetInTouch> getInTouchDetails(GetInTouch git)
        {
            List<GetInTouch> projects = new List<GetInTouch>();
            var ds = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("sp_GetGITDetails", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.Int).Value = git.UserID;
                    var adapter = new SqlDataAdapter(sql_cmnd);

                    adapter.Fill(ds);
                    sqlCon.Close();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            GetInTouch objProj = new GetInTouch();
                            objProj.GITID = Convert.ToInt32(dr["GITID"].ToString());
                            objProj.UserID = Convert.ToInt32(dr["UserID"].ToString());
                            objProj.Name = dr["Name"] != null ? dr["Name"].ToString() : "";
                            objProj.Message = dr["Message"] != null ? dr["Message"].ToString() : "";
                            objProj.Email = dr["Email"] != null ? dr["Email"].ToString() : "";
                            objProj.Phone = dr["Phone"] != null ? dr["Phone"].ToString() : "";
                            objProj.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                            objProj.AssignedUserID = Convert.ToInt32(dr["AssignedTo"].ToString());
                            objProj.AssignStatus = dr["AssignStatus"] != null ? dr["AssignStatus"].ToString() : "";
                            objProj.Comments = dr["Comments"] != null ? dr["Comments"].ToString() : "";
                            objProj.CommentedDate = string.IsNullOrWhiteSpace(dr["CommentedDate"].ToString()) ? DateTime.Now: Convert.ToDateTime(dr["CommentedDate"].ToString());
                            objProj.AssignedName = dr["Name"] != null ? dr["Name"].ToString() : "";

                            projects.Add(objProj);
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                sqlCon.Close();
            }

            return projects;
        }

    }
}