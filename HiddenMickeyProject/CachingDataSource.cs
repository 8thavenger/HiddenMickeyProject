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
        private IRegionRepository repository = null;
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
            Dictionary<int, Region> result = GetRegionDictionary();
            if (!result.ContainsKey(id))
            {
                Region region = this.repository.GetRegionById(id);
                AddRegionToDictionary(region, result);
            }
            return result[id];
        }

        private void AddRegionToDictionary(Region region, Dictionary<int, Region> result)
        {
            result.Add(region.RegionId, region);
            HttpContext.Current.Cache.Insert("Region", result, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));
        }

        public bool SaveRegon(Region region)
        {
            if (this.repository.SaveRegon(region))
            {
                Dictionary<int, Region> result = GetRegionDictionary();
                if (!result.ContainsKey(region.RegionId))
                    AddRegionToDictionary(region, result);
                return true;
            }
            return false;
        }

        private static Dictionary<int, Region> GetRegionDictionary()
        {
            Dictionary<int, Region> result = HttpContext.Current.Cache["Region"] as Dictionary<int, Region>;
            if (result == null)
            {
                result = new Dictionary<int, Region>();
            }
            return result;
        }

      
        public bool DeleteRegion(Region region)
        {
            return this.repository.DeleteRegion(region);
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
    }
}