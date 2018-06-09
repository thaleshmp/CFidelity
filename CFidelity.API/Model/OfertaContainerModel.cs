using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CFidelity.API.Models
{
    public class OfertaContainerModel
    {
        public ObjectId Id { get; set; }
        public string SKU { get; set; }
        public decimal PrecoDe { get; set; }
        public decimal PrecoFinal { get; set; }
        public string Bandeira { get; set; }
    }
}