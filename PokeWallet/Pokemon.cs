﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PokeWallet
{
    public class Pokemon
    {
        private string name;
        private int id;
        private string type;
        private string sprite;
        public Pokemon()
        { 

        }

        public Pokemon(int id)
        {
            using(var client = new HttpClient())
            {
                var endpoint = new Uri($"https://pokeapi.co/api/v2/pokemon/{id}");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                var data = (JObject)JsonConvert.DeserializeObject(json);

                this.name = data.SelectToken("name").Value<string>();
                this.type = data.SelectToken("types[0].type.name").Value<string>();
                this.sprite = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/{id}.png";
                this.id = id;
            }
        }

        public Pokemon(string name, int id,string type)
        {
            this.name = name;
            this.id = id;
            this.type = type;
            this.sprite = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/{id}.png";
        }

        public void UpdateSprite()
        {
            this.sprite = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/{this.Id}.png";
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
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
        
    }
}
