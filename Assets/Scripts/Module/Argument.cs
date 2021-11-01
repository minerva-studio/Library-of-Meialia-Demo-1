using UnityEngine;
using System;
using System.Collections.Generic;

namespace Minerva.Module
{
    [Serializable]
    public struct Argument : INameable, IEquatable<Argument>
    {
        [SerializeField] private string key;
        [SerializeField] private string value;

        public string Key { get => key; set => key = value; }
        public string Value { get => value; set => this.value = value; }
        public string Name => Key;

        public Argument(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public static implicit operator KeyValuePair<string, string>(Argument arg)
        {
            return new KeyValuePair<string, string>(arg.key, arg.value);
        }

        public static implicit operator Argument(KeyValuePair<string, string> arg)
        {
            return new Argument(arg.Key, arg.Value);
        }

        public static implicit operator ArgType<string>(Argument arg)
        {
            return new ArgType<string>(arg.Key, arg.Value);
        }

        public static implicit operator Argument(string @string)
        {
            string[] vs;
            Argument arg = new Argument();

            if (@string is null)
            {
                throw new ArgumentException();
            }

            if (@string.Split(':').Length > 2 || @string.Split(':').Length < 1)
            {
                throw new ArgumentException();
            }

            vs = @string.Split(':');
            arg = new Argument(vs[0], "");

            if (vs.Length > 1)
            {
                arg.value = vs[1];
            }

            return arg;
        }

        public bool Equals(Argument other)
        {
            return other.value == value && other.key == key;
        }

        public override bool Equals(object obj)
        {
            return obj is Argument ? Equals((Argument)obj) : false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Argument left, Argument right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Argument left, Argument right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return '{' + key + ": " + value + '}';
        }
    }
}