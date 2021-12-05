using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain;
using TesteBackendEnContact.Core.Interface;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    public class ImportCSVRepository : IImportCSVRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ImportCSVRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public List<ImportCSV> ImportCSV(string file)
        {
            //var path = file.OpenReadStream();
            using (var reader = new StreamReader(file, Encoding.GetEncoding("iso-8859-1")))
            {
                List<ImportCSV> importCSVs = new List<ImportCSV>();

                while (!reader.EndOfStream)
                {
                    ImportCSV import = new ImportCSV();
                    var linha = reader.ReadLine();
                    var valores = linha.Split(';');
                    import.Company = valores[0];
                    import.Name = valores[1];
                    import.Phone = valores[2];
                    import.Email = valores[3];
                    import.Adress = valores[4];
                    import.ContactName = valores[5];

                    importCSVs.Add(import);
                }

                return importCSVs;
            }

        }
        public async Task Save(ImportCSV importCSV)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                //TODO: Passar todos as colunas para os parametros e fazer o insert para cada ID

                var dao = new ImportCSVDao(importCSV);

                if (importCSV.CompanyId == 0)
                {
                    dao.CompanyId = await connection.InsertAsync(dao);
                }
                    var paramDetails = new DynamicParameters();
                paramDetails.Add("@Company", importCSV.Company);
                paramDetails.Add("@Name", importCSV.Name);
                paramDetails.Add("@Phone", importCSV.Phone);
                paramDetails.Add("@Email", importCSV.Email);
                paramDetails.Add("@Adress", importCSV.Adress);
               // paramDetails.Add("@ContactName", importCSV.ContactName);

                var sql = new StringBuilder();
                //sql.AppendLine("INSERT INTO Company VALUES (@Company)");
                sql.AppendLine("INSERT INTO Contact VALUES (@Name, @Phone,@Email,@Adress)");
                //sql.AppendLine("INSERT INTO ContactBook VALUES (@ContactName)");
                await connection.ExecuteAsync(sql.ToString(), new { paramDetails }, transaction);

                transaction.Commit();
            }
            catch (SqliteException ex)
            {
                transaction.Rollback();
                throw new System.Exception(ex.Message);
            }
        }
        //private List<SqliteParameter> GetParameters ()
        //{
        //    List<SqliteParameter> parameters = new List<SqliteParameter>();
        //    parameters.Add(new SqliteParameter("@Idf_Acao_Administrador"));

        //}

        [Table("Company")]
        public class ImportCSVDao : IImportCSV
        {
            [Key]
            public int CompanyId { get; set; }
            [Key]
            public int ContactBookId { get; set; }
            [Key]
            public int ContactId { get; set; }
            public string Company { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Adress { get; set; }
            public string ContactName { get; set; }

            public ImportCSVDao()
            {
            }

            public ImportCSVDao(IImportCSV importCSV)
            {
                CompanyId = importCSV.CompanyId;
                ContactBookId = importCSV.ContactBookId;
                ContactBookId = importCSV.ContactId;
                Company = importCSV.Company;
                Name = importCSV.Name;
                Phone = importCSV.Phone;
                Email = importCSV.Email;
                Adress = importCSV.Adress;
                ContactName = importCSV.ContactName;

            }

            public IImportCSV Export() => new ImportCSV(CompanyId, ContactBookId, ContactId, Company, Name, Phone, Email, Adress, ContactName);
        }
    }
}


