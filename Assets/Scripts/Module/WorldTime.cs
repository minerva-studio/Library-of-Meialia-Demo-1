using System;
using UnityEngine;

namespace Minerva.Module
{
    [Serializable]
    public struct WorldTime
    {
        [SerializeField]
        private string value;
        public static implicit operator DateTime(WorldTime time)
        {
            try
            {
                return DateTime.Parse(time.value);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static implicit operator WorldTime(DateTime time)
        {
            return new WorldTime() { value = time.ToString() };
        }

        public bool Equals(WorldTime obj)
        {
            return obj.value == value;
        }
        public override bool Equals(object obj)
        {
            if (obj is WorldTime)
            {
                return Equals((WorldTime)obj);
            }
            return false;
        }

        public static bool operator ==(WorldTime left, WorldTime right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WorldTime left, WorldTime right)
        {
            return !(left == right);
        }

        //public static bool operator >(WorldTime left, WorldTime right)
        //{
        //    return !((DateTime)left > ((DateTime)right));
        //}
        //public static bool operator <(WorldTime left, WorldTime right)
        //{
        //    return !((DateTime)left < ((DateTime)right));
        //}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
