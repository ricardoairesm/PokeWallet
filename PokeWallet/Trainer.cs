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
        private ObservableCollection<int> PokeWallet { get; set; }
        public Trainer()
        {
            PokeWallet = new ObservableCollection<int>();
        }
        public void Catch(int pokeId)
        {
            PokeWallet.Add(pokeId);
        }
    }
}
