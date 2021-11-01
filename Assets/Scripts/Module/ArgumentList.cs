using System;
using System.Collections.Generic;

namespace Minerva.Module
{
    [Serializable]
    public class ArgumentList : DataList<Argument>, ICloneable
    {
        public ICollection<string> Keys => GetKeys();
        public ICollection<string> Values => GetValues();





        /// <summary>
        /// Check effect has a arg with <paramref name="key"/>
        /// </summary>
        /// <param name="key">key of the arg</param>
        /// <returns></returns>
        public bool HasParam(string key)
        {
            return Keys.Contains(key);
        }

        /// <summary>
        /// Check effect has a arg {<paramref name="key"/>: <paramref name="value"/>}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HasParam(string key, string value)
        {
            return this[key] == value;
        }




        public static ArgumentList Parse(string value)
        {
            var ps = value.Split(',');
            var arg = new ArgumentList();
            foreach (var item in ps)
            {
                arg.Add(item);
            }
            return arg;
        }

        public static bool TryParse(string value, out ArgumentList arguments)
        {
            try
            {
                arguments = Parse(value);
                return true;
            }
            catch
            {
                arguments = null;
            }
            return false;
        }





        public ArgumentList() : base()
        {

        }

        public new string this[string index]
        {
            get => TryGet(index);
            set => SetParam(index, value);
        }

        public ArgumentList(IEnumerable<Argument> args)
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
        public void SetParam(string key, string value)
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

        public string ToString(int prespaceCount)
        {
            string prespace = "";
            for (int i = 0; i < prespaceCount; i++)
            {
                prespace += " ";
            }
            string v = prespace + ToString().Replace("\n", "\n" + prespace);
            if (v.Length < 2) return "";
            return v.Remove(v.Length - 2);
        }

        public static implicit operator Arguments(ArgumentList args)
        {
            return new Arguments(args);
        }

        public ArgumentList Clone() => new ArgumentList(this);
        object ICloneable.Clone() => Clone();
    }
}
