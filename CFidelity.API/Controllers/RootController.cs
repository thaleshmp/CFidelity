using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;
using CFidelity.API.Repository.Interface;
using System.Text;
using System.Data;
using OfficeOpenXml;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CFidelity.API.Repository.Entities;
using CFidelity.API.Models;
using System.Net.Http;
using CFidelity.API.Domain.Logging;

namespace CFidelity.API.Controllers
{
    [Route("v1/oferta")]
    public class OfertaController : Controller
    {
        private readonly IOfertaRepository _ofertaRepository;

        public OfertaController(IOfertaRepository ofertaRepository)
        {
            _ofertaRepository = ofertaRepository;
        }

        [HttpGet]
        [Route("processar")]
        public IActionResult ProcessarOferta(OfertaContainerModel model)
        {
            var list = new List<OfertaViaVarejo>();
            var currentOferta = new OfertaViaVarejo();
            if (model.Bandeira.ToLower() == "ponto frio")
            {
                currentOferta = ProccessPontoFrio(model);
                list.Add(ProccessExtra(model));
                list.Add(ProccessCasasBahia(model));
            }
            else if (model.Bandeira.ToLower() == "casas bahia")
            {
                currentOferta = ProccessCasasBahia(model);
                list.Add(ProccessExtra(model));
                list.Add(ProccessPontoFrio(model));
            }
            else if (model.Bandeira.ToLower() == "extra")
            {
                currentOferta = ProccessExtra(model);
                list.Add(ProccessPontoFrio(model));
                list.Add(ProccessCasasBahia(model));
            }
            OfertaCompareResultModel result = new OfertaCompareResultModel() { Ofertas = list };
            if (currentOferta.PrecoSku.PrecoVenda.Preco < model.PrecoFinal)
                result.Mensagem = "Preço desatualizado";

            result.OfertaValida = list.All(x => x.PrecoSku.PrecoVenda.Preco < currentOferta.PrecoSku.PrecoVenda.Preco);
            return Ok(result);
        }

        private OfertaViaVarejo ProccessExtra(OfertaContainerModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://preco.api-extra.com.br/");
            var response = client.GetStringAsync("/V1/Skus/PrecoVenda/?idssku=" + model.SKU).Result;
            var resultApi = Newtonsoft.Json.JsonConvert.DeserializeObject<OfertaViaVarejoAPI>(response);
            var result = new OfertaViaVarejo() { PrecoSku = resultApi.PrecoSkus.First() };
            result.Bandeira = "Extra";
            return result;
        }

        private OfertaViaVarejo ProccessCasasBahia(OfertaContainerModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://preco.api-casasbahia.com.br");
            var response = client.GetStringAsync("V1/Skus/PrecoVenda/?idssku=" + model.SKU).Result;
            var resultApi = Newtonsoft.Json.JsonConvert.DeserializeObject<OfertaViaVarejoAPI>(response);
            var result = new OfertaViaVarejo() { PrecoSku = resultApi.PrecoSkus.First() };
            result.Bandeira = "Casas Bahia";
            return result;
        }

        private OfertaViaVarejo ProccessPontoFrio(OfertaContainerModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://preco.api-pontofrio.com.br");
            var response = client.GetStringAsync("V1/Skus/PrecoVenda/?idssku=" + model.SKU).Result;
            var resultApi = Newtonsoft.Json.JsonConvert.DeserializeObject<OfertaViaVarejoAPI>(response);
            var result = new OfertaViaVarejo() { PrecoSku = resultApi.PrecoSkus.First() };
            result.Bandeira = "Ponto Frio";
            return result;
        }

        //[HttpPost]
        //[Route("")]
        //public IActionResult UploadFile()
        //{
        //    try
        //    {
        //        var file = Request.Form.Files[0];
        //        List<string> cols = new List<string>();
        //        var excelData = GetSheetData(file, out cols);
        //        List<Oferta> ofertas = new List<Oferta>();
        //        foreach (var row in excelData)
        //        {
        //            var oferta = new Oferta();
        //            foreach (var collumn in row.Keys)
        //            {
        //                switch (collumn.ToLower())
        //                {
        //                    case "descrição":
        //                        oferta.Descricao = row[collumn];
        //                        break;
        //                    case "sku":
        //                        oferta.SKU = row[collumn];
        //                        break;
        //                    case "categoria":
        //                        oferta.Categoria = row[collumn];
        //                        break;
        //                    case "marca":
        //                        oferta.Marca = row[collumn];
        //                        break;
        //                    case "preço de":
        //                        oferta.PrecoDe = decimal.Parse(row[collumn]);
        //                        break;
        //                    case "preço final":
        //                        oferta.PrecoFinal = decimal.Parse(row[collumn]);
        //                        break;
        //                    case "preço ponto por":
        //                        oferta.ValorPontos = decimal.Parse(row[collumn]);
        //                        break;
        //                    case "bandeira":
        //                        oferta.Bandeira = row[collumn];
        //                        break;
        //                }
        //            }
        //            ofertas.Add(oferta);
        //        }


        //        //_ofertaRepository.SaveOferta(ofertas.First());
        //        _ofertaRepository.SaveOfertas(ofertas);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        //[HttpGet]
        //[Route("")]
        //public IActionResult Get()
        //{
        //    return Ok();
        //}

        //private List<Dictionary<string, string>> GetSheetData(IFormFile file, out List<string> cols)
        //{
        //    using (var package = new ExcelPackage(file.OpenReadStream()))
        //    {
        //        var currentSheet = package.Workbook.Worksheets;
        //        var workSheet = currentSheet.First();
        //        var noOfCol = workSheet.Dimension.End.Column;
        //        var noOfRow = workSheet.Dimension.End.Row;

        //        //pegando colunas
        //        var colunas = new string[noOfCol];
        //        for (int i = 0; i < noOfCol; i++)
        //        {
        //            var value = workSheet.Cells[1, i + 1].Value;
        //            if (value != null)
        //                colunas[i] = value.ToString();
        //        }

        //        cols = colunas.ToList();

        //        //montando dicionario com valores da planilha
        //        var listDic = new List<Dictionary<string, string>>();
        //        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
        //        {
        //            var dic = new Dictionary<string, string>();
        //            for (int colIterator = 1; colIterator <= noOfCol; colIterator++)
        //            {
        //                dic.Add(colunas[colIterator - 1],
        //                    workSheet.Cells[rowIterator, colIterator].Value == null ? "" : workSheet.Cells[rowIterator, colIterator].Value.ToString());
        //            }
        //            listDic.Add(dic);
        //        }
        //        return listDic;
        //    }
        //}
    }
}