using System;

namespace HiddenMickeyProject.Data
{
    public class Area
    {
        private int areaID;
        private string areaName = string.Empty;
        private int regionId;

        public int AreaId
        {
            get { return areaID; }
            set { areaID = value; }
        }

        public string AreaName
        {
            get { return areaName; }
            set { areaName = String.IsNullOrEmpty(value) ? String.Empty : value.Trim(); }
        }

        public int RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }
    }
}