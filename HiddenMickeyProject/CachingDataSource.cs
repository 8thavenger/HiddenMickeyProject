using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiddenMickeyProject.Data;
using System.Web.Caching;

namespace HiddenMickeyProject
{
    public class CachingRepository : INavigationRepository
    {
        private INavigationRepository repository = null;
        Cache cache = null;

        public CachingRepository(INavigationRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Region> Regions()
        {
            IEnumerable<Region> result = HttpContext.Current.Cache["Regions"] as IEnumerable<Region>;
            if (result == null)
            {
                result = this.repository.Regions();
                HttpContext.Current.Cache.Insert("Regions", result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return result;
        }

        public Region GetRegionById(int id)
        {
            List<Region> result = HttpContext.Current.Cache["Regions"] as List<Region>;
            Region region = result.DefaultIfEmpty(new Region()).FirstOrDefault(r => r.RegionId == id);
            if (region.RegionId == 0)
            {
                region = this.repository.GetRegionById(id);
                result.Add(region);
                HttpContext.Current.Cache.Insert("Regions", result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return region;
        }

        public bool SaveRegon(Region region)
        {
            if (this.repository.SaveRegon(region))
            {
                List<Region> result = HttpContext.Current.Cache["Regions"] as List<Region>;
                Region existing = result.DefaultIfEmpty(null).FirstOrDefault(r => r.RegionId == region.RegionId);
                if (existing != null)
                    result.Remove(existing);
                result.Add(region);
                HttpContext.Current.Cache.Insert("Regions", result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }
      
        public bool DeleteRegion(Region region)
        {
            if (this.repository.DeleteRegion(region))
            {
                List<Region> result = HttpContext.Current.Cache["Regions"] as List<Region>;
                Region existing = result.DefaultIfEmpty(null).FirstOrDefault(r => r.RegionId == region.RegionId);
                if (existing != null)
                    result.Remove(existing);
                HttpContext.Current.Cache.Insert("Regions", result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }

        public IEnumerable<Area> GetAreasByRegionId(int id)
        {
            string key = string.Format("area[{0}]", id);
            List<Area> areas = HttpContext.Current.Cache[key] as List<Area>;
            if(areas == null)
            {
                areas = this.repository.GetAreasByRegionId(id).ToList();
                HttpContext.Current.Cache.Insert(key,areas, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return areas;
        }

        public bool SaveArea(Area area)
        {
            if (this.repository.SaveArea(area))
            {
                string key = string.Format("area[{0}]", area.RegionId);
                List<Area> areas = HttpContext.Current.Cache[key] as List<Area>;
                if (areas.Count(a => a.AreaId == area.AreaId) != 0)
                {
                    int index = areas.IndexOf(area);
                    areas.RemoveAt(index);
                }
                areas.Add(area);
                HttpContext.Current.Cache.Insert(key, areas, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }


        public IEnumerable<Location> GetLocationsByAreaId(int areaId)
        {
            return this.repository.GetLocationsByAreaId(areaId);
        }

        public IEnumerable<Entry> GetEntriesByLocationId(int locationId)
        {
            return this.repository.GetEntriesByLocationId(locationId);
        }

        public Area GetAreaById(int areaId)
        {
            return this.repository.GetAreaById(areaId);
        }

        public Location GetLocationById(int locationId)
        {
            return this.repository.GetLocationById(locationId);
        }

        public Entry GetEntryById(int entryId)
        {
            return this.repository.GetEntryById(entryId);
        }

        public bool SaveLocation(Location location)
        {
            return this.repository.SaveLocation(location);
        }

        public bool SaveEntry(Entry entry)
        {
            return this.repository.SaveEntry(entry);
        }

        public bool DeleteArea(Area area)
        {
            return this.repository.DeleteArea(area);
        }

        public bool DeleteLocation(Location location)
        {
            return this.repository.DeleteLocation(location);
        }

        public bool DeleteEntry(Entry entry)
        {
            return this.repository.DeleteEntry(entry);
        }
    }
}