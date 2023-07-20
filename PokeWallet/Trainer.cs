using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeWallet
{
    public class Trainer
    {
        private ObservableCollection<int> pokeWallet;
        private string name;
        private int id;
        public string DisplayPokeWallet { get { return pokeWallet.ToString(); } }

        public Trainer()
        {

        }
        public Trainer(int id, string name)
        {
            pokeWallet = new ObservableCollection<int>();
            this.id = id;
            this.name = name;
            PokeWallet = pokeWallet;
        }
        public void Catch(int pokeId)
        {
            pokeWallet.Add(pokeId);
        }

       // private string Metodo()
        //{
            
        //}

        public ObservableCollection<int> PokeWallet { get { return pokeWallet; } set { pokeWallet = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
    }
}
