using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using CFidelity.API.Domain.Logging;

namespace CFidelity.API.Models
{
    public class OfertaCompareResultModel
    {
        public List<OfertaViaVarejo> Ofertas { get; set; }
        public bool OfertaValida { get; set; }
        public string Mensagem { get; set; }
    }
}