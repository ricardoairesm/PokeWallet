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
        private ICommand updatePokemon;
        public Pokemon PokemonSelecionado { get; set; }
        public TrainerWalletVM(Trainer TreinadorSelecionado)
        {
            pokeList = new ObservableCollection<Pokemon>();
            Owner = TreinadorSelecionado;
            IniciaComandos();
        }

        public ObservableCollection<Pokemon> PokeList { get { return pokeList; } private set { pokeList = value; } }
        public ICommand RemovePokemon { get { return removePokemon; } private set { removePokemon = value; } }
        public ICommand UpdatePokemon { get { return updatePokemon; } private set { updatePokemon = value; } }

        public void IniciaComandos()
        {
            RemovePokemon = new RelayCommand((object _) => {
                if (PokemonSelecionado != null)
                {
                    Owner.PokeWallet.Remove(PokemonSelecionado.Id);
                    pokeList.Remove(PokemonSelecionado);
                }
            });

            UpdatePokemon = new RelayCommand((object _) => {
                if(PokemonSelecionado != null)
                {
                    PokemonUpdateInfoInput telaDeUpdate = new PokemonUpdateInfoInput();
                    telaDeUpdate.DataContext = PokemonSelecionado;
                    telaDeUpdate.ShowDialog();
                }
            });

        }
    }
}
