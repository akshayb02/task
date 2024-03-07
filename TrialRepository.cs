using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Assignment3.DataContext;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Assignment3.Repository
{
    public class TrialRepository : ITrialRepository
    {
        private readonly ApplicationDbContext _context;
        public TrialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<dynamic> GetAll(int type, decimal greaterThan, decimal lessThan, Boolean includeCountry)
        {
            string queryl4 = "Select s.AP_L1Category,s.AP_L2Category,s.AP_L3Category,s.AP_L4Category,l.Country ," +
                "count(s.PO_BASE_SPEND) as count,sum(s.PO_BASE_SPEND) as sum from spend_cube_full as s " +
                "Left JOIN dbo.Location_Info as l ON s.PURCHASING_LOCATION_ID = l.Id " +
                "where (s.PO_BASE_SPEND between '" + greaterThan + "' and '" + lessThan + "') " +
                "group by Ap_L1Category,AP_L2Category,AP_L3Category,AP_L4Category,l.Country";
            string queryl3 = "Select s.AP_L1Category,s.AP_L2Category,s.AP_L3Category,l.Country ," +
                "count(s.PO_BASE_SPEND) as count,sum(s.PO_BASE_SPEND) as sum from spend_cube_full as s " +
                "Left JOIN dbo.Location_Info as l ON s.PURCHASING_LOCATION_ID = l.Id " +
                "where (s.PO_BASE_SPEND between '" + greaterThan + "' and '" + lessThan + "') " +
                "group by Ap_L1Category,AP_L2Category,AP_L3Category,l.Country";
            string queryl2 = "Select s.AP_L1Category,s.AP_L2Category,l.Country ," +
               "count(s.PO_BASE_SPEND) as count,sum(s.PO_BASE_SPEND) as sum from spend_cube_full as s " +
               "Left JOIN dbo.Location_Info as l ON s.PURCHASING_LOCATION_ID = l.Id " +
               "where (s.PO_BASE_SPEND between '" + greaterThan + "' and '" + lessThan + "') " +
               "group by Ap_L1Category,AP_L2Category,l.Country";
            string queryl1 = "Select s.AP_L1Category,l.Country ," +
               "count(s.PO_BASE_SPEND) as count,sum(s.PO_BASE_SPEND) as sum from spend_cube_full as s " +
               "Left JOIN dbo.Location_Info as l ON s.PURCHASING_LOCATION_ID = l.Id " +
               "where (s.PO_BASE_SPEND between '" + greaterThan + "' and '" + lessThan + "') " +
               "group by Ap_L1Category,l.Country";
            string querylc4 = "Select s.AP_L1Category,s.AP_L2Category,s.AP_L3Category,s.AP_L4Category ," +
                "count(s.PO_BASE_SPEND) as count,sum(s.PO_BASE_SPEND) as sum from spend_cube_full as s " +
                "where (s.PO_BASE_SPEND between '" + greaterThan + "' and '" + lessThan + "') " +
                "group by Ap_L1Category,AP_L2Category,AP_L3Category,AP_L4Category";
            string querylc3 = "Select s.AP_L1Category,s.AP_L2Category,s.AP_L3Category ," +
                "count(s.PO_BASE_SPEND) as count,sum(s.PO_BASE_SPEND) as sum from spend_cube_full as s " +
                "where (s.PO_BASE_SPEND between '" + greaterThan + "' and '" + lessThan + "') " +
                "group by Ap_L1Category,AP_L2Category,AP_L3Category";
            string querylc2 = "Select s.AP_L1Category,s.AP_L2Category," +
               "count(s.PO_BASE_SPEND) as count,sum(s.PO_BASE_SPEND) as sum from spend_cube_full as s " +
               "where (s.PO_BASE_SPEND between '" + greaterThan + "' and '" + lessThan + "') " +
               "group by Ap_L1Category,AP_L2Category";
            string querylc1 = "Select s.AP_L1Category ," +
               "count(s.PO_BASE_SPEND) as count,sum(s.PO_BASE_SPEND) as sum from spend_cube_full as s " +
               "where (s.PO_BASE_SPEND between '" + greaterThan + "' and '" + lessThan + "') " +
               "group by Ap_L1Category";




            using (var connection = this._context.CreateConnection())
            {
                if (includeCountry == true)
                {
                    switch (type)
                    {
                        case 1:
                            var spendList = connection.Query(queryl4);
                            return spendList.ToList();
                            break;
                        case 2:
                            var spendList2 = connection.Query(queryl3);
                            return spendList2.ToList();
                            break;
                        case 3:
                            var spendList3 = connection.Query(queryl2);
                            return spendList3.ToList();
                            break;
                        case 4:
                            var spendList4 = connection.Query(queryl1);
                            return spendList4.ToList();
                            break;

                    }
                }
                else
                    switch (type)
                    {
                        case 1:
                            var spendList = connection.Query(querylc4);
                            return spendList.ToList();
                            break;
                        case 2:
                            var spendList2 = connection.Query(querylc3);
                            return spendList2.ToList();
                            break;
                        case 3:
                            var spendList3 = connection.Query(querylc2);
                            return spendList3.ToList();
                            break;
                        case 4:
                            var spendList4 = connection.Query(querylc1);
                            return spendList4.ToList();
                            break;
                    }
            }
            return null;

        }
        //public IEnumerable<dynamic> GetTrial()
        //{

        //}
        //return null;

        //public IEnumerable<dynamic> GetTrial()
        //{
        //    SqlCommand cmd;
        //    SqlDataReader reader;
        //    string connectionString = "Server=localhost;Database=Assignment; user Id=user1; password=user1;Encrypt=false;MultipleActiveResultSets=True";
        //    string sql1 = "select distinct AP_L1Category, count(*) as [Count] from spend_cube_full group by AP_L1Category order by AP_L1Category";
        //    string sql2 = "";
        //    SqlConnection cnn = new SqlConnection(connectionString);
        //    cnn.Open();
        //    cmd = new SqlCommand(sql1, cnn);
        //    reader = cmd.ExecuteReader();
        //    var dict = new Dictionary<string, int>();
        //    while (reader.Read())
        //    {
        //        string column = reader.GetString(0);
        //        int value = reader.GetInt32(1);
        //        dict.Add(column, value);

        //    }      



        //    foreach (string key in dict.Keys)
        //    {
        //        Console.WriteLine(key + "=" + dict[key]);
        //    }

        //    return cnn.Query(sql1);
        //}
        public IDictionary<string, Dictionary<string, List<string>>> GetTrial()
        {
            SqlCommand cmd;
            string connectionString = "Server=localhost;Database=Assignment; user Id=user1; password=user1;Encrypt=false;MultipleActiveResultSets=True";
            string query = "SELECT DISTINCT Ap_L1Category, AP_L2Category,AP_L3Category FROM spend_cube_full";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            cmd = new SqlCommand(query, cnn);
            Dictionary<string, Dictionary<string, List<string>>> dict = new Dictionary<string, Dictionary<string, List<string>>>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string l1 = reader.GetString(0);
                string l2 = reader.GetString(1);
                string l3 = reader.GetString(2);

                if (dict.ContainsKey(l1))
                {
                    var l2dict = dict[l1];
                    if (l2dict.ContainsKey(l2))
                    {
                        var l3list = l2dict[l2];
                        if (!l3list.Contains(l3))
                        {
                            l3list.Add(l3);
                        }
                    }
                    else
                    {
                        l2dict[l2] = new List<string> {l3};
                    }
                }
                else
                {
                    dict[l1] = new Dictionary<string, List<string>>
                    {
                        { l2, new List<string> {l3} }
                    };
                
                }
            }
            return dict;
        }
    }
}

