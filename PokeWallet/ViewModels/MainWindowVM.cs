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
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PokeWallet
{
    public class MainWindowVM
    {
        private BindingList<Pokemon> pokeList;
        private Pokedex pokedex;
        private IOrderedEnumerable<Pokemon> pokemonDbList;
        private IOrderedEnumerable<Trainer> trainerDbList;
        private ObservableCollection<Trainer> trainerList;
        private DbConnection dbConnection;
        private PokemonRepository pokemonRepository;
        private TrainerRepository trainerRepository;
        private PokeWalletRepository pokeWalletRepository;
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
            trainerRepository = new TrainerRepository();
            pokeWalletRepository = new PokeWalletRepository();
            pokedex = new Pokedex();
            TrainerList = new ObservableCollection<Trainer>();
            pokeList = new BindingList<Pokemon>();
            ObterPokemons();
            ObterTrainers();
            IniciaComandos();
        }

        public MainWindowVM(int random)
        {
            pokeList = new BindingList<Pokemon>();
        }

        public BindingList<Pokemon> PokeList { get { return pokeList; } private set { pokeList = value; } }
        public Pokedex Pokedex { get { return pokedex; } private set { pokedex = value; } }
        public IOrderedEnumerable<Pokemon> PokemonDbList { get { return pokemonDbList; } private set { pokemonDbList = value; } }
        public IOrderedEnumerable<Trainer> TrainerDbList { get { return trainerDbList; } private set { trainerDbList = value; } }
        public DbConnection DbConnection { get { return dbConnection; } private set { dbConnection = value; } }
        public PokemonRepository PokemonRepository { get { return pokemonRepository; } private set { pokemonRepository = value; } }
        public TrainerRepository TrainerRepository { get { return trainerRepository; } private set { trainerRepository = value; } }
        public PokeWalletRepository PokeWalletRepository { get { return pokeWalletRepository; } }
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
            try
            {
                PokemonRepository repository = pokemonRepository;
                pokemonDbList = repository.Get(dbConnection).OrderBy(pokemon => pokemon.Id);

                foreach (Pokemon pokemon in pokemonDbList)
                {
                    if(pokemon == pokemonDbList.Last())
                    {
                        IdForPokemons = pokemon.Id + 1;
                    }
                    PokeList.Add(pokemon);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private async void ObterTrainers()
        {
            try 
            {
                TrainerRepository Trepository = trainerRepository;
                PokeWalletRepository PWrepository = new PokeWalletRepository();
                trainerDbList = Trepository.Get(dbConnection).OrderBy(trainer => trainer.Id);

                foreach (Trainer trainer in trainerDbList)
                {
                    TrainerList.Add(new Trainer(trainer));
                    if(trainer == trainerDbList.Last())
                    {
                        IdForTrainers = trainer.Id + 1;
                    }
                }

                foreach (Trainer trainer in TrainerList)
                {
                    PWrepository.Get(trainer, DbConnection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public void IniciaComandos()
        {
            //AddPokemon = new RelayCommand((object Botao) => {
              //  PokemonInfoInput telaDeCadastro = new PokemonInfoInput();
                //PokemonRepository repository = pokemonRepository;

                //telaDeCadastro.DataContext = Pokedex;
                //bool? verifica = telaDeCadastro.ShowDialog();

               // if (verifica.HasValue && verifica.Value)
                //{
                  //  pokeList.Add(new Pokemon(Pokedex.pokeNameSelecionado, IdForPokemons));
                    //repository.Add(new Pokemon(Pokedex.pokeNameSelecionado, IdForPokemons), DbConnection);
                    //IdForPokemons++;
               // }
            //});

            RemovePokemon = new RelayCommand((object _) => {

                foreach(Trainer trainer in TrainerList)
                {
                    if (trainer.PokeWallet.Contains(PokemonSelecionado.Id))
                    {
                        MessageBox.Show("You can't remove pokemon that are on a team!", "Deleting pokemon in use");
                        return;
                    }
                }

                PokemonRepository repository = pokemonRepository;

                repository.Remove(PokemonSelecionado, DbConnection);
                pokeList.Remove(PokemonSelecionado);

            }, (object _) => PokemonSelecionado != null);

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
                    if (TreinadorSelecionado.PokeWallet.ToArray().Length == 6)
                    {
                        MessageBox.Show("You can't add more than 6 pokemon to a team", "Max Team Size");
                        return;
                    }
                    if (TreinadorSelecionado.PokeWallet.Contains(PokemonSelecionado.Id))
                    {
                        MessageBox.Show("You can't add the same pokemon more than once to a team", "Repeated Pokemon");
                        return;
                    }
                    if (TreinadorSelecionado != null && PokemonSelecionado != null)
                    {
                        PokeWalletRepository PWrepository = new PokeWalletRepository();
                        PWrepository.Add(TreinadorSelecionado.Id, PokemonSelecionado.Id, DbConnection);
                        TreinadorSelecionado.Catch(PokemonSelecionado.Id);
                    }
                }

            }, (object _) => TreinadorSelecionado != null && PokemonSelecionado != null);


            AddTreinador = new RelayCommand((object _) => {

                Trainer newTrainer = new Trainer();
                TrainerRepository repository = trainerRepository;

                TrainerInfoInput telaDeCadastro = new TrainerInfoInput();
                telaDeCadastro.DataContext = newTrainer;
                bool? verifica = telaDeCadastro.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    TrainerList.Add(new Trainer(IdForTrainers, newTrainer.Name, newTrainer.Age));
                    repository.Add(new Trainer(IdForTrainers, newTrainer.Name, newTrainer.Age), DbConnection);
                    IdForTrainers++;
                }
            });

            RemoveTreinador = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null)
                {
                    TrainerRepository Trepository = trainerRepository;
                    PokeWalletRepository PWrepository = pokeWalletRepository;
                    if (TreinadorSelecionado.PokeWallet.Count() > 0)
                    {
                        DialogResult dr = MessageBox.Show("Do you want to delete this trainer and his team?","Delete Trainer", MessageBoxButtons.YesNo);
                        switch (dr)
                        {
                            case DialogResult.Yes:
                                PWrepository.ClearTrainerWallet(TreinadorSelecionado.Id, dbConnection);
                                Trepository.Remove(TreinadorSelecionado, DbConnection);
                                trainerList.Remove(TreinadorSelecionado);
                                break;
                            case DialogResult.No:
                                break;
                        }
                    }
                  
                    //Trepository.Remove(TreinadorSelecionado, DbConnection);
                    //trainerList.Remove(TreinadorSelecionado);
                }

            }, (object _) => TreinadorSelecionado != null);


            UpdateTreinador = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null)
                {
                    TrainerInfoInput telaDeUpdate = new TrainerInfoInput();
                    Trainer UpdateHolder = TreinadorSelecionado.ShallowCopy();
                    TrainerRepository repository = trainerRepository;

                    telaDeUpdate.DataContext = UpdateHolder;
                    bool? verifica = telaDeUpdate.ShowDialog();

                    if (verifica.HasValue && verifica.Value)
                    {
                        TreinadorSelecionado.UpdateName(UpdateHolder.Name);
                        TreinadorSelecionado.UpdateAge(UpdateHolder.Age);
                        repository.Update(TreinadorSelecionado, DbConnection);
                    }
                }

            }, (object _) => TreinadorSelecionado != null);


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
            }, (object _) => TreinadorSelecionado != null);

        }
    }
}
