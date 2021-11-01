using System.Collections.Generic;
using UnityEngine;

namespace Minerva.Module
{
    [SerializeField]
    public class ArgType<TValue> : INameable
    {
        [SerializeField] private string key;
        [SerializeField] private TValue value;

        public ArgType(string key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public string Key { get => key; set => key = value; }
        public TValue Value { get => value; set => this.value = value; }
        public string Name => key;
    }
}

