using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minerva.Module
{
    /// <summary>
    /// A key-value pair of string/bool
    /// can construct a full list and check on the list  
    /// </summary>
    [Serializable]
    public class CheckList : DataList<Check>
    {
        public ICollection<string> Keys => GetKeys();

        public ICollection<bool> Values => GetValues();


        public CheckList() : base()
        {

        }

        public new bool this[string index]
        {
            get => base[index].Value;
            set => SetParam(index, value);
        }

        public void SetParam(string key, bool value)
        {
            Check arg = base[key];
            arg.Value = value;
            int v = IndexOf(base[key]);
            RemoveAt(v);
            Insert(v, arg);
        }

        public void Add(string key, bool value)
        {
            base.Add(new Check(key, value));
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

        private ICollection<bool> GetValues()
        {
            List<bool> a = new List<bool>();
            foreach (var item in list)
            {
                a.Add(item.Value);
            }
            return a;
        }

        public CheckList(IEnumerable<INameable> fullList, IEnumerable<INameable> check)
        {
            foreach (INameable item in fullList)
            {
                Add(item.Name, false);
            }
            foreach (INameable item in check)
            {
                this[item.Name] = true;
            }
        }

        public CheckList(IEnumerable<INameable> fullList, IEnumerable<string> check)
        {
            foreach (INameable item in fullList)
            {
                Add(item.Name, false);
            }
            foreach (string item in check)
            {
                this[item] = true;
            }
        }
    }

    [Serializable]
    public struct Check : INameable
    {
        [SerializeField] private string key;
        [SerializeField] private bool value;

        public string Key { get => key; set => key = value; }
        public bool Value { get => value; set => this.value = value; }
        public string Name => Key;

        public Check(string key, bool value)
        {
            this.key = key;
            this.value = value;
        }

        public static implicit operator KeyValuePair<string, bool>(Check arg)
        {
            return new KeyValuePair<string, bool>(arg.key, arg.value);
        }

        public static implicit operator Check(KeyValuePair<string, bool> arg)
        {
            return new Check(arg.Key, arg.Value);
        }
        public static implicit operator Argument(Check arg)
        {
            return new KeyValuePair<string, string>(arg.key, arg.value.ToString());
        }

        public static implicit operator Check(Argument arg)
        {
            return new Check(arg.Key, bool.Parse(arg.Value));
        }

        public static implicit operator Check(string arg)
        {
            if (arg is null)
            {
                throw new ArgumentException();
            }
            if (arg.Split(',').Length > 2 || arg.Split(',').Length < 1)
            {
                throw new ArgumentException();
            }

            string[] vs = arg.Split(',');

            Check arg1 = new Check(vs[0], false);
            if (vs.Length > 1)
            {
                arg1.value = bool.Parse(vs[1]);
            }
            return arg1;
        }

        public bool Equals(Check other)
        {
            return other.value == value && other.key == key;
        }

        public override bool Equals(object obj)
        {
            return obj is Argument ? Equals((Check)obj) : false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Check left, Check right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Check left, Check right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return '{' + key + ": " + value + '}';
        }
    }
}
