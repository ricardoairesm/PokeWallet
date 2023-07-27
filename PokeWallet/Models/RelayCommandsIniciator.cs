using PokeWallet.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeWallet.Models
{
    public class RelayCommandsIniciator
    {
        public static void AddPokemon(ICommand AddPokemon, Pokedex Pokedex, PokemonRepository pokemonRepository, BindingList<Pokemon> pokeList, int IdForPokemons, DbConnection DbConnection)
        {
            AddPokemon = new RelayCommand((object Botao) => {
                PokemonInfoInput telaDeCadastro = new PokemonInfoInput();
                PokemonRepository repository = pokemonRepository;

                telaDeCadastro.DataContext = Pokedex;
                bool? verifica = telaDeCadastro.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    pokeList.Add(new Pokemon(Pokedex.pokeNameSelecionado, IdForPokemons));
                    repository.Add(new Pokemon(Pokedex.pokeNameSelecionado, IdForPokemons), DbConnection);
                    IdForPokemons++;
                }
            });
        }
        public static void Remove() { }

    }
}
