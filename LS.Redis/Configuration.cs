using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.Redis
{
    public class Configuration
    {
        private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
                                                       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                       .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                                                       .Build();



        public class Controle
        {
            public static double TimeExpiration { get { return Convert.ToDouble(_configuration["Controle:TimeExpiration"]); } }
        }

        public class DataBase
        {
            public static string ConnectionStringRedis { get { return _configuration["ConnectionStrings:ConnectionStringRedis"]; } }
            public static string ConnectionStringMongo { get { return _configuration["ConnectionStrings:ConnectionStringMongo"]; } }
        }
    }
}
