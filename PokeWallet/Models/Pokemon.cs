using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PokeWallet
{
    public class Pokemon : INotifyPropertyChanged
    {
        private string name;
        private int pokeId;
        private int id;
        private string type;
        private string sprite;
        private string nickname;
        public Pokemon()
        { 

        }
        public Pokemon ShallowCopy()
        {
            return (Pokemon)this.MemberwiseClone();
        }


        public Pokemon(string pokeName, int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri($"https://pokeapi.co/api/v2/pokemon/{pokeName}");
                    var result = client.GetAsync(endpoint).Result;
                    var json = result.Content.ReadAsStringAsync().Result;
                    var data = (JObject)JsonConvert.DeserializeObject(json);

                    var types = data.SelectToken("types").Value<JArray>();

                    this.name = pokeName;

                    if (types.LongCount() > 1)
                    {
                        this.type = data.SelectToken("types[0].type.name").Value<string>() + " and " + data.SelectToken("types[1].type.name").Value<string>();
                    }

                    else
                    {
                        this.type = data.SelectToken("types[0].type.name").Value<string>();
                    }
                    this.pokeId = data.SelectToken("id").Value<int>();
                    int spriteId = this.pokeId;
                    this.sprite = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/{spriteId}.png";

                    this.Id = id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public Pokemon(string name, int id,string type)
        {
            this.name = name;
            this.pokeId = id;
            this.type = type;
            this.sprite = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/{id}.png";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notifica(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int PokeId
        {
            get { return pokeId; }
            set { pokeId = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
            
        }

        public string Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public void UpdateNickname(string nickname)
        {
            this.nickname = nickname;
            Notifica(nameof(Nickname));

        }

    }
}
