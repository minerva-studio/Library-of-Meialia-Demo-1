using System;
using System.Collections.Generic;

namespace Minerva.Module
{
    [Serializable]
    public class Dictionary : DataList<Argument>//, IDictionary<string, string>
    {
        public ICollection<string> Keys => GetKeys();
        public ICollection<string> Values => GetValues();

        public Dictionary() : base()
        {

        }

        public new string this[string index]
        {
            get => TryGet(index);
            set => Set(index, value);
        }

        public Dictionary(IEnumerable<Argument> args)
        {
            foreach (var item in args)
            {
                Add(item);
            }
        }

        public string TryGet(string key)
        {
            return ContainsKey(key) ? base[key].Value : null;
        }

        public bool ContainsKey(string key)
        {
            foreach (var item in this)
            {
                if (item.Key == key)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValidKey(string key)
        {
            return string.IsNullOrEmpty(this[key]);
        }

        /// <summary>
        /// set one parameter with given value
        /// <para>key must present in the list, otherwise it will throw Error</para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, string value)
        {
            Argument arg = base[key];
            arg.Key = key;
            arg.Value = value;
            int v = IndexOf(base[key]);
            if (v > -1)
            {
                RemoveAt(v);
                Insert(v, arg);
            }
            else Add(arg);
        }

        public void Add(string key, string value) => Add(new Argument(key, value));

        public bool Remove(string key)
        {
            try
            {
                Argument arg = base[key];
                int v = IndexOf(base[key]);
                RemoveAt(v);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private ICollection<string> GetKeys()
        {
            List<string> a = new List<string>();
            foreach (var item in list)
            {
                a.Add(item.Key);
            }
            return a;
        }

        private ICollection<string> GetValues()
        {
            List<string> a = new List<string>();
            foreach (var item in list)
            {
                a.Add(item.Value);
            }
            return a;
        }

        //public bool TryGetValue(string key, out string value)
        //{
        //    try
        //    {
        //        Arg arg = base[key];
        //        value = arg.Value;
        //        return true;
        //    }
        //    catch
        //    {
        //        value = null;
        //        return false;
        //    }
        //}

        public override string ToString()
        {
            string ans = "";
            if (Count == 0)
            {
                return ans;
            }
            foreach (var item in this)
            {
                ans += "\n  -";
                ans += item.Key;
                ans += ": ";
                ans += item.Value;
                ans += ";";
            }
            return ans;
        }

        public ArgumentList Clone() => new ArgumentList(this);
    }

}
