using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokeWallet.Infrastructure
{
    public class PostgresConnection : IDbConnection
    {
        public NpgsqlConnection Connection { get; set; }
        public PostgresConnection()
        {
            try
            {
                Connection = new NpgsqlConnection("Server=localhost;Port=5555;Database=pokemondb;User Id=postgres;Password=123456789;");
                Connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public bool AddPokemon(Pokemon pokemon)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;

                string query = @"INSERT INTO pokemon(name,""pokeId"",type,sprite) VALUES (@name,@pokeId,@type,@sprite)";
                result = connection.Execute(sql: query, param: pokemon);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public bool RemovePokemon(Pokemon pokemon)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;

                string query = $"DELETE FROM pokemon WHERE id = {pokemon.Id}";
                result = connection.Execute(sql: query);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;

                string query = $"UPDATE pokemon SET nickname = '{pokemon.Nickname}' WHERE id = {pokemon.Id};";
                result = connection.Execute(sql: query, param: pokemon);

                return result == 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public List<Pokemon> GetPokemon()
        {
            List<Pokemon> pokemon = new List<Pokemon>();
            try
            {
                NpgsqlConnection connection = Connection;
                string query = @"SELECT * FROM pokemon";
                var pokemons = connection.Query<Pokemon>(sql: query).OrderBy(p => p.Id);

                return pokemons.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return pokemon;
            }

        }

        public bool AddPokeWalletInstance(int trainerId, int pokemonId)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;

                string query = $@"INSERT INTO ""pokeWallet""(""pokemonId"",""trainerId"") values({pokemonId}, {trainerId})";
                result = connection.Execute(sql: query);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }
        public void GetPokeWallet(Trainer trainer)
        {
            List<Trainer> pokemon = new List<Trainer>();
            try
            {
                NpgsqlConnection connection = Connection;
                string query = @"SELECT ""pokemonId"" FROM ""pokeWallet"" WHERE ""trainerId"" = @Id";
                var pokemons = connection.Query<PokeWalletDb>(sql: query, param: trainer);

                foreach (PokeWalletDb pokeWalletDb in pokemons)
                {
                    trainer.PokeWallet.Add(pokeWalletDb.PokemonId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool RemovePokeWalletInstance(int trainerId, int pokemonId)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;

                string query = $@"DELETE FROM ""pokeWallet"" WHERE ""pokemonId"" = {pokemonId} AND ""trainerId"" = {trainerId}";
                result = connection.Execute(sql: query);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public bool ClearTrainerPokeWallet(int trainerId)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;

                string query = $@"DELETE FROM ""pokeWallet"" WHERE ""trainerId"" = {trainerId}";
                result = connection.Execute(sql: query);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public bool AddTrainer(Trainer trainer)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;


                string query = @"INSERT INTO trainer(name, age) values(@name, @age)";
                result = connection.Execute(sql: query, param: trainer);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public bool RemoveTrainer(Trainer trainer)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;

                string query = $"DELETE FROM trainer WHERE id = {trainer.Id}";
                result = connection.Execute(sql: query);

                return result == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public bool UpdateTrainer(Trainer trainer)
        {
            int result = 0;
            try
            {
                NpgsqlConnection connection = Connection;

                string query = $"UPDATE trainer SET name = '{trainer.Name}' age = '{trainer.Age}' WHERE id = {trainer.Id};";
                result = connection.Execute(sql: query, param: trainer);

                return result == 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return result == 0;
            }
        }

        public List<Trainer> GetTrainers()
        {
            List<Trainer> pokemon = new List<Trainer>();
            try
            {
                NpgsqlConnection connection = Connection;
                string query = @"SELECT * FROM trainer";
                var trainers = connection.Query<Trainer>(sql: query);

                return trainers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return pokemon;
            }
        }
    }
}
