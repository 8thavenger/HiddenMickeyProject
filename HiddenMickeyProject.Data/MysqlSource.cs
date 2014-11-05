using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace HiddenMickeyProject.Data
{
    public class MysqlSource : INavigationRepository
    {
        private string connectionString = string.Empty;

        public MysqlSource(string connectionString)
        {
            this.connectionString = connectionString.Trim();
        }

        public bool DeleteArea(Area area)
        {
            int rows = 0;
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("DeleteArea", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Area_Id", MySqlDbType.Int32).Value = area.AreaId;
                    cn.Open();
                    rows = cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            return rows == 1;
        }

        public bool DeleteEntry(Entry entry)
        {
            int rows = 0;
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("DeleteEntry", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Entry_Id", MySqlDbType.Int32).Value = entry.EntryId;
                    cn.Open();
                    rows = cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            return rows == 1;
        }

        public bool DeleteLocation(Location location)
        {
            int rows = 0;
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("DeleteLocation", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Location_Id", MySqlDbType.Int32).Value = location.LocationId;
                    cn.Open();
                    rows = cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            return rows == 1;
        }

        public bool DeleteRegion(Region region)
        {
            int rows = 0;
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("DeleteRegion", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Region_Id", MySqlDbType.Int32).Value = region.RegionId;
                    cn.Open();
                    rows = cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            return rows == 1;
        }

        public Area GetAreaById(int areaId)
        {
            Area area = null;
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SelectAreaById", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Area_Id", MySqlDbType.Int32).Value = areaId;
                    cn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            area = ReadArea(reader);
                    }
                }
            }
            return area;
        }

        public IEnumerable<Area> GetAreasByRegionId(int id)
        {
            List<Area> areas = new List<Area>();
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("GetAreasByRegionId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Region_Id", MySqlDbType.Int32).Value = id;
                    cn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            areas.Add(ReadArea(reader));
                    }
                }
            }
            return areas;
        }

        public IEnumerable<Entry> GetEntriesByLocationId(int locationId)
        {
            List<Entry> result = new List<Entry>();
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("GetEntriesByLocationId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Location_Id", MySqlDbType.Int32).Value = locationId;
                    cn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            result.Add(ReadEntry(reader));
                    }
                }
            }
            return result;
        }

        public Entry GetEntryById(int entryId)
        {
            Entry result = null;
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SelectEntryById", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Entry_Id", MySqlDbType.Int32).Value = entryId;
                    cn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            result = ReadEntry(reader);
                    }
                }
            }
            return result;
        }

        public Location GetLocationById(int locationId)
        {
            Location result = null;
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SelectLocationById", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Location_Id", MySqlDbType.Int32).Value = locationId;
                    cn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            result = ReadLocation(reader);
                    }
                }
            }
            return result;
        }

        public IEnumerable<Location> GetLocationsByAreaId(int areaId)
        {
            List<Location> result = new List<Location>();
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("GetLocationsByAreaId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Area_Id", MySqlDbType.Int32).Value = areaId;
                    cn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            result.Add(ReadLocation(reader));
                    }
                }
            }
            return result;
        }

        public Region GetRegionById(int id)
        {
            Region region = null;
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SelectRegionById", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Region_Id", MySqlDbType.Int32).Value = id;
                    cn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            region = ReadRegion(reader);
                    }
                }
            }
            return region;
        }

        public IEnumerable<Region> Regions()
        {
            List<Region> regions = new List<Region>();
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("GetAllRegions", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                            regions.Add(ReadRegion(reader));
                    }
                }
            }
            return regions;
        }

        public bool SaveArea(Area area)
        {
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                if (UpdateArea(area, cn))
                    return true;
                else
                    return InsertArea(area, cn);
            }
        }

        public bool SaveEntry(Entry entry)
        {
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                if (UpdateEntry(entry, cn))
                    return true;
                else
                    return InsertEntry(entry, cn);
            }
        }

        public bool SaveLocation(Location location)
        {
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                if (UpdateLocation(location, cn))
                    return true;
                else
                    return InsertLocation(location, cn);
            }
        }

        public bool SaveRegon(Region region)
        {
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                if (UpdateRegion(region, cn))
                    return true;
                else
                    return InsertRegion(region, cn);
            }
        }

        private static bool InsertRegion(Region region, MySqlConnection cn)
        {
            using (MySqlCommand cmd = new MySqlCommand("InsertRegion", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Region_Name", MySqlDbType.VarChar, 50).Value = region.RegionName;
                cn.Open();
                region.RegionId = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return region.RegionId != 0;
        }

        private static Area ReadArea(MySqlDataReader reader)
        {
            Area area = new Area();
            area.RegionId = Convert.ToInt32(reader["RegionId"]);
            area.AreaId = Convert.ToInt32(reader["AreaId"]);
            area.AreaName = reader["AreaName"].ToString();
            return area;
        }

        private static Entry ReadEntry(MySqlDataReader reader)
        {
            Entry item = new Entry();
            item.LocationId = Convert.ToInt32(reader["LocationId"]);
            item.EntryId = Convert.ToInt32(reader["EntryId"]);
            item.Hint = reader["Hint"].ToString();
            item.Clue = reader["Clue"].ToString();
            return item;
        }

        private static Location ReadLocation(MySqlDataReader reader)
        {
            Location item = new Location();
            item.LocationId = Convert.ToInt32(reader["LocationId"]);
            item.AreaId = Convert.ToInt32(reader["AreaId"]);
            item.LocationName = reader["LocationName"].ToString();
            return item;
        }

        private static Region ReadRegion(MySqlDataReader reader)
        {
            Region region = new Region();
            region.RegionId = Convert.ToInt32(reader["RegionId"]);
            region.RegionName = reader["RegionName"].ToString();
            return region;
        }

        private static bool UpdateRegion(Region region, MySqlConnection cn)
        {
            int rows = 0;
            using (MySqlCommand cmd = new MySqlCommand("UpdateRegion", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Region_Id", MySqlDbType.Int32).Value = region.RegionId;
                cmd.Parameters.Add("Region_Name", MySqlDbType.VarChar, 50).Value = region.RegionName;
                cn.Open();
                rows = cmd.ExecuteNonQuery();
                cn.Close();
            }
            return rows == 1;
        }

        private bool InsertArea(Area area, MySqlConnection cn)
        {
            using (MySqlCommand cmd = new MySqlCommand("InsertArea", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Region_Id", MySqlDbType.Int32).Value = area.RegionId;
                cmd.Parameters.Add("Area_Name", MySqlDbType.VarChar, 50).Value = area.AreaName;
                cn.Open();
                area.AreaId = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return area.AreaId != 0;
        }

        private bool InsertEntry(Entry entry, MySqlConnection cn)
        {
            using (MySqlCommand cmd = new MySqlCommand("InsertEntry", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Location_Id", MySqlDbType.Int32).Value = entry.LocationId;
                cmd.Parameters.Add("Hint", MySqlDbType.VarChar, 8000).Value = entry.Hint;
                cmd.Parameters.Add("Clue", MySqlDbType.VarChar, 8000).Value = entry.Clue;
                cn.Open();
                entry.EntryId = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return entry.EntryId != 0;
        }

        private bool InsertLocation(Location location, MySqlConnection cn)
        {
            using (MySqlCommand cmd = new MySqlCommand("InsertLocation", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Area_Id", MySqlDbType.Int32).Value = location.AreaId;
                cmd.Parameters.Add("Location_Name", MySqlDbType.VarChar, 50).Value =location.LocationName;
                cn.Open();
                location.LocationId = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return location.LocationId != 0;
        }

        private bool UpdateArea(Area area, MySqlConnection cn)
        {
            int rows = 0;
            using (MySqlCommand cmd = new MySqlCommand("UpdateArea", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Area_Id", MySqlDbType.Int32).Value = area.AreaId;
                cmd.Parameters.Add("Region_Id", MySqlDbType.Int32).Value = area.RegionId;
                cmd.Parameters.Add("Area_Name", MySqlDbType.VarChar, 50).Value = area.AreaName;
                cn.Open();
                rows = cmd.ExecuteNonQuery();
                cn.Close();
            }
            return rows == 1;
        }

        private bool UpdateEntry(Entry entry, MySqlConnection cn)
        {
            int rows = 0;
            using (MySqlCommand cmd = new MySqlCommand("InsertEntry", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Entry_Id", MySqlDbType.Int32).Value = entry.EntryId;
                cmd.Parameters.Add("Location_Id", MySqlDbType.Int32).Value = entry.LocationId;
                cmd.Parameters.Add("Hint", MySqlDbType.VarChar, 8000).Value = entry.Hint;
                cmd.Parameters.Add("Clue", MySqlDbType.VarChar, 8000).Value = entry.Clue;
                cn.Open();
                rows = cmd.ExecuteNonQuery();
                cn.Close();
            }
            return rows != 0;
        }

        private bool UpdateLocation(Location location, MySqlConnection cn)
        {
            int rows = 0;
            using (MySqlCommand cmd = new MySqlCommand("Updateocation", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Area_Id", MySqlDbType.Int32).Value = location.AreaId;
                cmd.Parameters.Add("Location_Id", MySqlDbType.Int32).Value = location.LocationId;
                cmd.Parameters.Add("Location_Name", MySqlDbType.VarChar, 50).Value = location.LocationName;
                cn.Open();
                rows = cmd.ExecuteNonQuery();
                cn.Close();
            }
            return rows == 1;
        }
    }
}