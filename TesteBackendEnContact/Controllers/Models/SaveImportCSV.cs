using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain;
using TesteBackendEnContact.Core.Interface;

namespace TesteBackendEnContact.Controllers.Models
{
    public class SaveImportCSV
    {
        public string Comany { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string ContactName { get; set; }

        public IImportCSV ToImportCSV() => new ImportCSV(Comany,Name,Phone,Email,Adress,ContactName);
    }
}
