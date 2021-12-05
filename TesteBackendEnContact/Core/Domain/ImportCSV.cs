using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface;

namespace TesteBackendEnContact.Core.Domain
{
    public class ImportCSV : IImportCSV
    {
        public string Comany { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string ContactName { get; set; }

        public ImportCSV(string comany, string name, string phone, string email, string adress, string contactName)
        {
            Comany = comany;
            Name = name;
            Phone = phone;
            Email = email;
            Adress = adress;
            ContactName = contactName;
        }

        public ImportCSV() { }
    }
}
