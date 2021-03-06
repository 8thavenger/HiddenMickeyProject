﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiddenMickeyProject.Models
{
    public class LocationViewModel:Data.Location
    {
        private string regionName = string.Empty;
        private string areaName = string.Empty;
        private List<Models.EntryViewModel> entries = new List<Models.EntryViewModel>();

        public string RegionName
        {
            get { return regionName; }
            set { regionName = String.IsNullOrEmpty(value)?string.Empty:value.Trim(); }
        }

        public string AreaName
        {
            get { return areaName; }
            set { areaName = String.IsNullOrEmpty(value) ? string.Empty : value.Trim(); ; }
        }

        public List<Models.EntryViewModel> Entries
        {
            get { return this.entries; }
        }
    }
}