using System;

namespace HiddenMickeyProject.Data
{
    public class Location
    {
        private int locationId;
        private int areaID;
        private string locationName = string.Empty;

        public int AreaId
        {
            get { return areaID; }
            set { areaID = value; }
        }

        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        public string LocationName
        {
            get { return locationName; }
            set { locationName = String.IsNullOrEmpty(value) ? String.Empty : value.Trim(); }
        }
    }
}