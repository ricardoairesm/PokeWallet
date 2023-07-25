using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokeWallet
{
    public class Trainer : INotifyPropertyChanged
    {
        private ObservableCollection<int> pokeWallet;
        private string name;
        private int age;
        private int id;
        public Trainer()
        {

        }
        public Trainer(int id, string name, int age)
        {
            pokeWallet = new ObservableCollection<int>();
            this.id = id;
            this.name = name;
            this.age = age;
        }

        public Trainer(Trainer trainer)
        {
            pokeWallet = new ObservableCollection<int>();
            this.id = trainer.id;
            this.name = trainer.name;
            this.age = trainer.age;
        }


        public Trainer ShallowCopy()
        {
            return (Trainer)this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notifica(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Catch(int pokeId)
        {
            pokeWallet.Add(pokeId);
            Notifica(nameof(PokeWalletString));

        }

        public void Release(int pokeId)
        {
            pokeWallet.Remove(pokeId);
            Notifica(nameof(PokeWalletString));

        }

        public void UpdateName(string name)
        {
            this.name = name;
            Notifica(nameof(Name));

        }

        public void UpdateAge(int age)
        {
            this.age = age;
            Notifica(nameof(Age));

        }

        public string PokeWalletString 
            {
            get
            {
                string s = "";
                if(PokeWallet.ToArray().Length != 0)
                {
                    s += PokeWallet[0];

                    for (int i = 1; i < PokeWallet.ToArray().Length; i++)
                    {
                        s += ", " + PokeWallet[i];
                    }
                }
                return s;
            }
            set { }
            } 
         
        public ObservableCollection<int> PokeWallet { get { return pokeWallet; } set { pokeWallet = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
        public int Age { get { return age; } set { age = value; } }
    }
}
