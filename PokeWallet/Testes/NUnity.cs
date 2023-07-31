using Moq;
using NUnit.Framework;
using PokeWallet.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using Assert = NUnit.Framework.Assert;

namespace PokeWallet.Test
{
    public class XUnity
    {
        [Test]
        public void LoadPokemon_ValidCall()
        {
            var dbAccessMock = new Mock<IDbDataAccess>();
            dbAccessMock.Setup(x => x.LoadData<Pokemon>("SELECT * FROM pokemon")).Returns(GetSamplePokemon());
            var cls = new PostgresConnection(dbAccessMock.Object);
            var expected = GetSamplePokemon();

            var actual = cls.GetPokemon();

            Assert.AreEqual(expected, actual);
        }

        public List<Pokemon> GetSamplePokemon()
        {
            List<Pokemon> output = new List<Pokemon>
            {
                new Pokemon("pikachu",1),
                new Pokemon("charizard",2),
                new Pokemon("zapdos",3),
                new Pokemon("blastoise",4),
            };

            return output;
        }
    }
}
