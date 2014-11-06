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
        private List<LocationViewModel> locations = new List<LocationViewModel>();

        public string RegionName
        {
            get { return regionName; }
            set { regionName = String.IsNullOrEmpty(value) ? String.Empty : value.Trim(); ; }
        }

        public List<LocationViewModel> Locations
        {
            get { return this.locations; }
        }
    }
}