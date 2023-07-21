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
        public int IdForTrainers { get; set; }
        public int IdForPokemons { get; set; }
        public Game()
        {
            IdForTrainers = 1;
            TrainerList = new ObservableCollection<Trainer>();
            pokeList = new ObservableCollection<Pokemon>()
            {   
                new Pokemon(25,1),
                new Pokemon(6,2),
                new Pokemon(38,3),
                new Pokemon(445,4),
                new Pokemon(448,5),
                new Pokemon(145,6),
                new Pokemon(150,7),
                new Pokemon(149,8),
                new Pokemon(895,9),
                new Pokemon(894,10),
                new Pokemon(379,11),
                new Pokemon(378,12),
                new Pokemon(889,13),
            };
            IdForPokemons = 14;
            Ash = new Trainer(IdForTrainers,"Ash");
            TrainerList.Add(Ash);
            Ash.Catch(1);
            IdForTrainers++;
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
                    pokeList.Add(new Pokemon(newPokemon.PokeId,IdForPokemons));
                }

                IdForPokemons++;
            });

            RemovePokemon = new RelayCommand((object _) => {
                pokeList.Remove(PokemonSelecionado);
            });

            UpdatePokemon = new RelayCommand((object _) => {

                if (PokemonSelecionado != null)
                {
                    PokemonUpdateInfoInput telaDeUpdate = new PokemonUpdateInfoInput();
                    telaDeUpdate.DataContext = PokemonSelecionado;
                    bool? verifica = telaDeUpdate.ShowDialog();
                }

            });

            CatchPokemon = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null && PokemonSelecionado != null)
                {
                    if (TreinadorSelecionado.PokeWallet.Contains(PokemonSelecionado.Id) || TreinadorSelecionado.PokeWallet.ToArray().Length == 6)
                    {
                        PokemonSelecionado = null;
                    }
                    else
                    {
                        TreinadorSelecionado.Catch(PokemonSelecionado.Id);
                    }
                }

            });

            AddTreinador = new RelayCommand((object _) => {

                Trainer newTrainer = new Trainer();

                TrainerInfoInput telaDeCadastro = new TrainerInfoInput();
                telaDeCadastro.DataContext = newTrainer;
                telaDeCadastro.ShowDialog();

                TrainerList.Add(new Trainer(IdForTrainers,newTrainer.Name));
                IdForTrainers++;

            });

            RemoveTreinador = new RelayCommand((object _) => {

                if(TreinadorSelecionado != null)
                {
                    trainerList.Remove(TreinadorSelecionado);
                    IdForTrainers--;
                }

            });

            ShowTrainerWallet = new RelayCommand((object _) =>
            {

                if(TreinadorSelecionado != null)
                {
                    TrainerWalletVM screenContext = new TrainerWalletVM(TreinadorSelecionado);

                    foreach(Pokemon p in PokeList) 
                    {
                        foreach(int id in TreinadorSelecionado.PokeWallet)
                        {
                            if (p.Id == id) screenContext.PokeList.Add(p);
                        }

                    }

                    TrainerWallet telaTrainerWallet = new TrainerWallet();
                    telaTrainerWallet.DataContext = screenContext;
                    telaTrainerWallet.ShowDialog();
                }
            });

        }
    }
}
