using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiddenMickeyProject.Data
{
    public interface IRegionRepository
    {
        IEnumerable<Region> Regions();
        Region GetRegionById(int id);
        bool SaveRegon(Region region);
        bool DeleteRegion(Region region);
        IEnumerable<Area> GetAreasByRegionId(int id);
    }
}
