using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace PokeWallet.Infrastructure
{
    public class DbDataAccess : IDbDataAccess
    {
        public List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new NpgsqlConnection(connectionString()))
            {
                var output = cnn.Query<T>(sql, new DynamicParameters());
                return output.ToList();
            }
        }

        public void SaveData<T>(T data, string sql)
        {
            using (IDbConnection cnn = new NpgsqlConnection(connectionString()))
            {
                cnn.Execute(sql, data);
            }
        }

        public void UpdateData<T>(T data, string sql)
        {
            using (IDbConnection cnn = new NpgsqlConnection(connectionString()))
            {
                cnn.Execute(sql, data);
            }
        }

        public void RemoveData<T>(T data,string sql)
        {
            using (IDbConnection cnn = new NpgsqlConnection(connectionString()))
            {
                cnn.Execute(sql);
            }
        }

        public string connectionString()
        {
            return "Server=localhost;Port=5555;Database=pokemondb;User Id=postgres;Password=123456789;";
        }
    }
}
