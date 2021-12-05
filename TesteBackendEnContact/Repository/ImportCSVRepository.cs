using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain;
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
                    import.Comany = valores[0];
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
                var paramDetails = new DynamicParameters();
                paramDetails.Add("@Company");
                paramDetails.Add("@Name");
                paramDetails.Add("@Phone");
                paramDetails.Add("@Email");
                paramDetails.Add("@Adress");
                paramDetails.Add("@ContactName");


                //List<SqliteParameter> sqliteParameter = new List<SqliteParameter>();
                //    sqliteParameter[0].ParameterName = "@Company";
                //    sqliteParameter[0].Value = importCSV.Comany;
                //    sqliteParameter[1].ParameterName = "@Name";
                //    sqliteParameter[1].Value = importCSV.Name;
                //    sqliteParameter[2].ParameterName = "@Phone";
                //    sqliteParameter[2].Value = importCSV.Phone;
                //    sqliteParameter[3].ParameterName = "@Email";
                //    sqliteParameter[3].Value = importCSV.Email;
                //    sqliteParameter[4].ParameterName = "@Adress";
                //    sqliteParameter[4].Value = importCSV.Adress;
                //    sqliteParameter[5].ParameterName = "@ContactName";
                //    sqliteParameter[5].Value = importCSV.ContactName;


                var sql = new StringBuilder();
                sql.AppendLine("INSERT INTO Company VALUES (@Company)");
                sql.AppendLine("INSERT INTO Contact VALUES (@Name, @Phone,@Email,@Adress)");
                sql.AppendLine("INSERT INTO ContactBook VALUES (@ContactName)");
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
    }
}


