using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models;
using TesteBackendEnContact.Core.Domain;
using TesteBackendEnContact.Core.Interface;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportCSVController : ControllerBase
    {
        [HttpPost("{url}")]
        public ActionResult<IImportCSV> Post(string url, [FromServices] IImportCSVRepository importCSVRepository)
        {
            return Ok(importCSVRepository.ImportCSV(url));
        }

        [HttpPost]
        public ActionResult<IImportCSV> SalvarCSV(ImportCSV importCSV, [FromServices] IImportCSVRepository importCSVRepository)
        {
            return Ok(importCSVRepository.Save(importCSV));
        }
    }
}
