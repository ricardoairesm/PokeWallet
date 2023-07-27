using PokeWallet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeWallet
{
    public class TrainerWalletVM
    {
        public Trainer Owner { get; set; }
        private ObservableCollection<Pokemon> pokeList;
        private ICommand removePokemon;
        private PostgresConnection connection;
        public Pokemon PokemonSelecionado { get; set; }
        public TrainerWalletVM(Trainer TreinadorSelecionado)
        {
            pokeList = new ObservableCollection<Pokemon>();
            connection = new PostgresConnection();
            Owner = TreinadorSelecionado;
            IniciaComandos();
        }

        public ObservableCollection<Pokemon> PokeList { get { return pokeList; } private set { pokeList = value; } }
        public PostgresConnection Connection { get { return connection; } }
        public ICommand RemovePokemon { get { return removePokemon; } private set { removePokemon = value; } }

        public void IniciaComandos()
        {
            RemovePokemon = new RelayCommand((object _) => {
                if (PokemonSelecionado != null)
                {
                    Owner.Release(PokemonSelecionado.Id);
                    connection.RemovePokeWalletInstance(Owner.Id, PokemonSelecionado.Id);
                    pokeList.Remove(PokemonSelecionado);
                }
            });
        }
    }
}
