using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RE.Models
{
    public class Plots
    {
        public int PlotID { get; set; }
        public string PlotNo { get; set; }
        public string Facings { get; set; }
        public string PlotSize { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public DirectionFaces RoadsInfo { get; set; }
        public List<files> PlotDocuments { get; set; }
        public List<Geos> GEOInfo { get; set; }
        public int IsSold { get; set; }
        public DirectionFaces Borders { get; set; }
        public string RoadNumber { get; set; }
        public bool IsApproved { get; set; }

        public string ProjName { get; set; }
        public string ProjAddress { get; set; }
        public string ProjDistrict { get; set; }
        public string ProjState { get; set; }
        public string ProjPostalCode { get; set; }
        public string ProjLandmark { get; set; }


        public string CoverPhotoTitle { get; set; }
        public string CoverPhoto { get; set; }
        public string CoverPhotoDecription { get; set; }
        public int PhotoID { get; set; }
        public string PlotDecription { get; set; }
        public DirectionFaces Boundaries { get; set; }

        public string SoldUserName { get; set; }
        public string SoldUserEmail { get; set; }
        public string SoldUserMobile { get; set; }
        public string ReservedUserName { get; set; }
        public string ReservedUserEmail { get; set; }
        public string ReservedUserMobile { get; set; }
        public string ResellUserName { get; set; }
        public string ResellUserEmail { get; set; }
        public string ResellUserMobile { get; set; }

        public decimal SQYDPrice { get; set; }

        public string PlotLength { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }

    }

    public class PlotsResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<Plots> Plots { get; set; }
    }

    public class PlotsWithUser
    {
        public int CustomerID { get; set; }
        public int ProjectID { get; set; }
        public int PlotID { get; set; }
        public int AgentID { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string AgentName { get; set; }
        public string CustomerMobile { get; set; }
        public string AgentMobile { get; set; }
        public Plots PlotInfo { get; set; }
    }

    public class PlotsWithUserResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<PlotsWithUser> PlotsWithUser { get; set; }
    }

    public class PlotsApprove
    {
        public List<int> PlotID { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }
    }
    public class PlotsUnAssigned
    {
        public int PlotID { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }
    }
    public class AgentApprove
    {
        public int AgentID { get; set; }
        public int ProjectID { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class AgentResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<Agent> Agents { get; set; }
    }

    public class PlotPrice
    {
        public int ProjectID { get; set; }
        public int PlotID { get; set; }
        public decimal SQYDPrice { get; set; }
        public int updated_by { get; set; }

    }

    public class PlotsHistory
    {
        public string Description { get; set; }
        public int PlotID { get; set; }
        public string PlotNo { get; set; }
        public string Facings { get; set; }
        public string PlotSize { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public DirectionFaces RoadsInfo { get; set; }
        public List<files> PlotDocuments { get; set; }
        public List<Geos> GEOInfo { get; set; }
        public int IsSold { get; set; }
        public DirectionFaces Borders { get; set; }
        public string RoadNumber { get; set; }
        public string ProjName { get; set; }
        public string ProjAddress { get; set; }
        public string ProjDistrict { get; set; }
        public string ProjState { get; set; }
        public string ProjPostalCode { get; set; }
        public string ProjLandmark { get; set; }
        public decimal SQYDPrice { get; set; }

    }

    public class PlotsHistoryResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public List<PlotsHistory> PlotsHistory { get; set; }
    }

    /// <summary>Request for sp_Get_PlotCurrentStatus_with_History</summary>
    public class PlotCurrentStatusRequest
    {
        public int PlotID { get; set; }
        /// <summary>'CURRENT' = current status + latest history; 'FULL' = full history</summary>
        public string Type { get; set; }
    }

    public class PlotStatusHistoryItem
    {
        public int HistoryID { get; set; }
        public int StatusID { get; set; }
        public string HistoryStatus { get; set; }
        public string CommentText { get; set; }
        public int ChangedBy { get; set; }
        public System.DateTime? ChangedDate { get; set; }
        public string ChangedByName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }

    public class PlotStatusPhoto
    {
        public int PhotoID { get; set; }
        public int HistoryID { get; set; }
        public string PhotoPath { get; set; }
        public int UploadedBy { get; set; }
        public System.DateTime? UploadedDate { get; set; }
    }

    public class PlotCurrentStatusResponce
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int PlotID { get; set; }
        public string PlotNumber { get; set; }
        public int ProjectID { get; set; }
        public string CurrentStatus { get; set; }
        public int CurrentStatusID { get; set; }
        public List<PlotStatusHistoryItem> History { get; set; }
        public List<PlotStatusPhoto> Photos { get; set; }
    }

    /// <summary>Request for Save_Status_For_Plots</summary>
    public class SaveStatusForPlotsRequest
    {
        public int PlotID { get; set; }
        public int ProjectID { get; set; }
        public int CurrentStatus { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
        public string PhotoPath { get; set; }
    }
}