using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Please replace the connection string attribute settings
                string constr = "user id=viper;password=VIPER_ts;data source=SampleDataSource";

                OracleConnection con = new OracleConnection(constr);
                con.Open();
                Console.WriteLine("Connected to Oracle Database {0}", con.ServerVersion);
                con.Dispose();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex);
            }
            Console.WriteLine("Press RETURN to exit.");
            Console.ReadLine();
        }
    }
}
