using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiddenMickeyProject.Data;

namespace HiddenMickeyProject.Models
{
    public class Navigator
    {
        private string regionName;
        private string areaName;
        private string locationName;
        private List<Region> regions = new List<Region>();
        private List<Area> areas = new List<Area>();

        private int regionId;

        public int RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }
        

        public string RegionName
        {
            get { return regionName; }
            set { regionName = String.IsNullOrEmpty(value) ? string.Empty : value.Trim(); }
        }

        public string AreaName
        {
            get { return areaName; }
            set { areaName = String.IsNullOrEmpty(value) ? string.Empty : value.Trim(); }
        }

        public string LocationName
        {
            get { return locationName; }
            set { locationName = String.IsNullOrEmpty(value)?string.Empty:value.Trim(); }
        }

        public List<Region> Regions
        {
            get { return regions; }
        }

        public List<Area> Areas
        {
            get { return areas; }
        }

    }
}