using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CFidelity.API.Settings
{
    public class MongoDBSettings
    {
        public string CFidelityConnection { get; set; }
        public string CFidelityDatabase { get; set; }
    }
}