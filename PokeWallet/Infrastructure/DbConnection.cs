using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokeWallet.Infrastructure
{
    public class DbConnection
    {
        public NpgsqlConnection Connection { get; set; }
        public DbConnection()
        {
            try
            {
                Connection = new NpgsqlConnection("Server=localhost;Port=5555;Database=pokemondb;User Id=postgres;Password=123456789;");
                Connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
