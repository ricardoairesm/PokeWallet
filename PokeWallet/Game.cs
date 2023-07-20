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
    public class Game
    {
        private ObservableCollection<Pokemon> pokeList;
        private ObservableCollection<Trainer> trainerList;
        private Trainer ash;
        private ICommand addPokemon;
        private ICommand removePokemon;
        private ICommand updatePokemon;
        private ICommand catchPokemon;
        private ICommand addTreinador;
        private ICommand removeTreinador;
        private ICommand updateTreinador;
        private ICommand showTrainerWallet;
        public Pokemon PokemonSelecionado { get; set; }
        public Trainer TreinadorSelecionado { get; set; }
        public int Id { get; set; }
        public Game()
        {
            Id = 1;
            TrainerList = new ObservableCollection<Trainer>();
            pokeList = new ObservableCollection<Pokemon>()
            {
                new Pokemon(6),
            };
            Ash = new Trainer(Id,"Ash");
            TrainerList.Add(Ash);
            Ash.Catch(25);
            Id++;
            IniciaComandos();
        }

        public Game(int random)
        {
            pokeList = new ObservableCollection<Pokemon>();
        }

        public ObservableCollection<Pokemon> PokeList { get { return pokeList; } private set { pokeList = value; } }
        public Trainer Ash { get {  return ash; } private set { ash = value; } }
        public ICommand AddPokemon {  get { return addPokemon; } private set { addPokemon = value; } }
        public ICommand RemovePokemon { get { return removePokemon; }  private set { removePokemon = value; } }
        public ICommand UpdatePokemon { get { return updatePokemon; }  private set { updatePokemon = value; } }
        public ICommand CatchPokemon {  get { return catchPokemon; }  private set {  catchPokemon = value; } }
        public ICommand AddTreinador { get { return addTreinador; } private set { addTreinador = value; } }
        public ICommand RemoveTreinador { get { return removeTreinador; } private set { removeTreinador = value; } }
        public ICommand UpdateTreinador { get { return updateTreinador; } private set { updateTreinador = value; } }
        public ICommand ShowTrainerWallet { get { return showTrainerWallet; } private set { showTrainerWallet = value; } }
        public ObservableCollection<Trainer> TrainerList { get { return trainerList; } private set {  trainerList = value; } }

   

        public void IniciaComandos()
        {
            AddPokemon = new RelayCommand((object Botao) => {
                Pokemon newPokemon = new Pokemon();
                PokemonInfoInput telaDeCadastro = new PokemonInfoInput();

                telaDeCadastro.DataContext = newPokemon;
                bool? verifica = telaDeCadastro.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    pokeList.Add(new Pokemon(newPokemon.Id));
                }
            });

            RemovePokemon = new RelayCommand((object _) => {
                pokeList.Remove(PokemonSelecionado);
            });

            UpdatePokemon = new RelayCommand((object _) => {

                if (PokemonSelecionado != null)
                {
                    PokemonUpdateInfoInput telaDeUpdate = new PokemonUpdateInfoInput();
                    telaDeUpdate.DataContext = PokemonSelecionado;
                    telaDeUpdate.ShowDialog();
                }

            });

            CatchPokemon = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null && PokemonSelecionado != null)
                {
                    TreinadorSelecionado.Catch(PokemonSelecionado.Id);
                }

            });

            AddTreinador = new RelayCommand((object _) => {

                Trainer newTrainer = new Trainer();

                TrainerInfoInput telaDeCadastro = new TrainerInfoInput();
                telaDeCadastro.DataContext = newTrainer;
                telaDeCadastro.ShowDialog();

                TrainerList.Add(new Trainer(Id,newTrainer.Name));
                Id++;

            });

            ShowTrainerWallet = new RelayCommand((object _) =>
            {

                if(TreinadorSelecionado != null)
                {
                    TrainerWalletVM screenContext = new TrainerWalletVM(TreinadorSelecionado);

                    foreach(int id in TreinadorSelecionado.PokeWallet) 
                    {
                        screenContext.PokeList.Add(new Pokemon(id));
                    }

                    TrainerWallet telaTrainerWallet = new TrainerWallet();
                    telaTrainerWallet.DataContext = screenContext;
                    telaTrainerWallet.ShowDialog();
                }
            });

        }
    }
}
