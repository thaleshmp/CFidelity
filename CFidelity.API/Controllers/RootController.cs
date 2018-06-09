using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CFidelity.API.Controllers
{
    [Route("v1/root")]
    public class RootController : Controller
    {
        public RootController()
        {
        }

        [HttpPost]
        [Route("")]
        public IActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files["teste"];
                List<string> listA = new List<string>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        listA.Add(line);
                    }
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}