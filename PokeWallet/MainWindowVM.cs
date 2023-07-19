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
        public ObservableCollection<int> example { get; set; }
        public Trainer Ash { get; set; }
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Update { get; private set; }
        public ICommand Catch { get; private set; }
        public Pokemon PokemonSelecionado { get; set; }
        public MainWindowVM()
        {
            Ash = new Trainer();
            example = new ObservableCollection<int>();
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
                Ash.Catch(newPokemon.Id);

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
            Catch = new RelayCommand((object _) => {
                Ash.Catch(PokemonSelecionado.Id);
            });

        }
    }
}
