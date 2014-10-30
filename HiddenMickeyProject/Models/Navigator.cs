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
        private List<Location> locations = new List<Location>();
        private List<Entry> entries = new List<Entry>();

        private int regionId;
        private int areaId;
        private int locationId;

        public int RegionId
        {
            get { return regionId; }
            set { regionId = value; }
        }

        public int AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }


        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        public string RegionName
        {
            get { return regionName; }
            set { regionName = String.IsNullOrEmpty(value) ? string.Empty : value.Trim(); }
        }

        public string AreaName
        {
            get 
            {
                if (String.IsNullOrEmpty(this.areaName) && this.areas.Count > 0)
                    return this.areas.First().AreaName;
                return areaName;            
            }
            set { areaName = String.IsNullOrEmpty(value) ? string.Empty : value.Trim(); }
        }

        public string LocationName
        {
            get
            {
                if (String.IsNullOrEmpty(this.locationName) && this.locations.Count > 0)
                    return this.locations.First().LocationName;
                return locationName;
            }
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

        public List<Location> Locations
        {
            get { return locations; }
        }

        public List<Entry> Entries
        {
            get { return this.entries; }
        }
    }
}