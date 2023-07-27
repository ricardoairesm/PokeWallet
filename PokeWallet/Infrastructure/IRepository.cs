using System.Collections.Generic;

namespace PokeWallet.Infrastructure
{
    public interface IRepository
    {
        bool Add();
        List<Pokemon> Get();
        bool Remove();
        bool Update();
    }
}