using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Models
{
    public class Agent
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentMobile { get; set; }
        public int CreatedByID { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public string CreatedByMobile { get; set; }
                   
    }
}