using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hydrogen.General.Collections
{
    /// <summary>
    /// Linked-list backend IDictionary implementation. 
    /// Code copied and changed from REVISION 679 of following SVN repository:
    /// https://slog.dk/svn/home/jensen/source/linked_dictionary/trunk/
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class AbstractLinkedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Fields

        protected readonly IDictionary<TKey, LinkedKeyValuePair> Backend;
        protected volatile uint Updates;
        protected LinkedKeyValuePair First;
        protected LinkedKeyValuePair Last;

        #endregion

        #region Initialization

        protected AbstractLinkedDictionary(IDictionary<TKey, LinkedKeyValuePair> backend)
        {
            Backend = backend;
        }

        #endregion

        #region Abstracts

        protected abstract bool RemoveItem(TKey key);
        protected abstract void AddItem(TKey key, TValue value);
        protected abstract void SetItem(TKey key, TValue value);
        protected abstract LinkedKeyValuePair GetItem(TKey key);

        #endregion

        #region Explicit directional iteration

        public IEnumerator<KeyValuePair<TKey, TValue>>  GetEnumeratorForward()
        {
            return new LinkedDictionaryEnumerator(this, true);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumeratorBackward()
        {
            return new LinkedDictionaryEnumerator(this, false);
        }

        public ICollection<TValue> ValuesForward => new DictionaryValues(this, true);

        public ICollection<TValue> ValuesBackward => new DictionaryValues(this, false);

        public ICollection<TKey> KeysForward => new DictionaryKeys(this, true);

        public ICollection<TKey> KeysBackward => new DictionaryKeys(this, false);

        #endregion

        #region Implementation of IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IEnumerable<out KeyValuePair<TKey,TValue>>

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetEnumeratorForward();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<TKey,TValue>>

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            Updates++;
            Backend.Clear();
            First = null;
            Last = null;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Backend.ContainsKey(item.Key) && Backend[item.Key].Value.Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            foreach (KeyValuePair<TKey, TValue> e in this)
                array.SetValue(e, index++);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
                return Remove(item.Key);

            return false;
        }

        public int Count => Backend.Count;

        public bool IsReadOnly => Backend.IsReadOnly;

        #endregion

        #region Implementation of IDictionary<TKey,TValue>

        public bool ContainsKey(TKey key)
        {
            return Backend.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            Updates++;
            AddItem(key, value);
        }

        public bool Remove(TKey key)
        {
            Updates++;
            return RemoveItem(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var item = GetItem(key);
            if (item != null)
            {
                value = item.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                return GetItem(key).Value;
            }
            set
            {
                Updates++;
                SetItem(key, value);
            }
        }

        public virtual ICollection<TKey> Keys => KeysForward;

        public virtual ICollection<TValue> Values => ValuesForward;

        #endregion

        #region Inner types

        public class LinkedKeyValuePair
        {
            public LinkedKeyValuePair Previous { get; set; }
            public KeyValuePair<TKey, TValue> KeyValuePair { get; }
            public LinkedKeyValuePair Next { get; set; }

            public TKey Key => KeyValuePair.Key;

            public TValue Value => KeyValuePair.Value;

            public LinkedKeyValuePair(TKey key, TValue value, LinkedKeyValuePair prev, LinkedKeyValuePair next)
            {
                KeyValuePair = new KeyValuePair<TKey, TValue>(key, value);
                Previous = prev;
                Next = next;
            }
        }

        private class LinkedDictionaryEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private readonly AbstractLinkedDictionary<TKey, TValue> _parent;
            private readonly bool _forward;
            private readonly uint _updates;
            private LinkedKeyValuePair _current;

            public LinkedDictionaryEnumerator(AbstractLinkedDictionary<TKey, TValue> parent, bool forward)
            {
                _parent = parent;
                _forward = forward;
                _current = null;
                _updates = parent.Updates;
            }

            #region IEnumerator Members

            public void Reset()
            {
                _current = null;
            }

            public KeyValuePair<TKey, TValue> Current => new KeyValuePair<TKey, TValue>(_current.Key, _current.Value);

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_parent.Updates != _updates)
                    throw new InvalidOperationException("Collection was modified after the enumerator was created");
                if (_current == null)
                    _current = _forward ? _parent.First : _parent.Last;
                else
                    _current = _forward ? _current.Next : _current.Previous;
                return _current != null;
            }

            public void Dispose()
            {
            }

            #endregion

            #region Key and Value direct access members

            public TKey Key
            {
                get
                {
                    if (_current == null)
                        throw new IndexOutOfRangeException();
                    
                    return _current.Key;
                }
            }
            public TValue Value
            {
                get
                {
                    if (_current == null)
                        throw new IndexOutOfRangeException();
                    
                    return _current.Value;
                }
            }

            #endregion
        }

        private struct DictionaryKeys : ICollection<TKey>
        {
            private readonly AbstractLinkedDictionary<TKey, TValue> _parent;
            private readonly bool _forward;

            public DictionaryKeys(AbstractLinkedDictionary<TKey, TValue> parent, bool forward)
            {
                _parent = parent;
                _forward = forward;
            }

            #region Implementation of IEnumerable

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion

            #region Implementation of IEnumerable<out TKey>

            public IEnumerator<TKey> GetEnumerator()
            {
                return new KeysEnumerator(new LinkedDictionaryEnumerator(_parent, _forward));
            }

            #endregion

            #region Implementation of ICollection<TKey>

            public void Add(TKey item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(TKey item)
            {
                return _parent.Backend.ContainsKey(item);
            }

            public void CopyTo(TKey[] array, int index)
            {
                foreach (object o in this)
                    array.SetValue(o, index++);
            }

            public bool Remove(TKey item)
            {
                throw new NotSupportedException();
            }

            public int Count => _parent.Backend.Count;

            public bool IsReadOnly => true;

            #endregion

            #region Inner types

            private struct KeysEnumerator : IEnumerator<TKey>
            {
                private readonly LinkedDictionaryEnumerator _enumerator;

                public KeysEnumerator(LinkedDictionaryEnumerator enumerator)
                {
                    _enumerator = enumerator;
                }

                #region IEnumerator Members

                public void Reset()
                {
                    _enumerator.Reset();
                }

                public TKey Current => _enumerator.Key;

                public bool MoveNext()
                {
                    return _enumerator.MoveNext();
                }

                public void Dispose()
                {
                }

                object IEnumerator.Current => Current;

                #endregion
            }

            #endregion
        }

        public struct DictionaryValues : ICollection<TValue>
        {
            private readonly AbstractLinkedDictionary<TKey, TValue> _parent;
            private readonly bool _forward;

            public DictionaryValues(AbstractLinkedDictionary<TKey, TValue> parent, bool forward)
            {
                _parent = parent;
                _forward = forward;
            }

            #region Implementation of IEnumerable

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion

            #region Implementation of IEnumerable<out TValue>

            public IEnumerator<TValue> GetEnumerator()
            {
                return new ValuesEnumerator(new LinkedDictionaryEnumerator(_parent, _forward));
            }

            #endregion

            #region Implementation of ICollection<TValue>

            public void Add(TValue item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(TValue item)
            {
                return _parent.Backend.Values.Any(l => l.Value.Equals(item));
            }

            public void CopyTo(TValue[] array, int index)
            {
                foreach (object o in this)
                    array.SetValue(o, index++);
            }

            public bool Remove(TValue item)
            {
                throw new NotSupportedException();
            }

            public int Count => _parent.Count;

            public bool IsReadOnly => true;

            #endregion

            #region Inner types

            struct ValuesEnumerator : IEnumerator<TValue>
            {
                private readonly LinkedDictionaryEnumerator _enumerator;

                public ValuesEnumerator(LinkedDictionaryEnumerator enumerator)
                {
                    _enumerator = enumerator;
                }

                #region IEnumerator Members

                public void Reset()
                {
                    _enumerator.Reset();
                }

                object IEnumerator.Current => Current;

                public bool MoveNext()
                {
                    return _enumerator.MoveNext();
                }

                public TValue Current => _enumerator.Value;

                public void Dispose()
                {
                }

                #endregion
            }

            #endregion
        }

        #endregion

        #region Debug Helpers

#if DEBUG
        // ReSharper disable UnusedMember.Local
        static object[] Array(ICollection c) { var a = new object[c.Count]; c.CopyTo(a, 0); return a; }
        // ReSharper restore UnusedMember.Local
#endif  
      
        #endregion
    }

    /// <summary>
    /// Provides an IDictionary which is iterated in the inverse order of update
    /// </summary>
    public class UpdateLinkedDictionary<TKey, TValue> : AbstractLinkedDictionary<TKey, TValue>
    {
        public UpdateLinkedDictionary()
            : this(new Dictionary<TKey, LinkedKeyValuePair>())
        {
        }

        public UpdateLinkedDictionary(IDictionary<TKey, LinkedKeyValuePair> backend) 
            : base(backend)
        {
        }

        protected override void AddItem(TKey key, TValue value)
        {
            if (Backend.ContainsKey(key))
                throw new ArgumentException($"Key \"{key}\" already present in dictionary");

            var l = new LinkedKeyValuePair(key, value, Last, null);
            if (Last != null)
                Last.Next = l;

            Last = l;
            if (First == null)
                First = l;

            Backend.Add(key, l);
        }

        protected override LinkedKeyValuePair GetItem(TKey key)
        {
            LinkedKeyValuePair result;
            return Backend.TryGetValue(key, out result) ? result : null;
        }

        protected override bool RemoveItem(TKey key)
        {
            LinkedKeyValuePair l;
            if (Backend.TryGetValue(key, out l))
            {
                if (l != null)
                {
                    LinkedKeyValuePair pre = l.Previous;
                    LinkedKeyValuePair nxt = l.Next;

                    if (pre != null)
                        pre.Next = nxt;
                    else
                        First = nxt;
                    if (nxt != null)
                        nxt.Previous = pre;
                    else
                        Last = pre;

                }

                return Backend.Remove(key);
            }

            return false;
        }

        protected override void SetItem(TKey key, TValue value)
        {
            LinkedKeyValuePair l = GetItem(key);
            if (l != null)
                RemoveItem(key);

            AddItem(key, value);
        }
    }

    public class LruDictionary<TKey, TValue> : UpdateLinkedDictionary<TKey, TValue>
    {
        public LruDictionary() 
            : this(new Dictionary<TKey, LinkedKeyValuePair>())
        {    
        }

        public LruDictionary(IDictionary<TKey, LinkedKeyValuePair> backend) 
            : base(backend)
        {
        }

        protected override LinkedKeyValuePair GetItem(TKey key)
        {
            LinkedKeyValuePair l;
            if (!Backend.TryGetValue(key, out l))
                return null;

            if (l == null)
                return null;

            LinkedKeyValuePair nxt = l.Next;
            if (nxt != null) // last => no-change
            {
                Updates++; // looking is updating
                // note, atleast 2 items in chain now, since l != last
                LinkedKeyValuePair pre = l.Previous;
                if (pre == null)
                    First = nxt;
                else
                    pre.Next = l.Next;

                nxt.Previous = pre; // nxt != null since l != last
                Last.Next = l;
                l.Next = null;
                l.Previous = Last;
                Last = l;
            }

            return l;
        }
    }

    public class MruDictionary<TKey, TValue> : LruDictionary<TKey, TValue>
    {
        public MruDictionary()
            : this(new Dictionary<TKey, LinkedKeyValuePair>())
        {
        }

        public MruDictionary(IDictionary<TKey, LinkedKeyValuePair> backend) 
            : base(backend)
        {
        }

        public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetEnumeratorForward();
        }

        public override ICollection<TKey> Keys => KeysBackward;

        public override ICollection<TValue> Values => ValuesBackward;
    }
}