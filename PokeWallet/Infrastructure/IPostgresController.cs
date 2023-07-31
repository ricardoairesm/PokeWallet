using System.Collections.Generic;

namespace PokeWallet.Infrastructure
{
    public interface IPostgresController
    {
        void AddPokemon(Pokemon pokemon);
        void AddPokeWalletInstance(int trainerId, int pokemonId);
        void AddTrainer(Trainer trainer);
        void ClearTrainerPokeWallet(int trainerId);
        List<Pokemon> GetPokemon();
        void GetPokeWallet(Trainer trainer);
        List<Trainer> GetTrainers();
        void RemovePokemon(Pokemon pokemon);
        void RemovePokeWalletInstance(int trainerId, int pokemonId);
        void RemoveTrainer(Trainer trainer);
        void UpdatePokemon(Pokemon pokemon);
        void UpdateTrainer(Trainer trainer);
    }
}