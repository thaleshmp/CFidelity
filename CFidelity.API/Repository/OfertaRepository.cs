using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using CFidelity.API.Repository.Interface;
using System.Threading.Tasks;
using CFidelity.API.Settings;
using Microsoft.Extensions.Options;
using CFidelity.API.Domain.MongoDB;
using CFidelity.API.Repository.Entities;

namespace CFidelity.API.Repository
{
    public class OfertaRepository : IOfertaRepository
    {
        private readonly MongoDBSettings _mongoDBSettings;
        private const string collectionName = "ofertas";

        public OfertaRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings.Value;
        }
        
        public void SaveOfertas(IEnumerable<Oferta> ofertas)
        {
            var database = MongoDBHelper.GetDatabase("mongodb://cfidelityapp:pass123@ds253840.mlab.com:53840", "cfidelity");
            var collection = database.GetCollection<Oferta>(collectionName);
            collection.InsertMany(ofertas);
        }

        public void SaveOferta(Oferta oferta)
        {
            var database = MongoDBHelper.GetDatabase("mongodb://cfidelityapp:pass123@ds253840.mlab.com:53840/cfidelity", "cfidelity");
            var collection = database.GetCollection<Oferta>(collectionName);
            collection.InsertOne(oferta);
        }
    }
}