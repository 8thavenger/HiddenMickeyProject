using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace HiddenMickeyProject.Data
{
    public class MysqlSource : INavigationRepository
    {
        string connectionString = string.Empty;

        public MysqlSource(string connectionString)
        {
            this.connectionString = connectionString.Trim();
        }

        public IEnumerable<Region> Regions()
        {
            List<Region> regions = new List<Region>();
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                using(MySqlCommand cmd = new MySqlCommand("GetAllRegions",cn))
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
                        {
                            Area area = new Area();
                            area.RegionId = Convert.ToInt32(reader["RegionId"]);
                            area.AreaId = Convert.ToInt32(reader["AreaId"]);
                            area.AreaName = reader["AreaName"].ToString();
                            areas.Add(area);
                        }
                    }
                }
            }
            return areas;
        }

        private static Region ReadRegion(MySqlDataReader reader)
        {
            Region region = new Region();
            region.RegionId = Convert.ToInt32(reader["RegionId"]);
            region.RegionName = reader["RegionName"].ToString();
            return region;
        }

        public bool SaveRegon(Region region)
        {
            using (MySqlConnection cn = new MySqlConnection(this.connectionString))
            {
                if (UpdateRegion(region, cn))
                    return true;
                else
                    return InsertRegion(region,cn);
            }
        }

        private static bool InsertRegion(Region region, MySqlConnection cn)
        {
            using (MySqlCommand cmd = new MySqlCommand("InsertRegion", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Region_Name", MySqlDbType.VarChar,50).Value = region.RegionName;
                cn.Open();
                region.RegionId = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return region.RegionId != 0;
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
    }
}
