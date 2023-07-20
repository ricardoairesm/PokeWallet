using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeWallet
{
    public class MainWindowVM
    {
        private ICommand add;
        private ICommand remove;
        private ICommand update;
        private ICommand catchPokemon;
        public Game Pokemon;
        public ObservableCollection<Pokemon> Pokedex { get; set; }
        public MainWindowVM()
        {
            Pokemon = new Game();
            Pokedex = Pokemon.PokeList;
            IniciaComandos();
        }
        public ICommand Add { get { return add; } set { add = value; } }
        public ICommand Remove { get { return remove; }  set { remove = value; } }
        public ICommand Update { get { return update; } set { update = value; } }
        public ICommand CatchPokemon { get { return catchPokemon; }  set { catchPokemon = value; } }

        public void IniciaComandos()
        {
            Add = Pokemon.Add;
            Remove = Pokemon.Remove;
            Update = Pokemon.Update;
            CatchPokemon = Pokemon.CatchPokemon;
        }
    }
}
