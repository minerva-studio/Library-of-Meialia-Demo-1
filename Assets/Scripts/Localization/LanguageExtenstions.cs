using Amlos;
using Amlos.Core;
using Minerva.Module;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Minerva.Localization
{
    public static class LanguageExtenstions
    {
        public static LanguageFileManager LanguageFileManager => GameData.instance.languageFilesManager;
        public static string gameLanguage => Simulation.GetModel<Game>().Language;


        #region 基本  

        /// <summary>
        /// Get the matched display language from dictionary by key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Lang(this string key, bool useDefault = false)
        {
            return LanguageFileManager.Lang(gameLanguage, key, useDefault);
        }






        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary> 
        /// <param name="param"></param>
        /// <returns></returns> 
        public static string Lang(params string[] param)
        {
            string fullName = "";

            if (param.Length >= 1)
            {
                fullName = param[0];
            }

            for (int i = 1; i < param.Length; i++)
            {
                fullName += "." + param[i];
            }

            return fullName.Lang(false);
        }
        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary> 
        /// <param name="param"></param>
        /// <returns></returns> 
        public static string Lang(bool useDefault = false, params string[] param)
        {
            string fullName = "";

            if (param.Length >= 1)
            {
                fullName = param[0];
            }

            for (int i = 1; i < param.Length; i++)
            {
                fullName += "." + param[i];
            }

            return fullName.Lang(useDefault);
        }
        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary> 
        /// <param name="instance"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public static string Lang(this object instance, string name, params string[] param)
        {
            List<string> vs = new List<string>() { instance.GetFullTypeName() + "." + name };
            vs.AddRange(param);
            return Lang(false, vs.ToArray());
        }
        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary> 
        /// <param name="instance"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public static string Lang(this object instance, string name, bool useDefault, params string[] param)
        {
            List<string> vs = new List<string>() { instance.GetFullTypeName() + "." + name };
            vs.AddRange(param);
            return Lang(useDefault, vs.ToArray());
        }
        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public static string Lang<T>(this T instance, params string[] param) where T : INameable
        {
            return instance.Lang(instance.Name, false, param);
        }
        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public static string Lang<T>(this T instance, bool useDefault = false, params string[] param) where T : INameable
        {
            return instance.Lang(instance.Name, useDefault, param);
        }
        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary> 
        /// <param name="instance"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public static string Lang(this Type instance, string name, bool useDefault = false, params string[] param)
        {
            List<string> vs = new List<string>() { instance.FullName + "." + name };
            vs.AddRange(param);
            return Lang(useDefault, vs.ToArray());
        }



        #region Enum
        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary>
        /// <typeparam name="T"></typeparam> 
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public static string Lang<T>(bool useDefault, params string[] param) where T : Enum
        {
            List<string> vs = new List<string>() { GetFullTypeName<T>() };
            vs.AddRange(param);
            return Lang(useDefault, vs.ToArray());
        }
        /// <summary>
        /// Get the matched display language from dictionary from a instance and a parameter
        /// </summary>
        /// <typeparam name="T"></typeparam> 
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public static string Lang<T>(params string[] param) where T : Enum
        {
            List<string> vs = new List<string>() { GetFullTypeName<T>() };
            vs.AddRange(param);
            return Lang(false, vs.ToArray());
        }

        public static string Lang<T>(this T instance) where T : Enum
        {
            string name = instance.ToString();

            name = name[0].ToString().ToLower() + name.Remove(0, 1);
            string key = instance.GetFullTypeName() + "." + name;
            return key.Lang();
        }

        #endregion








        public static string GetFullTypeName<T>(this T instance)
        {
            return instance.GetType().FullName;
        }

        public static string GetFullTypeName<T>()
        {
            return typeof(T).FullName;
        }
        #endregion

    }
}