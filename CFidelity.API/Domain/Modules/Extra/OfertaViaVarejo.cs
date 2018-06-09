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
    public class OfertaViaVarejo
    {
        public IEnumerable<OfertaPrecoViaVarejo> PrecoSkus { get; set; }
    }

    public class OfertaPrecoViaVarejo
    {
        public OfertaBandeiraViaVarejo PrecoVenda { get; set; }
    }

    public class OfertaBandeiraViaVarejo
    {
        public string IdSku { get; set; }
        public decimal PrecoDe { get; set; }
        public decimal Preco { get; set; }
        public bool DisponibilidadeVenda { get; set; }
        public bool PossuiDescontoFidelizacao { get; set; }
        public decimal PercentualDesconto { get { return 100 - ((Preco * 100) / PrecoDe); } }
    }
}