using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using CFidelity.API.Repository.Entities;

namespace CFidelity.API.Repository.Interface
{
    public interface IOfertaRepository
    {
        void SaveOfertas(IEnumerable<Oferta> ofertas);
        void SaveOferta(Oferta oferta);
    }
}