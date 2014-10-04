using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiddenMickeyProject.Areas.Editor.Models
{
    public class RegionModel : Data.Region
    {
        private List<Data.Area> areas = new List<Data.Area>();

        public List<Data.Area> Areas
        {
            get { return areas; }
        }
    }
}