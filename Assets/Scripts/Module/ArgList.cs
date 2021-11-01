using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Minerva.Module
{
    [Serializable]
    public struct Arguments : IEnumerable<Argument>
    {
        [SerializeField] private Argument[] args;

        public string this[string index]
        {
            get { return Get(index); }
            set { Set(index, value); }
        }

        private string Get(string index)
        {
            foreach (var item in args)
            {
                if (item.Key == index)
                {
                    return item.Value;
                }
            }

            return null;
        }

        public List<string> FindAll(string key)
        {
            List<string> vs = new List<string>();
            foreach (var item in args)
            {
                if (item.Key == key)
                {
                    vs.Add(item.Value);
                }
            }

            return vs;
        }

        private void Set(string index, string value)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Argument item = args[i];
                if (item.Key == index)
                {
                    if (value is null)
                        Remove(item);
                    else
                        args[i].Value = value;
                    return;
                }
            }

            Add(index, value);
        }

        public string ConvertToSave()
        {
            return JsonUtility.ToJson(this);
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var item in this)
            {
                ret = item.ToString() + "\n";
            }

            return ret;
        }

        public Arguments(IEnumerable<Argument> args)
        {
            this.args = args.ToArray();
        }

        private void Remove(Argument i)
        {
            args = args.Except(new Argument[] { i }).ToArray();
        }

        private void Add(string index, string value)
        {
            args = args.Append(new Argument(index, value)).ToArray();
        }

        public IEnumerator<Argument> GetEnumerator()
        {
            if (args == null) args = Array.Empty<Argument>();
            return ((IEnumerable<Argument>)args).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (args == null) args = Array.Empty<Argument>();
            return ((IEnumerable<Argument>)args).GetEnumerator();
        }

        public static implicit operator ArgumentList(Arguments args)
        {
            return new ArgumentList(args);
        }
    }
}