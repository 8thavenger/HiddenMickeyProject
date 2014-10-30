using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiddenMickeyProject.Data;
using HiddenMickeyProject.Models;

namespace HiddenMickeyProject.Utilities
{
    public class DataFactory
    {
        public static RegionViewModel CreateRegion(Models.Navigator navigator)
        {
            RegionViewModel region = new RegionViewModel();
            region.RegionId = navigator.RegionId;
            region.RegionName = navigator.RegionName;
            region.Areas.AddRange(navigator.Areas);
            return region;
        }
     }
}