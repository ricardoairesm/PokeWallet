using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokeWallet.Infrastructure
{
    public class PokeWalletRepository
    {
        public bool Add(int trainerId, int pokemonId ,DbConnection DbConnection)
        {
            int result = 0;
            try
            {
                DbConnection connection = DbConnection;

                string query = $@"INSERT INTO ""pokeWallet""(""pokemonId"",""trainerId"") values({pokemonId}, {trainerId})";
                result = connection.Connection.Execute(sql: query);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }
        public void Get(Trainer trainer,DbConnection DbConnection)
        {
            List<Trainer> pokemon = new List<Trainer>();
            try
            {
                DbConnection connection = DbConnection;
                string query = @"SELECT ""pokemonId"" FROM ""pokeWallet"" WHERE ""trainerId"" = @Id";
                var pokemons = connection.Connection.Query<PokeWalletDb>(sql: query,param:trainer);

                foreach(PokeWalletDb pokeWalletDb in pokemons)
                {
                    trainer.PokeWallet.Add(pokeWalletDb.PokemonId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
