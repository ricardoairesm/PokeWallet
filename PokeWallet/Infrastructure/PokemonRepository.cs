using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokeWallet.Infrastructure
{
    public class PokemonRepository
    {
        public bool Add(Pokemon pokemon, DbConnection DbConnection)
        {
            int result = 0;
            try
            {
                DbConnection connection = DbConnection;

                string query = @"INSERT INTO pokemon(name,""pokeId"",type,sprite) VALUES (@name,@pokeId,@type,@sprite)";
                result = connection.Connection.Execute(sql: query, param: pokemon);

                return result == 1;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public bool Update(Pokemon pokemon, DbConnection DbConnection)
        {
            int result = 0;
            try 
            {
                DbConnection connection = DbConnection;

                string query = $"UPDATE pokemon SET nickname = '{pokemon.Nickname}' WHERE id = {pokemon.Id};";
                result = connection.Connection.Execute(sql: query, param: pokemon);

                return result == 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public List<Pokemon> Get(DbConnection DbConnection)
        {
            List<Pokemon> pokemon = new List<Pokemon>();
            try
            {
                DbConnection connection = DbConnection;
                string query = @"SELECT * FROM pokemon";
                var pokemons = connection.Connection.Query<Pokemon>(sql: query);

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
