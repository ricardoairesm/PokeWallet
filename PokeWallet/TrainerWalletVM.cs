using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeWallet
{
    public class TrainerWalletVM
    {
        public ObservableCollection<Pokemon> DisplayWallet { get; set; }
        public TrainerWalletVM(Trainer TreinadorSelecionado, ObservableCollection<Pokemon> PokeList)
        {
            DisplayWallet = new ObservableCollection<Pokemon>();
            foreach(int id in TreinadorSelecionado.PokeWallet)
            {
                DisplayWallet.Add((Pokemon)PokeList.Where(p => p.Id == id));
            }
        }
    }
}
