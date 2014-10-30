using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HiddenMickeyProject.Data
{
    public class XmlSource : INavigationRepository
    {
        XmlDocument doc;

        public XmlSource(XmlReader reader)
        {
            doc = new XmlDocument();
            doc.Load(reader);
        }

        public IEnumerable<Region> Regions()
        {
            XmlNodeList list = doc.SelectNodes("hidden-mickey/region");
            List<Region> regions = new List<Region>();
            foreach (XmlNode node in list)
            {
                Region region = new Region();
                region.RegionName = node.Attributes["name"].Value.ToString();
                region.RegionId = Convert.ToInt32(node.Attributes["id"].Value);
                regions.Add(region);
            }
            return regions;
        }

        public IEnumerable<Area> GetAreasByRegionId(int id)
        {
            XmlNodeList list = doc.SelectNodes("hidden-mickey/region[@id="+id+"]/area");
            List<Area> areas = new List<Area>();
            foreach (XmlNode node in list)
            {
                Area area = new Area();
                area.RegionId = id;
                area.AreaId = Convert.ToInt32(node.Attributes["id"].Value);
                area.AreaName = node.Attributes["name"].Value;
                areas.Add(area);
            }
            return areas;
        }

        public IEnumerable<Location> GetLocationsByAreaId(int areaId)
        {
            XmlNodeList nodes = doc.SelectNodes("hidden-mickey/region/area[@id=" + areaId + "]/location");
            List<Location> locations = new List<Location>();
            foreach (XmlNode node in nodes)
            {
                Location location = new Location();
                location.AreaId = Convert.ToInt32(node.ParentNode.Attributes["id"].Value);
                location.LocationId = Convert.ToInt32(node.Attributes["id"].Value);
                location.LocationName = node.Attributes["name"].Value;
                locations.Add(location);
            }
            return locations;
        }

        public IEnumerable<Entry> GetEntriesByLocationId(int locationId)
        {
            XmlNodeList nodes = doc.SelectNodes("hidden-mickey/region/area/location[@id=" + locationId + "]/entry");
            List<Entry> entries = new List<Entry>();
            foreach (XmlNode node in nodes)
            {
                Entry entry = new Entry();
                entry.EntryId = Convert.ToInt32(node.Attributes["id"].Value);
                entry.Hint = node.SelectSingleNode("Hint").InnerText;
                entry.Clue = node.SelectSingleNode("Clue").InnerText;
                entry.LocationId = locationId;
                entries.Add(entry);
            }
            return entries;
        }

        public Region GetRegionById(int id)
        {
            XmlNode node = doc.SelectSingleNode("hidden-mickey/region[@id=" + id + "]");
            Region region = new Region();
            region.RegionName = node.Attributes["name"].Value.ToString();
            region.RegionId = Convert.ToInt32(node.Attributes["id"].Value);
            return region;
        }

        public Area GetAreaById(int areaId)
        {                        
            XmlNode node = doc.SelectSingleNode("hidden-mickey/region/area[@id="+areaId+"]");
            Area area = new Area();
            area.RegionId = Convert.ToInt32(node.ParentNode.Attributes["id"]);
            area.AreaId = Convert.ToInt32(node.Attributes["id"].Value);
            area.AreaName = node.Attributes["name"].Value;
            return area;
        }

        public Location GetLocationById(int locationId)
        {
            XmlNode node = doc.SelectSingleNode("hidden-mickey/region/area/location[@id=" + locationId + "]");
            Location location = new Location();
            location.AreaId = Convert.ToInt32(node.ParentNode.Attributes["id"]);
            location.LocationId = Convert.ToInt32(node.Attributes["id"].Value);
            location.LocationName = node.Attributes["name"].Value;
            return location;
        }

        public Entry GetEntryById(int entryId)
        {
            throw new NotImplementedException();
        }

        public bool SaveRegon(Region region)
        {
            return true;
        }

        public bool SaveArea(Area area)
        {
            return true;
        }

        public bool SaveLocation(Location location)
        {
            return true;
        }

        public bool SaveEntry(Entry entry)
        {
            return true;
        }

        public bool DeleteRegion(Region region)
        {
            return true;
        }

        public bool DeleteArea(Area area)
        {
            return true;
        }

        public bool DeleteLocation(Location location)
        {
            return true;
        }

        public bool DeleteEntry(Entry entry)
        {
            return true;
        }
    }
}
