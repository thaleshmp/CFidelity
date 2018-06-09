using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CFidelity.API.Domain.Logging
{
    public interface ICFidelityMongoClient : IMongoClient
    {
    }
}