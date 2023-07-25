using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeWallet
{
    public class PokeWalletDb
    {
        private int userId;
        private int pokemonId;
        public PokeWalletDb()
        {
            userId = new int();
            pokemonId = new int();
        }
        public int UserId { get { return userId; } set {  userId = value; } }
        public int PokemonId { get {  return pokemonId; } set {  pokemonId = value; } }
    }
}
