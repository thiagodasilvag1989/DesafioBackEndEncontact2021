using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface;

namespace TesteBackendEnContact.Core.Domain
{
    public class ImportCSV : IImportCSV
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

        public ImportCSV(int companyId, int contactBookId, int contactId, string company, string name, string phone, string email, string adress, string contactName)
        {
            CompanyId = companyId;
            ContactBookId = contactBookId;
            ContactId = contactId;
            Company = company;
            Name = name;
            Phone = phone;
            Email = email;
            Adress = adress;
            ContactName = contactName;
        }

        public ImportCSV() { }
    }
}
