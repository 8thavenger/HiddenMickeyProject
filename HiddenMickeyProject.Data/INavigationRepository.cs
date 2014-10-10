using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiddenMickeyProject.Data
{
    public interface INavigationRepository 
    {
        IEnumerable<Region> Regions();
        IEnumerable<Area> GetAreasByRegionId(int id);
        IEnumerable<Location> GetLocationsByAreaId(int areaId);
        IEnumerable<Entry> GetEntriesByLocationId(int locationId);

        Region GetRegionById(int id);
        Area GetAreaById(int areaId);
        Location GetLocationById(int locationId);
        Entry GetEntryById(int entryId);

        bool SaveRegon(Region region);
        bool SaveArea(Area area);
        bool SaveLocation(Location location);
        bool SaveEntry(Entry entry);

        bool DeleteRegion(Region region);
        bool DeleteArea(Area area);
        bool DeleteLocation(Location location);
        bool DeleteEntry(Entry entry);
    }
}
