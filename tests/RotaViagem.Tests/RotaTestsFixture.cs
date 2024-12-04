using Bogus;
using RotaViagem.Domain.Entities;

namespace RotaViagem.Tests
{
    [CollectionDefinition(nameof(RotaTestCollection))]
    public class RotaTestCollection : ICollectionFixture<RotaTestsFixture>
    { }


    public class RotaTestsFixture : IDisposable
    {

        public Rota GeraRotaValida()
        {
            var rota = new Rota("GRU", "BRC", 10);
            return rota;
        }
        public Rota GeraRotaInvalida()
        {
            var rota = new Rota("", "", 0);
            return rota;
        }

        public IEnumerable<Rota> ObterVariasRotas()
        {
            var rotas = new List<Rota>();
            rotas.AddRange(GerarVariasRotas(6));

            return rotas;
        }


        public IEnumerable<Rota> GerarVariasRotas(int quantidade)
        {

            var rota = new Faker<Rota>("pt_BR")
                .CustomInstantiator(f => new Rota(
                    f.Address.CityPrefix(),
                    f.Address.CityPrefix(),
                    Convert.ToDouble(f.Finance.Amount())));
            return rota.Generate(quantidade);
        }


        public void Dispose()
        {            
        }
    }
}
