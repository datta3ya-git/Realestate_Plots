using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RE.Enums
{
    public class Enums
    {
        public enum ActionTypes
        {
            NO,
            Add,
            Modify,
            Delete
        }

        public enum PlotStatus
        {
            Available,
            Sold,
            Reserved,
            Cancelled,
            Resale
        }
        public enum PhotoTypes
        {
            Common,
            Cover,
            Brochure,
            Logo,
            Layout
        }
    }
}