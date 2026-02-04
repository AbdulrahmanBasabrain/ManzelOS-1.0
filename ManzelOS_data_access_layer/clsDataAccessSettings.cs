using System.Configuration;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer
{
    internal static class clsDataAccessSettings
    {
        //public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
         public static string ConnectionString = "Server = .; Database= ManzelOSDB; User Id = sa; Password = 123456; TrustServerCertificate=True;";

    }
}
