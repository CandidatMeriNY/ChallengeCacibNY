using ChallengeCacibNY.Core.Models;
using FASTER.core;
using System.Reflection;

namespace ChallengeCacibNY.Core.Data
{
    public class DataManager<I, V> : IDataManager<I, V>
        where V : IValue<I>
        where I : IComparable<I>
    {
        public DataManager()
        {
            storeSettings = storeSettings ?? new FasterKVSettings<I, V>($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{typeof(V).Name}");
            store = store ?? new FasterKV<I, V>(storeSettings);
            _session = store.For(new SimpleFunctions<I, V>()).NewSession<SimpleFunctions<I, V>>();
        }

        private ClientSession<I, V, V, V, Empty, SimpleFunctions<I, V>> _session { get; }

        private static FasterKVSettings<I, V> storeSettings { get; set; }
        private static FasterKV<I, V> store { get; set; }

        public static I MaxId { get; private set; }

        public async Task<V> Get(I id)
        {
            var v = await _session.ReadAsync(id);
            return v.Output;
        }

        public async Task UpdateOrInsert(I id, V value)
        {
            if (id.CompareTo(MaxId) >= 0)
            {
                MaxId = id;
            }
            await _session.UpsertAsync(id, value);
        }

        public async Task Delete(I id)
        {
            await _session.DeleteAsync(id);
        }
    }
}
