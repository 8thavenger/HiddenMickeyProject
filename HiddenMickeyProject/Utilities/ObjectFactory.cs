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
        public static EntryViewModel CreateEntry(Models.Navigator navigator)
        {
            EntryViewModel model = new EntryViewModel();
            if (navigator.Entry != null)
            {
                model.EntryId = navigator.Entry.EntryId;
                model.Clue = navigator.Entry.Clue;
                model.Hint = navigator.Entry.Hint;
            }
            model.LocationId = navigator.LocationId;
            model.LocationName = navigator.LocationName;
            model.AreaName = navigator.AreaName;
            model.RegionName = navigator.RegionName;
            return model;
        }

        public static AreaViewModel CreateArea(Models.Navigator navigator)
        {
            AreaViewModel area = new AreaViewModel();
            area.RegionId = navigator.RegionId;
            area.AreaId = navigator.AreaId;
            area.AreaName = navigator.AreaName;
            area.RegionName = navigator.RegionName;
            area.Locations.AddRange(navigator.Locations.Select(location => new LocationViewModel() { AreaId = area.AreaId, LocationId = location.LocationId, LocationName = location.LocationName, RegionName = navigator.RegionName, AreaName = area.AreaName }));
            return area;
        }

        public static LocationViewModel CreateLocation(Models.Navigator navigator)
        {
            LocationViewModel location = new LocationViewModel();
            location.LocationId = navigator.LocationId;
            location.AreaId = navigator.AreaId;
            location.LocationName = navigator.LocationName;
            location.AreaName = navigator.AreaName;
            location.RegionName = navigator.RegionName;
            location.Entries.AddRange(navigator.Entries.Select(entry => new EntryViewModel() { Clue = entry.Clue, Hint = entry.Hint, LocationId = entry.LocationId, EntryId = entry.EntryId, AreaName = location.AreaName, LocationName = location.LocationName, RegionName = location.RegionName }));
            return location;
        }

        public static RegionViewModel CreateRegion(Models.Navigator navigator)
        {
            RegionViewModel region = new RegionViewModel();
            region.RegionId = navigator.RegionId;
            region.RegionName = navigator.RegionName;
            region.Areas.AddRange(navigator.Areas.Select(area => new AreaViewModel() { RegionId = region.RegionId, RegionName = region.RegionName, AreaId = area.AreaId, AreaName= area.AreaName }));
            return region;
        }

        public static Navigator GetNavigator(string regionName, string areaName, string locationName, int entryId=0)
        {
            Data.INavigationRepository repository = GetRepository();
            Models.Navigator navigator = new Models.Navigator();
            navigator.RegionName = regionName;
            navigator.AreaName = areaName;
            navigator.LocationName = locationName;
            navigator.EntryId = entryId;
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