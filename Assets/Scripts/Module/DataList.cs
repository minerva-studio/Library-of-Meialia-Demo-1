using Canute.Module;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minerva.Module
{
    [Serializable]
    public class DataList<T> : IDataList<T> where T : INameable
    {
        [SerializeField] protected List<T> list = new List<T>();

        public virtual T Get(string name)
        {
            if (list == null)
            {
                return default;
            }
            foreach (T item in list)
            {
                if (item?.Name == name)
                {
                    return item;
                }
            }
            return default;
        }

        public virtual T this[int index] { get => list[index]; set => list[index] = value; }
        public virtual T this[string name] { get => Get(name); set => list[list.IndexOf(Get(name))] = value; }
        object IDataList.this[int index] { get => list[index]; set => list[index] = (T)value; }

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
            return GetEnumerator();
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

        object IDataList.Get(string name)
        {
            return Get(name);
        }


        public DataList()
        {
            list = new List<T>();
        }

        public DataList(IEnumerable<T> list)
        {
            this.list = list.ToList();
        }

        public DataList(int capacity)
        {
            list.Capacity = capacity;
        }

        public static implicit operator List<T>(DataList<T> list)
        {
            return list.list;
        }

        public static explicit operator DataList<T>(List<T> ts)
        {
            return new DataList<T>(ts);
        }
    }
}

namespace Canute.Module
{
    public interface IDataList<T> : IDataList, IList<T>, IEnumerable<T>
    {
        T this[string name] { get; set; }

        new T Get(string name);
    }

    public interface IDataList
    {
        object this[int id] { get; set; }
        object Get(string name);
    }
}