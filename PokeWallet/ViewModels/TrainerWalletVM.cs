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
        private ICommand updatePokemon;
        private PokeWalletRepository pokeWalletRepository;
        private DbConnection connection;
        public Pokemon PokemonSelecionado { get; set; }
        public TrainerWalletVM(Trainer TreinadorSelecionado)
        {
            pokeList = new ObservableCollection<Pokemon>();
            connection = new DbConnection();
            pokeWalletRepository = new PokeWalletRepository();
            Owner = TreinadorSelecionado;
            IniciaComandos();
        }

        public ObservableCollection<Pokemon> PokeList { get { return pokeList; } private set { pokeList = value; } }
        public PokeWalletRepository PokeWalletRepository { get {  return pokeWalletRepository; } }
        public DbConnection Connection { get { return connection; } }
        public ICommand RemovePokemon { get { return removePokemon; } private set { removePokemon = value; } }
        public ICommand UpdatePokemon { get { return updatePokemon; } private set { updatePokemon = value; } }

        public void IniciaComandos()
        {
            RemovePokemon = new RelayCommand((object _) => {
                if (PokemonSelecionado != null)
                {
                    Owner.Release(PokemonSelecionado.Id);
                    pokeWalletRepository.Remove(Owner.Id, PokemonSelecionado.Id, connection);
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
