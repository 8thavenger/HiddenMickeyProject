using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiddenMickeyProject.Data;

namespace HiddenMickeyProject.Models
{
    public class RegionViewModel : Region
    {
        private List<AreaViewModel> areas = new List<AreaViewModel>();
        public List<AreaViewModel> Areas
        {
            get { return this.areas; }
        }
    }
}