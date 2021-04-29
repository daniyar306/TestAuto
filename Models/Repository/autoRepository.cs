using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuto.Models
{
    public class autoRepository : IRepository<auto>
    {
        private string connectionString;
        public autoRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(auto item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO auto (brand,model) VALUES(@brand,@model)", item);
            }

        }

        public async Task<IEnumerable<auto>> FindAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<auto>("SELECT * FROM auto");
            }
        }
        public IEnumerable<auto> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<auto>("SELECT * FROM auto");
            }
        }

        public auto FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<auto>("SELECT * FROM auto WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM auto WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(auto item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE auto SET brand = @brand,  model  = @model WHERE id = @Id", item);
            }
        }
        public async Task UpdateAsync(auto item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                await dbConnection.QueryAsync("UPDATE auto SET brand = @brand,  model  = @model WHERE id = @Id", item);
            }
        }
    }
}
