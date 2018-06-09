using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CFidelity.API.Controllers
{
    public class ValuesController : Controller
    {
        public ValuesController()
        {
        }

        [HttpGet]
        public IActionResult GetWithRestriction()
        {
            try
            {
                //var file = Request.Form.Files[""];
                //var reader = new StreamReader(file.OpenReadStream());
                //var csv = new CsvReader(reader);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}