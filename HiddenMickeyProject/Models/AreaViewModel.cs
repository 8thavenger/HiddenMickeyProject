using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiddenMickeyProject.Data;

namespace HiddenMickeyProject.Models
{
    public class AreaViewModel:Data.Area
    {
        private string regionName = string.Empty;
        private List<Location> locations = new List<Location>();

        public string RegionName
        {
            get { return regionName; }
            set { regionName = String.IsNullOrEmpty(value) ? String.Empty : value.Trim(); ; }
        }

        public List<Location> Locations
        {
            get { return this.locations; }
        }
    }
}