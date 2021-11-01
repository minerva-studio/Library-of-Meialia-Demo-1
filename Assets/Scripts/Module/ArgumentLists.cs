using System;
using UnityEngine;

namespace Minerva.Module
{
    public static class ArgumentLists
    {

        /// <summary>
        /// add args to the effect
        /// </summary>
        /// <param name="args"></param>
        public static void AddParams(this ArgumentList arguements, params Argument[] args)
        {
            foreach (var item in args)
            {
                arguements[item.Key] = item.Value;
            }
        }

        /// <summary>
        /// get a integer parameter by <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetIntParam(this ArgumentList arguements, string key)
        {
            string value = arguements[key];
            try
            {
                return int.Parse(value ?? "-1");
            }
            catch
            {
                Debug.LogError("Conversion Failed: " + value + "is not a integer");
                return -1;
            }
        }

        /// <summary>
        /// get a boolean parameter by <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBoolParam(this ArgumentList arguements, string key)
        {
            string value = arguements[key];
            if (bool.TryParse(value ?? "false", out bool result))
            {
                return result;
            }
            else
            {
                Debug.LogError("Conversion Failed: " + value + "is not a boolean");
                return false;
            }
        }

        /// <summary>
        /// get a double parameter by <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static double GetDoubleParam(this ArgumentList arguements, string key)
        {
            string value = arguements[key];
            try
            {
                return double.Parse(value ?? "-1");
            }
            catch
            {
                Debug.LogError("Conversion Failed: " + value + "is not a double");
                return -1;
            }
        }
        /// <summary>
        /// get a double parameter by <paramref name="key"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static UUID GetUUIDParam(this ArgumentList arguements, string key)
        {
            string value = arguements[key];
            try
            {
                return Guid.Parse(value ?? Guid.Empty.ToString());
            }
            catch
            {
                Debug.LogError("Conversion Failed: " + value + "is not a uuid");
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Get a enum type <typeparamref name="T"/> params by <paramref name="key"/>
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetEnumParam<T>(this ArgumentList arguements, string key) where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), arguements[key]);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return default;
            }
        }

    }
}
