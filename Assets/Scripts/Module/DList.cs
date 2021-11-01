using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minerva.Module
{
    [Serializable]
    public class DList<T> : IEnumerable<T> where T : class
    {
        [SerializeField] protected List<T> list;

        public T this[int index] { get => list[index]; set => list[index] = value; }

        public virtual void Add(T item)
        {
            list.Add(item);
        }

        public virtual void AddRange(IEnumerable<T> enumerable)
        {
            list.AddRange(enumerable);
        }

        public virtual bool Remove(T item)
        {
            return list.Remove(item);
        }

        public virtual void Clear()
        {
            list.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }
        public virtual IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }

        public virtual int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public virtual bool Contains(T item)
        {
            return list.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public virtual int Count => list.Count;

        public virtual bool IsReadOnly { get; set; }

        public virtual T[] ToArray()
        {
            return list.ToArray();
        }

        public DList()
        {

        }

        public DList(IEnumerable<T> list)
        {
            this.list = list.ToList();
        }

        public DList(int capacity)
        {
            list.Capacity = capacity;
        }

        public static implicit operator List<T>(DList<T> list)
        {
            return list.list;
        }

        public static explicit operator DList<T>(List<T> ts)
        {
            return new DList<T>(ts);
        }
    }
}