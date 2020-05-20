using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.Redis
{
    public class ApplicationInitializer
    {
        private readonly IMongoClient mongoClient;

        public ApplicationInitializer(IMongoClient mongoClient)
        {            
            this.mongoClient = mongoClient;
        }

        public void Initializer()
        {
            InitializeMongo();
        }

        private void InitializeMongo()
        {
            mongoClient.GetDatabase("Api");
        }
    }
}
