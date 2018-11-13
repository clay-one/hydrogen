using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Hydrogen.General.Collections;

namespace Hydrogen.General.Text
{
    public class RegexCache
    {
        private readonly LruCache<string, Regex> _cache;

        public RegexCache(int size)
        {
            _cache = new LruCache<string, Regex>(size);
        }

        public Regex GetPatternForRegex(string regex)
        {
            Regex pattern = _cache.Get(regex);
            if (pattern == null)
            {
                pattern = new Regex(regex);
                _cache.Put(regex, pattern);
            }
            return pattern;
        }

        // This method is used for testing.
        public bool ContainsRegex(string regex)
        {
            return _cache.ContainsKey(regex);
        }

        private class LruCache<TK, TV>
        {
            // LinkedHashMap offers a straightforward implementation of LRU cache.
            private readonly LruDictionary<TK, TV> _map;
            private readonly int _size;

            public LruCache(int size)
            {
                _size = size;
                _map = new LruDictionary<TK, TV>();
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public TV Get(TK key)
            {
                TV result;
                if (!_map.TryGetValue(key, out result))
                    return default(TV);

                return result;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public void Put(TK key, TV value)
            {
                _map[key] = value;
                if (_map.Count > _size)
                {
                    var first = _map.Keys.First();
                    _map.Remove(first);
                }
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool ContainsKey(TK key)
            {
                return _map.ContainsKey(key);
            }
        }
    }
}