using System;

namespace HiddenMickeyProject.Data
{
    public class Region
    {
        private int regionId;

        public int RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }

        private string regionName = string.Empty;

        public string RegionName
        {
            get { return regionName; }
            set { regionName = String.IsNullOrEmpty(value) ? String.Empty : value.Trim(); }
        }
    }
}