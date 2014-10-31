using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using HiddenMickeyProject.Data;
using HiddenMickeyProject.Models;
using System.Configuration;

namespace HiddenMickeyProject.Utilities
{
    public static class ObjectFactory
    {
        public static AreaViewModel CreateArea(Models.Navigator navigator)
        {
            AreaViewModel area = new AreaViewModel();
            area.RegionId = navigator.RegionId;
            area.AreaId = navigator.AreaId;
            area.AreaName = navigator.AreaName;
            //area.Locations.AddRange(navigator.Areas);
            return area;
        }

        public static LocationViewModel CreateLocation(Models.Navigator navigator)
        {
            LocationViewModel location = new LocationViewModel();
            location.LocationId = navigator.LocationId;
            location.AreaId = navigator.AreaId;
            location.LocationName = navigator.LocationName;
            //area.Locations.AddRange(navigator.Areas);
            return location;
        }

        public static RegionViewModel CreateRegion(Models.Navigator navigator)
        {
            RegionViewModel region = new RegionViewModel();
            region.RegionId = navigator.RegionId;
            region.RegionName = navigator.RegionName;
            region.Areas.AddRange(navigator.Areas);
            return region;
        }

        public static Navigator GetNavigator(string regionName, string areaName, string locationName)
        {
            Data.INavigationRepository repository = GetRepository();
            Models.Navigator navigator = new Models.Navigator();
            navigator.RegionName = regionName;
            navigator.AreaName = areaName;
            navigator.LocationName = locationName;
            navigator.Regions.AddRange(repository.Regions());
            navigator.RegionId = navigator.Regions.DefaultIfEmpty(new Data.Region()).First(r => r.RegionName == navigator.RegionName).RegionId;

            navigator.Areas.AddRange(repository.GetAreasByRegionId(navigator.RegionId));
            if (!String.IsNullOrEmpty(navigator.AreaName))
            {
                Area area = navigator.Areas.FirstOrDefault<Area>(a => String.Compare(a.AreaName, navigator.AreaName, true) == 0);
                navigator.AreaId = area.AreaId;
                navigator.Locations.AddRange(repository.GetLocationsByAreaId(navigator.AreaId));
            }

            //locatons
            if (!String.IsNullOrEmpty(navigator.LocationName))
            {
                navigator.LocationId = navigator.Locations.DefaultIfEmpty(new Data.Location()).First(l => l.LocationName == navigator.LocationName).LocationId;
                navigator.Entries.AddRange(repository.GetEntriesByLocationId(navigator.LocationId));
            }

            return navigator;
        }

        public static INavigationRepository GetRepository()
        {
#if DEBUG
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream data = assembly.GetManifestResourceStream("HiddenMickeyProject.MockData.xml");
            INavigationRepository repository;
            using (XmlReader reader = XmlReader.Create(data))
            {
                repository = new Data.XmlSource(reader);
            }
            return new CachingRepository(repository);
#else
            string connection = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;
            return new CachingRepository(new Data.MysqlSource(connection));
#endif
        }
    }
}