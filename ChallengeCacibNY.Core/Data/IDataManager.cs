using ChallengeCacibNY.Core.Models;

namespace ChallengeCacibNY.Core.Data
{
    public interface IDataManager<I, V>
        where V : IValue<I>
        where I : IComparable<I>
    {
        Task<V> Get(I id);
        Task UpdateOrInsert(I id, V value);
        Task Delete(I id);
    }
}
