using Autofac.Extras.Moq;
using NUnit.Framework.Constraints;
using PokeWallet.Infrastructure;

namespace PokeWallet.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LoadPokemon_ValidCall()
        {
            using(var mock = AutoMock.GetLoose())
            {
                mock.Mock<PostgresConnection>().Setup(x => x.GetPokemon()).Returns(GetSamplePokemon);
                var cls = mock.Create<MainWindowVM>();
                var expected = GetSamplePokemon();

                var actual = cls.PokeList;


                Assert.True(actual != null);
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        private List<Pokemon> GetSamplePokemon()
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