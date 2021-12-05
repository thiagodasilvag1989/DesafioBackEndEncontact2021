using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IImportCSVRepository
    {
        Task Save(ImportCSV importCSV);
        List<ImportCSV> ImportCSV(string file);
    }
}
