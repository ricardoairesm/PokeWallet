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
        private IDbConnection dbConnection;
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
            dbConnection = new PostgresConnection();
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
        public IDbConnection DbConnection { get { return dbConnection; } private set { dbConnection = value; } }
        public ICommand AddPokemon { get { return addPokemon; } private set { addPokemon = value; } }
        public ICommand RemovePokemon { get { return removePokemon; } private set { removePokemon = value; } }
        public ICommand UpdatePokemon { get { return updatePokemon; } private set { updatePokemon = value; } }
        public ICommand CatchPokemon { get { return catchPokemon; } private set { catchPokemon = value; } }
        public ICommand AddTreinador { get { return addTreinador; } private set { addTreinador = value; } }
        public ICommand RemoveTreinador { get { return removeTreinador; } private set { removeTreinador = value; } }
        public ICommand UpdateTreinador { get { return updateTreinador; } private set { updateTreinador = value; } }
        public ICommand ShowTrainerWallet { get { return showTrainerWallet; } private set { showTrainerWallet = value; } }
        public ObservableCollection<Trainer> TrainerList { get { return trainerList; } private set { trainerList = value; } }


        public void ObterPokemons()
        {
            try
            {
                pokemonDbList = dbConnection.GetPokemon().OrderBy(pokemon => pokemon.Id);

                foreach (Pokemon pokemon in pokemonDbList)
                {
                    if (pokemon == pokemonDbList.Last())
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
                
                trainerDbList = dbConnection.GetTrainers().OrderBy(trainer => trainer.Id);

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
                    dbConnection.GetPokeWallet(trainer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public void IniciaComandos()
        {
            AddPokemon = new RelayCommand((object Botao) => {
                PokemonInfoInput telaDeCadastro = new PokemonInfoInput();

                telaDeCadastro.DataContext = Pokedex;
                bool? verifica = telaDeCadastro.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    pokeList.Add(new Pokemon(Pokedex.pokeNameSelecionado, IdForPokemons));
                    dbConnection.AddPokemon(new Pokemon(Pokedex.pokeNameSelecionado, IdForPokemons));
                    IdForPokemons++;
                }
            });

            RemovePokemon = new RelayCommand((object _) => {

                foreach(Trainer trainer in TrainerList)
                {
                    if (trainer.PokeWallet.Contains(PokemonSelecionado.Id))
                    {
                        MessageBox.Show("You can't remove pokemon that are on a team!", "Deleting pokemon in use");
                        return;
                    }
                }

                dbConnection.RemovePokemon(PokemonSelecionado);
                pokeList.Remove(PokemonSelecionado);

            }, (object _) => PokemonSelecionado != null);

            UpdatePokemon = new RelayCommand((object _) => {

                if (PokemonSelecionado != null)
                {
                    PokemonUpdateInfoInput telaDeUpdate = new PokemonUpdateInfoInput();
                    Pokemon UpdateHolder = PokemonSelecionado.ShallowCopy();

                    telaDeUpdate.DataContext = UpdateHolder;
                    bool? verifica = telaDeUpdate.ShowDialog();

                    if (verifica.HasValue && verifica.Value)
                    {
                        PokemonSelecionado.UpdateNickname(UpdateHolder.Nickname);
                        dbConnection.UpdatePokemon(PokemonSelecionado);
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
                        dbConnection.AddPokeWalletInstance(TreinadorSelecionado.Id, PokemonSelecionado.Id);
                        TreinadorSelecionado.Catch(PokemonSelecionado.Id);
                    }
                }

            }, (object _) => TreinadorSelecionado != null && PokemonSelecionado != null);


            AddTreinador = new RelayCommand((object _) => {

                Trainer newTrainer = new Trainer();

                TrainerInfoInput telaDeCadastro = new TrainerInfoInput();
                telaDeCadastro.DataContext = newTrainer;
                bool? verifica = telaDeCadastro.ShowDialog();

                if (verifica.HasValue && verifica.Value)
                {
                    TrainerList.Add(new Trainer(IdForTrainers, newTrainer.Name, newTrainer.Age));
                    dbConnection.AddTrainer(new Trainer(IdForTrainers, newTrainer.Name, newTrainer.Age));
                    IdForTrainers++;
                }
            });

            RemoveTreinador = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null)
                {
                    if (TreinadorSelecionado.PokeWallet.Count() > 0)
                    {
                        DialogResult dr = MessageBox.Show("Do you want to delete this trainer and his team?","Delete Trainer", MessageBoxButtons.YesNo);
                        switch (dr)
                        {
                            case DialogResult.Yes:
                                dbConnection.ClearTrainerPokeWallet(TreinadorSelecionado.Id);
                                dbConnection.RemoveTrainer(TreinadorSelecionado);
                                trainerList.Remove(TreinadorSelecionado);
                                break;
                            case DialogResult.No:
                                break;
                        }
                    }
                }

            }, (object _) => TreinadorSelecionado != null);


            UpdateTreinador = new RelayCommand((object _) => {

                if (TreinadorSelecionado != null)
                {
                    TrainerInfoInput telaDeUpdate = new TrainerInfoInput();
                    Trainer UpdateHolder = TreinadorSelecionado.ShallowCopy();

                    telaDeUpdate.DataContext = UpdateHolder;
                    bool? verifica = telaDeUpdate.ShowDialog();

                    if (verifica.HasValue && verifica.Value)
                    {
                        TreinadorSelecionado.UpdateName(UpdateHolder.Name);
                        TreinadorSelecionado.UpdateAge(UpdateHolder.Age);
                        dbConnection.UpdateTrainer(TreinadorSelecionado);
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
