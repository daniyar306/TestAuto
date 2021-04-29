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
    public class driverRepository : IRepository<driver>
    {
        private string connectionString;
        public driverRepository(IConfiguration configuration)
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

        public void Add(driver item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.ExecuteAsync("INSERT INTO driver (name,auto_id) VALUES(@name,@auto_id)", item);
            }

        }

        public IEnumerable<driver> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var sql = "SELECT d.id,d.name,d.auto_id,a.model" +
                   " FROM driver as d" +
                   " inner join auto as a " +
                   "on d.auto_id=a.id";
                return dbConnection.Query<driver, auto, driver>(sql, (driver, auto) => {
                               driver.auto = auto;
                               return driver;
                                    }, splitOn: "auto_id");

              
            }
        }
        public async Task<IEnumerable<driver>> FindAllAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var sql = "SELECT d.id,d.name,d.auto_id,a.model" +
                   " FROM driver as d" +
                   " inner join auto as a " +
                   "on d.auto_id=a.id";
                return await dbConnection.QueryAsync<driver, auto, driver>(sql, (driver, auto) => {
                    driver.auto = auto;
                    return driver;
                }, splitOn: "auto_id");


            }
        }

        public driver FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<driver>("SELECT * FROM driver driver WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.ExecuteAsync("DELETE FROM driver WHERE Id=@Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(driver item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
               await dbConnection.QueryAsync("UPDATE driver SET name = @Name,  auto_id= @auto_id WHERE id = @Id", item);
            }
        }
        public void Update(driver item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE driver SET name = @Name,  auto_id= @auto_id WHERE id = @Id", item);
            }
        }

    }
}
