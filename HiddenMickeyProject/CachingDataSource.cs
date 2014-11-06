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
        private const string ENTRY_KEY = "entry[{0}]";
        private const string LOCATION_KEY = "location[{0}]";
        private const string AREA_KEY = "area[{0}]";
        private const string REGION_KEY = "regions";
        private INavigationRepository repository = null;
        Cache cache = null;

        public CachingRepository(INavigationRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Region> Regions()
        {
            IEnumerable<Region> result = HttpContext.Current.Cache[REGION_KEY] as IEnumerable<Region>;
            if (result == null)
            {
                result = this.repository.Regions();
                HttpContext.Current.Cache.Insert(REGION_KEY, result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return result;
        }

        public Region GetRegionById(int id)
        {
            List<Region> result = HttpContext.Current.Cache[REGION_KEY] as List<Region>;
            Region region = result.DefaultIfEmpty(new Region()).FirstOrDefault(r => r.RegionId == id);
            if (region.RegionId == 0)
            {
                region = this.repository.GetRegionById(id);
                result.Add(region);
                HttpContext.Current.Cache.Insert(REGION_KEY, result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return region;
        }

        public bool SaveRegon(Region region)
        {
            if (this.repository.SaveRegon(region))
            {
                List<Region> result = HttpContext.Current.Cache[REGION_KEY] as List<Region>;
                Region existing = result.DefaultIfEmpty(null).FirstOrDefault(r => r.RegionId == region.RegionId);
                if (existing != null)
                    result.Remove(existing);
                result.Add(region);
                HttpContext.Current.Cache.Insert(REGION_KEY, result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }
      
        public bool DeleteRegion(Region region)
        {
            if (this.repository.DeleteRegion(region))
            {
                List<Region> result = HttpContext.Current.Cache[REGION_KEY] as List<Region>;
                Region existing = result.DefaultIfEmpty(null).FirstOrDefault(r => r.RegionId == region.RegionId);
                if (existing != null)
                    result.Remove(existing);
                HttpContext.Current.Cache.Insert(REGION_KEY, result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }

        public IEnumerable<Area> GetAreasByRegionId(int id)
        {
            string key = string.Format(AREA_KEY, id);
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
                string key = string.Format(AREA_KEY, area.RegionId);
                List<Area> areas = HttpContext.Current.Cache[key] as List<Area>;
                Area current = areas.FirstOrDefault(a => a.AreaId == area.AreaId);
                if (current!=null)
                    areas.Remove(current);
                areas.Add(area);
                HttpContext.Current.Cache.Insert(key, areas, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }

        public IEnumerable<Location> GetLocationsByAreaId(int areaId)
        {
            string key = string.Format(LOCATION_KEY, areaId);
            List<Location> locations = HttpContext.Current.Cache[key] as List<Location>;
            if (locations == null)
            {
                locations = this.repository.GetLocationsByAreaId(areaId).ToList();
                HttpContext.Current.Cache.Insert(key, locations, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return locations;
        }

        public IEnumerable<Entry> GetEntriesByLocationId(int locationId)
        {
            string key = string.Format(ENTRY_KEY, locationId);
            List<Entry> entries = HttpContext.Current.Cache[key] as List<Entry>;
            if (entries == null)
            {
                entries = this.repository.GetEntriesByLocationId(locationId).ToList();
                HttpContext.Current.Cache.Insert(key, entries, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
            }
            return entries;
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
            if (this.repository.SaveLocation(location))
            {
                List<Location> locations = this.GetLocationsByAreaId(location.AreaId).ToList();
                Location current = locations.FirstOrDefault(l => l.LocationId == location.LocationId);
                if (current != null)
                    locations.Remove(current);
                locations.Add(location);
                string key = string.Format(LOCATION_KEY, location.AreaId);
                HttpContext.Current.Cache.Insert(key, locations, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }

        public bool SaveEntry(Entry entry)
        {
            if (this.repository.SaveEntry(entry))
            {
                List<Entry> entries = this.GetEntriesByLocationId(entry.LocationId).ToList();
                Entry current = entries.DefaultIfEmpty(null).First(e=>e.EntryId==entry.EntryId);
                if (current != null)
                    entries.Remove(current);
                entries.Add(entry);
                string key = string.Format(ENTRY_KEY, entry.LocationId);
                HttpContext.Current.Cache.Insert(key, entries, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }

        public bool DeleteArea(Area area)
        {
            if (this.repository.DeleteArea(area))
            {
                string key = string.Format(AREA_KEY, area.RegionId);
                List<Area> areas = HttpContext.Current.Cache[key] as List<Area>;
                Area current = areas.FirstOrDefault(a=>a.AreaId == area.AreaId);
                if(current != null)
                    areas.Remove(current);
                HttpContext.Current.Cache.Insert(key, areas, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }

        public bool DeleteLocation(Location location)
        {
            if (this.repository.DeleteLocation(location))
            {
                List<Location> locations = this.GetLocationsByAreaId(location.AreaId).ToList();
                Location current = locations.FirstOrDefault(l => l.LocationId == location.LocationId);
                if (current != null)
                    locations.Remove(current);
                string key = string.Format(LOCATION_KEY, location.AreaId);
                HttpContext.Current.Cache.Insert(key, locations, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }

        public bool DeleteEntry(Entry entry)
        {
            if (this.repository.DeleteEntry(entry))
            {
                List<Entry> entries = this.GetEntriesByLocationId(entry.LocationId).ToList();
                Entry current = entries.DefaultIfEmpty(null).First(e => e.EntryId == entry.EntryId);
                if (current != null)
                    entries.Remove(current);
                string key = string.Format(ENTRY_KEY, entry.LocationId);
                HttpContext.Current.Cache.Insert(key, entries, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
                return true;
            }
            return false;
        }
    }
}