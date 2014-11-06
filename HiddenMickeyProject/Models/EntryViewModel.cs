using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiddenMickeyProject.Models
{
    public class EntryViewModel : Data.Entry
    {

        private string regionName = string.Empty;
        private string areaName = string.Empty;        
        private string locationName = string.Empty;

	    public string LocationName
	    {
		    get { return locationName;}
		    set { locationName = String.IsNullOrEmpty(value)?string.Empty:value.Trim();}
	    }


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

    }
}