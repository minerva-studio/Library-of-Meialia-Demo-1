using Minerva.Module;
using System;

namespace Canute.Module
{
    [Serializable]
    public abstract class SerialzablePair : INameable
    {
        public abstract string Name { get; }
    }

    [Serializable]
    public class SerialzablePair<T1, T2> : SerialzablePair where T1 : INameable
    {
        public T1 key;
        public T2 value;

        public override string Name => key.Name;
    }

    public class SerialzablePairDictionary<T> : DataList<T> where T : SerialzablePair
    {

    }
}