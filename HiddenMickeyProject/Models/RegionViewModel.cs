using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiddenMickeyProject.Data;

namespace HiddenMickeyProject.Models
{
    public class RegionViewModel : Region
    {
        private List<Area> areas = new List<Area>();
        public List<Area> Areas
        {
            get { return this.areas; }
        }
    }
}