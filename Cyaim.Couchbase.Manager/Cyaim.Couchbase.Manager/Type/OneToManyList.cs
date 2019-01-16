using System;
using System.Collections.Generic;
using System.Text;

namespace Cyaim.Couchbase.Manager.Type
{
    public class OneToManyList<Key, Value>
    {

        Dictionary<Key, List<Value>> pairs = new Dictionary<Key, List<Value>>();


        public List<Value> this[Key key]
        {
            get { return pairs[key]; }

            set
            {
                if (pairs.ContainsKey(key))
                {
                    pairs[key] = value;
                }
                else
                {
                    pairs.Add(key, value);
                }
            }
        }

        /// <summary>
        /// Add value to list by key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Key key, Value value)
        {
            if (pairs.ContainsKey(key))
            {
                pairs[key].Add(value);
            }
            else
            {
                pairs.Add(key, new List<Value>() { value });
            }
        }

        /// <summary>
        /// Remove value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(Key key)
        {
            if (pairs.ContainsKey(key))
            {
               return pairs.Remove(key);
            }
            else
            {
                return false;
            }
        }
    }
}
