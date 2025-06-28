using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class Projects
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Landmark { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string Person1Mobile1 { get; set; }
        public string Person1Mobile2 { get; set; }
        public string Person2Mobile1 { get; set; }
        public string Person2Mobile2 { get; set; }
        public string Emails { get; set; }
        public string Description { get; set; }
        public List<files> Photos { get; set; }
        public List<files> Documents { get; set; }
        public List<files> Brocher { get; set; }
        public List<Geos> GEOInfo { get; set; }
        public string Amenities { get; set; }
        public string Phase { get; set; }
        public string Blocks { get; set; }
        public DirectionFaces Faces { get; set; }
        public string Naksha { get; set; }
        public string RoadsInfo { get; set; }
        public string NearByFeatures { get; set; }
        public string Directions { get; set; }
        public string Disclamier { get; set; }
        public string TotalArea { get; set; }
        public string Type { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public DirectionFaces Borders { get; set; }
        public string RoadNumber { get; set; }
        public Geos ventureLocation { get; set; }
        public string SurveyNumber { get; set; }

        public string CoverPhotoTitle { get; set; }
        public string CoverPhoto { get; set; }
        public string CoverPhotoDecription { get; set; }
        public string CoverPhotoName { get; set; }

        public List<ProjectsAttachments> ProjectPhotos { get; set; }

        public List<RoadsInfo> AllRoadsInfo { get; set; }

        public string BrochurePhotoTitle { get; set; }
        public string BrochurePhoto { get; set; }
        public string BrochurePhotoDecription { get; set; }
        public string BrochurePhotoName { get; set; }

        public string LogoPhotoTitle { get; set; }
        public string LogoPhoto { get; set; }
        public string LogoPhotoDecription { get; set; }
        public string LogoPhotoName { get; set; }
        public SqydPrice sqydPrice { get; set; }
        public string AuthoritiesInfo { get; set; }
        public string ProjectHighlights { get; set; }
    }

    public class ProjectsAttachments
    {
        public int PhotoID { get; set; }
        public string PhotoTitle { get; set; }
        public string Photo { get; set; }
        public string PhotoDecription { get; set; }
        public string PhotoName { get; set; }
        public int IsCoverPhoto { get; set; }
    }

    public class ProjectStaticResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ProjectsResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<ProjectsMini> Projects { get; set; }
    }
    public class ProjectsSpecResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public Projects Projects { get; set; }
    }

    public class Geos
    {
        public string lag { get; set; }
        public string lat { get; set; }
    }

    public class files
    {
        public string url { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }

    public class DirectionFaces
    {
        public string East { get; set; }
        public string North { get; set; }
        public string West { get; set; }
        public string South { get; set; }
    }


    public class ProjectsMini
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Landmark { get; set; }
        public List<files> Photos { get; set; }
        public string Phase { get; set; }
        public string Blocks { get; set; }
        public DirectionFaces Faces { get; set; }
        public int ProjectID { get; set; }
        public DirectionFaces Borders { get; set; }
        public string RoadNumber { get; set; }
        public Geos ventureLocation { get; set; }
        public string CoverPhotoTitle { get; set; }
        public string CoverPhoto { get; set; }
        public string CoverPhotoDecription { get; set; }
        public string CoverPhotoName { get; set; }
        public int PhotoID { get; set; }

        public string BrochurePhotoTitle { get; set; }
        public string BrochurePhoto { get; set; }
        public string BrochurePhotoDecription { get; set; }
        public string BrochurePhotoName { get; set; }

        public string LogoPhotoTitle { get; set; }
        public string LogoPhoto { get; set; }
        public string LogoPhotoDecription { get; set; }
        public string LogoPhotoName { get; set; }
        public SqydPrice sqydPrice { get; set; }
        public string AuthoritiesInfo { get; set; }

        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string Person1Mobile1 { get; set; }
        public string Person1Mobile2 { get; set; }
        public string Person2Mobile1 { get; set; }
        public string Person2Mobile2 { get; set; }
        public string ProjectHighlights { get; set; }
    }

    public class ProjectAssign
    {
        public int AssignedUserID { get; set; }
        public int ProjectID { get; set; }
        public List<int> PlotID { get; set; }
        public int UserID { get; set; }
    }

    public class ImageFiles
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ImageName { get; set; }
    }

    public class LikeProjects
    {
        public int ProjectID { get; set; }
        public int PlotID { get; set; }
        public int UserID { get; set; }
        public bool isLiked { get; set; }
        public int EnquiryCompletedBY { get; set; }
        public string Comments { get; set; }
        public int EnquiryID { get; set; }
    }

    public class EnquiredProject
    {
        public int EnquiryID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectAddress { get; set; }
        public string ProjectDistrict { get; set; }
        public string ProjectState { get; set; }
        public string ProjectPostalCode { get; set; }
        public string ProjectLandmark { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }
        public DateTime EnquiredDate { get; set; }
    }

    public class EnquiredProjectsResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<EnquiredProject> Projects { get; set; }
    }

    public class RoadsInfo
    {
        public int projectID { get; set; }
        public int userID { get; set; }
        public string roadNo { get; set; }
        public string roadWidth { get; set; }
        public List<Geos> RoadGEOInfo { get; set; }

    }

    public class SqydPrice
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }

    public class ProjectAmenitiesResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<ProjectAmenities> ProjectAmenities { get; set; }
    }

    public class ProjectFileName
    {
        public string FileName { get; set; }
    }

    public class ProjectReviewResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<ProjectReview> ProjectReview { get; set; }
    }
}