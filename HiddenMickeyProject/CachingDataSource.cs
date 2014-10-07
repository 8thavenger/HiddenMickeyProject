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
            return this.GetRegionById(id);
        }

        public bool SaveRegon(Region region)
        {
            return this.repository.SaveRegon(region);
        }

        public bool DeleteRegion(Region region)
        {
            return this.repository.DeleteRegion(region);
        }

        public IEnumerable<Area> GetAreasByRegionId(int id)
        {
            return this.repository.GetAreasByRegionId(id);
        }
    }
}