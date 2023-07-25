using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeWallet
{
    public class Pokedex
    {
        public List<string> pokeNames { get; set; }
        public enum Poke { 
        };
        public string pokeNameSelecionado {get;set;}
        public Pokedex()
        {
            using (var client = new HttpClient())
            {
                pokeNames = new List<string>();
                var endpoint = new Uri("https://pokeapi.co/api/v2/pokemon?limit=1009&offset=0");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                var data = (JObject)JsonConvert.DeserializeObject(json);

                for(int i = 0; i < 1009; i++)
                {
                    string pokeName = data.SelectToken($"results[{i}].name").Value<string>();
                    pokeNames.Add(pokeName);
                }
            }
        }
    }
}
