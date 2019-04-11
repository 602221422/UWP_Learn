using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3.Helper
{
    class SqlDBHelper
    {
        private static string connectionString = @"Data Source = 172.17.16.133; Initial Catalog = ServerDB; User ID = sa; Password=Password001!;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }
        public static ObservableCollection<string> QueryDataInDB()
        {
            const string GetMembersQuery = "select ChineseName from dbo.EngineerDaysOff ";
            ObservableCollection<string> names=new ObservableCollection<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connectionString;
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetMembersQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string name = reader.GetString(0);
                                    names.Add(name);
                                }
                            }
                        }
                    }

                }
                return names;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

    }
}
