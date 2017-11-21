using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConsoleApplication1
{
    public class SqlConnectTest
    {
        const string conStr = @"Data Source=CO1MSDNMTPSPPE6;Integrated Security=True";

        public void SqlConnect()
        {                                 
            SqlConnection sqlConnect = new SqlConnection(conStr);
            sqlConnect.Open();
            SqlCommand command = sqlConnect.CreateCommand();
            command.CommandText = "SELECT [insertUTCDate] FROM [Ratings].[dbo].[rating] order by [Ratings].[dbo].[rating].[insertUTCDate] desc";
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();            
            DateTime b = (DateTime)reader["insertUTCDate"];
            int c = b.Day;
            Console.ReadLine();
            reader.Close();
            sqlConnect .Close();
        }


        //Need to input System.Configuration in Reference for "ConnectionStringSettings"
        public void DbFactory()
        {
            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings["myConnection"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionSettings.ProviderName);
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionSettings.ConnectionString;
            conn.Open();
            DbCommand command = conn.CreateCommand();
            command.CommandText = "SELECT top 10 [insertUTCDate] FROM [Ratings].[dbo].[rating] order by [Ratings].[dbo].[rating].[insertUTCDate] desc";           
            using (DbDataReader dr = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    Console.WriteLine(dr["insertUTCDate"]);                    
                }
                dr.Close();              
                Console.ReadLine();
            }

        }
    }
}
