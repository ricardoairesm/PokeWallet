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
        private ObservableCollection<int> pokeWallet { get; set; }
        public Trainer()
        {
            pokeWallet = new ObservableCollection<int>();
        }
        public void Catch(int pokeId)
        {
            pokeWallet.Add(pokeId);
        }

        public ObservableCollection<int> PokeWallet { get { return pokeWallet; } set { pokeWallet = value; } }
    }
}
