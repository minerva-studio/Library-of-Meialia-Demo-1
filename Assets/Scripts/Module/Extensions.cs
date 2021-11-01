using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minerva.Module
{
    public static class Extensions
    {
        public static T Exist<T>(this T instance) where T : UnityEngine.Object
        {
            return instance ? instance : null;
        }

        public static Vector3Int ToVector3Int(this Vector2Int vector2Int, int z = 0)
        {
            return new Vector3Int(vector2Int.x, vector2Int.y, z);
        }
        public static Vector2Int ToVector2Int(this Vector3Int vector2Int)
        {
            return new Vector2Int(vector2Int.x, vector2Int.y);
        }

        public static List<T> Clone<T>(this List<T> ts) where T : ICloneable
        {
            if (ts is null)
            {
                return null;
            }
            List<T> newList = new List<T>();
            foreach (T item in ts)
            {
                newList.Add((T)item.Clone());
            }
            return newList;
        }

        /// <summary>
        /// return a list with items of the same reference but in a different list instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static List<T> ShallowClone<T>(this List<T> ts)
        {
            if (ts is null)
            {
                return null;
            }
            List<T> newList = new List<T>();
            foreach (T item in ts)
            {
                newList.Add(item);
            }
            return newList;
        }


        /// <summary>
        /// return a list with a cloned items and in a different list instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static List<T> DeepClone<T>(this IEnumerable<T> ts) where T : ICloneable
        {
            if (ts is null) return null;

            List<T> newList = new List<T>();
            foreach (T item in ts)
            {
                newList.Add((T)item.Clone());
            }
            return newList;
        }


        public static IEnumerable<T> Clone<T>(this IEnumerable<T> ts) where T : ICloneable
        {
            List<T> newList = new List<T>();
            foreach (T item in ts)
            {
                newList.Add((T)item.Clone());
            }
            return newList;
        }

        /// <summary>
        /// find whether two IEnumerable matchs all elements in the list (same count, call IEquatable)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts1"></param>
        /// <param name="ts2"></param>
        /// <returns></returns>
        public static bool MatchAll<T>(this IEnumerable<T> ts1, IEnumerable<T> ts2)
        {
            if (ts1.Count() != ts2.Count()) return false;
            return ts1.Except(ts2).Count() == 0;
        }

        /// <summary>
        /// find whether fisrt IEnumerable contains all elements in the second list (all IEquatable)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts1"></param>
        /// <param name="ts2"></param>
        /// <returns></returns>
        public static bool ContainAll<T>(this IEnumerable<T> ts1, IEnumerable<T> ts2)
        {
            return ts2.Except(ts1).Count() == 0;
        }

        /// <summary>
        /// get the most common item in the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static T Mode<T>(this IEnumerable<T> ts)
        {
            Dictionary<T, int> keyValuePairs = new Dictionary<T, int>();
            foreach (var item in ts)
            {
                if (!keyValuePairs.ContainsKey(item)) keyValuePairs.Add(item, 1);
                else keyValuePairs[item] += 1;
            }
            foreach (var item in keyValuePairs) Debug.Log(item.Key + ": " + item.Value);
            return keyValuePairs.OrderByDescending(k => k.Value).FirstOrDefault((a) => true).Key;
        }

        public static List<UUID> ToUUIDList<T>(this List<T> ts) where T : class, IUUIDLabeled
        {
            List<UUID> uUIDs = new List<UUID>();
            foreach (T item in ts)
            {
                uUIDs.Add(item.UUID);
            }
            return uUIDs;
        }


        public static List<T1> Finds<T1, T2>(this IEnumerable<T1> list, T2[] findList) where T1 : INameable where T2 : INameable
        {
            List<T1> find = new List<T1>();
            foreach (T2 item in findList)
            {
                foreach (T1 listItem in list)
                {
                    if (listItem.Name == item.Name)
                    {
                        find.Add(listItem);
                    }
                }
            }
            return find;
        }

        //public static StatusList ToStatList(this IEnumerable<Status> stats)
        //{
        //    return new StatusList(stats);
        //}


        //public static string Color(this string message, Career career)
        //{
        //    string colorName;
        //    switch (career)
        //    {
        //        case Career.scholar:
        //            colorName = "blue";
        //            break;
        //        case Career.politician:
        //            colorName = "red";
        //            break;
        //        case Career.general:
        //            colorName = "yellow";
        //            break;
        //        case Career.merchant:
        //            colorName = "green";
        //            break;
        //        default:
        //            colorName = "white";
        //            break;
        //    }
        //    return message.Color(colorName);
        //}
        public static string Color(this string message, string colorName)
        {
            return "<color=" + colorName + ">" + message + "</color>";
        }

        public static string Color(this string message, int color)
        {
            return "<color=" + Convert.ToString(color, 16) + ">" + message + "</color>";
        }

        public static string AutoColor(this string message)
        {
            int lastIndex = 0;
            List<string> section = new List<string>();
            List<int> numbers = new List<int>();
            for (int i = 0; i < message.Length; i++)
            {
                char cha = message[i];
                if (int.TryParse(cha.ToString(), out int integer))
                {
                    int number = integer;
                    string before = message.Remove(i);
                    section.Add(before);
                    lastIndex = i;
                    i++;
                    for (; i < message.Length; i++)
                    {
                        if (int.TryParse(message[i].ToString(), out integer))
                        {
                            number = number * 10 + integer;
                            lastIndex = i;
                        }
                        else
                        {
                            numbers.Add(number);
                        }
                    }

                }
            }
            string remaining = message.Remove(0, lastIndex + 1);
            string ret = string.Empty;
            for (int i = 0; i < section.Count; i++)
            {
                ret += section[i] + numbers[i].ToString().Color("yellow");
            }
            return ret + remaining;
        }

        //public static Color GetColor(this Career career)
        //{
        //    switch (career)
        //    {
        //        case Career.scholar:
        //            return UnityEngine.Color.blue;
        //        case Career.politician:
        //            return UnityEngine.Color.red;
        //        case Career.general:
        //            return UnityEngine.Color.yellow;
        //        case Career.merchant:
        //            return UnityEngine.Color.green;
        //        default:
        //            return UnityEngine.Color.white;
        //    }
        //}

        //public static Color GetColor(this Rarity rarity)
        //{
        //    switch (rarity)
        //    {
        //        case Rarity.common:
        //            return UnityEngine.Color.white;
        //        case Rarity.rare:
        //            return UnityEngine.Color.green;
        //        case Rarity.epic:
        //            return UnityEngine.Color.blue;
        //        case Rarity.legendary:
        //            return UnityEngine.Color.yellow;
        //        default:
        //            return UnityEngine.Color.red;
        //    }
        //}

        public static T[] GetAllValue<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)) as T[];

        }

        public static Vector3 ToVector3(this string sVector)
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            Vector3 result = new Vector3(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]));

            return result;
        }

        public static bool IsVector2(this string sVector)
        {
            try
            {
                sVector.ToVector2();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Vector2 ToVector2(this string sVector)
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            Vector2 result = new Vector2(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]));

            return result;
        }

        public static Vector3Int ToVector3Int(this string sVector)
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            Vector3Int result = new Vector3Int(
                int.Parse(sArray[0]),
                int.Parse(sArray[1]),
                int.Parse(sArray[2]));

            return result;
        }

        public static bool IsVector3(this string sVector)
        {
            try
            {
                sVector.ToVector3();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Vector2Int ToVector2Int(this string sVector)
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            Vector2Int result = new Vector2Int(
                int.Parse(sArray[0]),
                int.Parse(sArray[1]));

            return result;
        }
    }
}

namespace Canute.Module
{
    public static class Extensions2
    {
        public static List<T> Clone<T>(this List<T> ts) where T : struct
        {
            if (ts is null)
            {
                return null;
            }
            List<T> newList = new List<T>();
            foreach (T item in ts)
            {
                T a = item;
                newList.Add(a);
            }
            return newList;
        }
    }
}