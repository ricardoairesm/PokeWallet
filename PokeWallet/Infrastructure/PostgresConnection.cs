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
    public class PostgresConnection:IPostgresController
    {
        IDbDataAccess _database;
        public PostgresConnection(IDbDataAccess database)
        {
            _database = database;
        }

        public void AddPokemon(Pokemon pokemon)
        {
            try
            {
                string query = @"INSERT INTO pokemon(name,""pokeId"",type,sprite) VALUES (@name,@pokeId,@type,@sprite)";
                _database.SaveData(pokemon, query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RemovePokemon(Pokemon pokemon)
        {
            try
            {
                string query = $"DELETE FROM pokemon WHERE id = {pokemon.Id}";
                _database.RemoveData(pokemon,query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdatePokemon(Pokemon pokemon)
        {
        
            try
            {
                string query = $"UPDATE pokemon SET nickname = '{pokemon.Nickname}' WHERE id = {pokemon.Id};";
                _database.UpdateData(pokemon, query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<Pokemon> GetPokemon()
        {
            List<Pokemon> pokemon = new List<Pokemon>();
            try
            {
                string query = @"SELECT * FROM pokemon";
                var pokemons = _database.LoadData<Pokemon>(query);

                return pokemons;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return pokemon;
            }

        }

        public void AddPokeWalletInstance(int trainerId, int pokemonId)
        {
            try
            {
                string query = $@"INSERT INTO ""pokeWallet""(""pokemonId"",""trainerId"") values({pokemonId}, {trainerId})";
                _database.SaveData(trainerId, query);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void GetPokeWallet(Trainer trainer)
        {
            try
            {
                string query = $@"SELECT ""pokemonId"" FROM ""pokeWallet"" WHERE ""trainerId"" = {trainer.Id}";
                var output = _database.LoadData<PokeWalletDb>(query);

                foreach (PokeWalletDb pokeWalletDb in output)
                {
                    trainer.PokeWallet.Add(pokeWalletDb.PokemonId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RemovePokeWalletInstance(int trainerId, int pokemonId)
        {
            try
            {
                string query = $@"DELETE FROM ""pokeWallet"" WHERE ""pokemonId"" = {pokemonId} AND ""trainerId"" = {trainerId}";
                _database.RemoveData(trainerId,query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ClearTrainerPokeWallet(int trainerId)
        {
            try
            {
                string query = $@"DELETE FROM ""pokeWallet"" WHERE ""trainerId"" = {trainerId}";
                _database.RemoveData(trainerId, query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddTrainer(Trainer trainer)
        {
            try
            {
                string query = @"INSERT INTO trainer(name, age) values(@name, @age)";
                _database.SaveData(trainer, query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RemoveTrainer(Trainer trainer)
        {
            try
            {
                string query = $"DELETE FROM trainer WHERE id = {trainer.Id}";
                _database.RemoveData(trainer, query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateTrainer(Trainer trainer)
        {
            try
            {
                string query = $"UPDATE trainer SET name = '{trainer.Name}' age = '{trainer.Age}' WHERE id = {trainer.Id};";
                _database.UpdateData(trainer, query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<Trainer> GetTrainers()
        {
            List<Trainer> emptyTrainerList = new List<Trainer>();
            try
            {
                string query = @"SELECT * FROM trainer";
                var trainers = _database.LoadData<Trainer>(query);

                return trainers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return emptyTrainerList;
            }
        }
    }
}
