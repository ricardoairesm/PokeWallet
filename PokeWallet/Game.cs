using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeWallet
{
    public class Game
    {
        private ObservableCollection<Pokemon> pokeList;
        private Trainer ash;
        private ICommand add;
        private ICommand remove;
        private ICommand update;
        private ICommand catchPokemon;
        private Pokemon pokemonSelecionado;
        public Game()
        {
            pokeList = new ObservableCollection<Pokemon>()
            {
                new Pokemon("lucario",448,"steel"),
                new Pokemon("bulbassauro",1,"grass"),
                new Pokemon("Charizard",1,"grass"),
                new Pokemon("bulbassauro",1,"grass"),
                new Pokemon("bulbassauro",1,"grass"),
                new Pokemon("bulbassauro",1,"grass"),
                new Pokemon("bulbassauro",1,"grass"),
                new Pokemon("bulbassauro",1,"grass"),
            };
            Ash = new Trainer();
            IniciaComandos();
        }

        public ObservableCollection<Pokemon> PokeList { get { return pokeList; }  set { pokeList = value; } }
        public Trainer Ash { get {  return ash; }  set { ash = value; } }
        public ICommand Add {  get { return add; }  set {  add = value; } }
        public ICommand Remove { get { return remove; }  set { remove = value; } }
        public ICommand Update { get { return update; }  set {  update = value; } }
        public ICommand CatchPokemon {  get { return catchPokemon; }  set {  catchPokemon = value; } }
        public Pokemon PokemonSelecionado { get { return pokemonSelecionado; }  set { pokemonSelecionado = value; } }


        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) => {
                Pokemon newPokemon = new Pokemon();
                AddPokemonWindow telaDeCadastro = new AddPokemonWindow();
                telaDeCadastro.DataContext = newPokemon;
                telaDeCadastro.ShowDialog();
                pokeList.Add(new Pokemon(newPokemon.Name, newPokemon.Id, newPokemon.Type));
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
            CatchPokemon = new RelayCommand((object _) => {
                Ash.Catch(PokemonSelecionado.Id);
            });

        }
    }
}
