﻿using System;
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
        private int id;
        public Trainer()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notifica(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Catch(int pokeId)
        {
            pokeWallet.Add(pokeId);
            Notifica("Metodo");

        }

        public Trainer(int id, string name)
        {
            pokeWallet = new ObservableCollection<int>();
            this.id = id;
            this.name = name;
            PokeWallet = pokeWallet;

        }
 
        public string Metodo 
            {
            get
            {
                string s = "" + PokeWallet[0];

                for(int i = 1; i < PokeWallet.ToArray().Length; i++)
                {
                    s += ", " + id;
                }

                return s;
            }
            set { }
            } 
         
        public ObservableCollection<int> PokeWallet { get { return pokeWallet; } set { pokeWallet = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
    }
}
