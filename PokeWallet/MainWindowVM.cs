using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeWallet
{
    public class MainWindowVM
    {
        public Game Pokemon { get; set; }
        public MainWindowVM()
        {
            Pokemon = new Game();
        }
      
    }
}
