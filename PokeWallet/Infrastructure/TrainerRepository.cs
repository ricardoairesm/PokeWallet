using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokeWallet.Infrastructure
{
    public class TrainerRepository
    {
        public bool Add(Trainer trainer, DbConnection DbConnection)
        {
            int result = 0;
            try
            {
                DbConnection connection = DbConnection;
                

                string query = @"INSERT INTO trainer(name, age) values(@name, @age)";
                result = connection.Connection.Execute(sql: query, param: trainer);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public List<Trainer> Get(DbConnection DbConnection)
        {
            List<Trainer> pokemon = new List<Trainer>();
            try
            {
                DbConnection connection = DbConnection;
                string query = @"SELECT * FROM trainer";
                var pokemons = connection.Connection.Query<Trainer>(sql: query);

                return pokemons.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return pokemon;
            }
        }
    }
}
