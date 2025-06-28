using System;
using System.Collections.Generic;
using System.Linq;
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
}