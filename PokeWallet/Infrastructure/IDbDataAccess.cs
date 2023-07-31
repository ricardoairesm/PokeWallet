using System.Collections.Generic;

namespace PokeWallet.Infrastructure
{
    public interface IDbDataAccess
    {
        string connectionString();
        List<T> LoadData<T>(string sql);
        void SaveData<T>(T data, string sql);
        void UpdateData<T>(T data, string sql);
        void RemoveData<T>(T data,string sql);
    }
}