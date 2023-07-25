using PokeWallet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PokeWallet
{
    public class MainWindowVM
    {
        private BindingList<Pokemon> pokeList;
        private Pokedex pokedex;
        private IOrderedEnumerable<Pokemon> pokemonDbList;
        private ObservableCollection<Trainer> trainerList;
        private Trainer ash;
        private DbConnection dbConnection;
        private PokemonRepository pokemonRepository;
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
        public MainWindowVM()
        {
            dbConnection = new DbConnection();
            pokemonRepository = new PokemonRepository();
            pokedex = new Pokedex();
            IdForTrainers = 1;
            TrainerList = new ObservableCollection<Trainer>();
            pokeList = new BindingList<Pokemon>();
            ObterPokemons();
            IdForPokemons = 14;
            Ash = new Trainer(IdForTrainers, "Ash");
            TrainerList.Add(Ash);
            Ash.Catch(2);
            IdForTrainers++;
            IniciaComandos();
        }

        public MainWindowVM(int random)
        {
            pokeList = new BindingList<Pokemon>();
        }

        public BindingList<Pokemon> PokeList { get { return pokeList; } private set { pokeList = value; } }
        public Pokedex Pokedex { get { return pokedex; } private set { pokedex = value; } }
        public IOrderedEnumerable<Pokemon> PokemonDbList { get { return pokemonDbList; } private set { pokemonDbList = value; } }
        public Trainer Ash { get { return ash; } private set { ash = value; } }
        public DbConnection DbConnection { get { return dbConnection; } private set { dbConnection = value; } }
        public PokemonRepository PokemonRepository { get { return pokemonRepository; } private set { pokemonRepository = value; } }
        public ICommand AddPokemon { get { return addPokemon; } private set { addPokemon = value; } }
        public ICommand RemovePokemon { get { return removePokemon; } private set { removePokemon = value; } }
        public ICommand UpdatePokemon { get { return updatePokemon; } private set { updatePokemon = value; } }
        public ICommand CatchPokemon { get { return catchPokemon; } private set { catchPokemon = value; } }
        public ICommand AddTreinador { get { return addTreinador; } private set { addTreinador = value; } }
        public ICommand RemoveTreinador { get { return removeTreinador; } private set { removeTreinador = value; } }
        public ICommand UpdateTreinador { get { return updateTreinador; } private set { updateTreinador = value; } }
        public ICommand ShowTrainerWallet { get { return showTrainerWallet; } private set { showTrainerWallet = value; } }
        public ObservableCollection<Trainer> TrainerList { get { return trainerList; } private set { trainerList = value; } }


        private async void ObterPokemons()
        {
            PokemonRepository repository = pokemonRepository;
            pokemonDbList = repository.Get(dbConnection).OrderBy(pokemon => pokemon.Id);
            foreach(Pokemon pokemon in pokemonDbList)
            {
                PokeList.Add(pokemon);
            }
        }

        public void IniciaComandos()
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
                }

                Pokedex.pokeNameSelecionado = null;
                IdForPokemons++;
            });

            RemovePokemon = new RelayCommand((object _) => {
                pokeList.Remove(PokemonSelecionado);
            }, (object _) => {
                bool verifica = PokemonSelecionado != null;
                return (verifica);
            });

            UpdatePokemon = new RelayCommand((object _) => {

                if (PokemonSelecionado != null)
                {
                    PokemonUpdateInfoInput telaDeUpdate = new PokemonUpdateInfoInput();
                    PokemonRepository repository = pokemonRepository;
                    Pokemon UpdateHolder = PokemonSelecionado.ShallowCopy();
                    telaDeUpdate.DataContext = UpdateHolder;
                    bool? verifica = telaDeUpdate.ShowDialog();
                    if (verifica.HasValue && verifica.Value)
                    {
                        PokemonSelecionado.UpdateNickname(UpdateHolder.Nickname);
                        repository.Update(PokemonSelecionado, DbConnection);
                    }
                }

            }, (object _) => PokemonSelecionado != null);

            CatchPokemon = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null && PokemonSelecionado != null)
                {
                    if ( TreinadorSelecionado.PokeWallet.ToArray().Length == 6)
                    {
                        PokemonSelecionado = null;
                        MessageBox.Show("You can't add more than 6 pokemon to a team","Max Team Size");
                    }
                    if (TreinadorSelecionado.PokeWallet.Contains(PokemonSelecionado.Id))
                    {
                        PokemonSelecionado = null;
                        MessageBox.Show("You can't add the same pokemon more than once to a team", "Repeated Pokemon");
                    }
                    else
                    {
                        TreinadorSelecionado.Catch(PokemonSelecionado.Id);
                    }
                }

            }, (object _) => {
                bool verifica = TreinadorSelecionado != null && PokemonSelecionado != null;
                return (verifica);
            });

            AddTreinador = new RelayCommand((object _) => {

                Trainer newTrainer = new Trainer();

                TrainerInfoInput telaDeCadastro = new TrainerInfoInput();
                telaDeCadastro.DataContext = newTrainer;
                telaDeCadastro.ShowDialog();

                TrainerList.Add(new Trainer(IdForTrainers, newTrainer.Name));
                IdForTrainers++;

            });

            RemoveTreinador = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null)
                {
                    trainerList.Remove(TreinadorSelecionado);
                    IdForTrainers--;
                }


            }, (object _) => {
                bool verifica = TreinadorSelecionado != null;
                return (verifica);
            });

            UpdateTreinador = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null)
                {
                    PokemonUpdateInfoInput telaDeUpdate = new PokemonUpdateInfoInput();
                    Trainer UpdateHolder = new Trainer();
                    telaDeUpdate.DataContext = UpdateHolder;
                    bool? verifica = telaDeUpdate.ShowDialog();
                    if (verifica.HasValue && verifica.Value)
                    {
                        TreinadorSelecionado.UpdateName(UpdateHolder.Name);
                    }
                }

            }, (object _) => {
                bool verifica = TreinadorSelecionado != null;
                return (verifica);
            });

            ShowTrainerWallet = new RelayCommand((object _) =>
            {

                if (TreinadorSelecionado != null)
                {
                    TrainerWalletVM screenContext = new TrainerWalletVM(TreinadorSelecionado);

                    foreach (Pokemon p in PokeList)
                    {
                        foreach (int id in TreinadorSelecionado.PokeWallet)
                        {
                            if (p.Id == id) screenContext.PokeList.Add(p);
                        }

                    }

                    TrainerWallet telaTrainerWallet = new TrainerWallet();
                    telaTrainerWallet.DataContext = screenContext;
                    telaTrainerWallet.ShowDialog();
                }
            }, (object _) => {
                bool verifica = TreinadorSelecionado != null;
                return (verifica);
            });

        }
    }
}
