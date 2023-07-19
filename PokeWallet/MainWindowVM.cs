using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeWallet
{
    public class MainWindowVM
    {
        public ObservableCollection<Pokemon> pokeList { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Update { get; private set; }
        public Pokemon PokemonSelecionado { get; set; }
        public MainWindowVM()
        {
            pokeList = new ObservableCollection<Pokemon>()
            {
                new Pokemon("lucario",448,"steel")
            };
            IniciaComandos();
        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) => {
                Pokemon newPokemon = new Pokemon();
                AddPokemonWindow telaDeCadastro = new AddPokemonWindow();
                telaDeCadastro.DataContext = newPokemon;
                telaDeCadastro.ShowDialog();
                pokeList.Add(new Pokemon(newPokemon.Name,newPokemon.Id,newPokemon.Type));
            });
            Remove = new RelayCommand((object _) => {
                pokeList.Remove(PokemonSelecionado);
            });

            Update = new RelayCommand((object _) => {
                AddPokemonWindow telaDeUpdate = new AddPokemonWindow();
                telaDeUpdate.DataContext = PokemonSelecionado;
                telaDeUpdate.ShowDialog();
                PokemonSelecionado.UpdateSprite();
            });
        }
    }
}
