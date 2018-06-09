using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CFidelity.API.Domain.MongoDB
{
    public class MongoDBHelper
    {
        private static IMongoDatabase database;

        public static IMongoDatabase GetDatabase(string connectionString, string db)
        {
            if (database == null)
            {
                var client = new MongoClient(connectionString);

                database = client.GetDatabase(db);
            }

            return database;
        }
    }
}