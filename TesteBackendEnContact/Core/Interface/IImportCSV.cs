using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteBackendEnContact.Core.Interface
{
    public interface IImportCSV
    {
        public int CompanyId { get; set; }
        public int ContactBookId { get; set; }
        public int ContactId { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string ContactName { get; set; }
    }
}
