using System.Collections.Generic;

namespace PokeWallet.Infrastructure
{
    public interface IDbConnection
    {
        bool AddPokemon(Pokemon pokemon);
        bool AddPokeWalletInstance(int trainerId, int pokemonId);
        bool AddTrainer(Trainer trainer);
        bool ClearTrainerPokeWallet(int trainerId);
        List<Pokemon> GetPokemon();
        void GetPokeWallet(Trainer trainer);
        List<Trainer> GetTrainers();
        bool RemovePokemon(Pokemon pokemon);
        bool RemovePokeWalletInstance(int trainerId, int pokemonId);
        bool RemoveTrainer(Trainer trainer);
        bool UpdatePokemon(Pokemon pokemon);
        bool UpdateTrainer(Trainer trainer);
    }
}