using Minerva.Module;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Minerva.Localization
{
    [CreateAssetMenu(fileName = "Language File", menuName = "Localization/Language File")]
    public class LanguageFile : ScriptableObject
    {
        [SerializeField] private LocalizationCountry language;
        [SerializeField] private KeyMatchMode missingKeySolution;
        [SerializeField] private Dictionary dictionary;

        public LocalizationCountry Language { get => language; set => language = value; }
        public KeyMatchMode MissingKeySolution { get => missingKeySolution; set => missingKeySolution = value; }
        public Dictionary Dictionary { get => dictionary; set => dictionary = value; }


        public void OnValidate()
        {
#if UNITY_EDITOR
            //ForceLoadLang();
#endif
        }





        public List<string> FindMatchedKeys(string pKey)
        {
            List<string> keys = new List<string>();
            foreach (var item in dictionary)
            {
                if (item.Key.Contains(pKey) && item.Key != pKey) keys.Add(item.Key);
            }
            if (keys.Count == 0) keys.Add(string.Empty);
            return keys;
        }

        public List<string> FindPossibleNextClass(string pKey)
        {
            int pKeyClassCount = pKey.Split('.').Length - 1;
            //if (!pKey.EndsWith(".") && pKey != string.Empty) return new List<string>() { "" };
            List<string> classes = new List<string>();
            var list = FindMatchedKeys(pKey);
            foreach (var item in list)
            {
                string[] vs = item.Split('.');
                if (pKeyClassCount >= vs.Length) continue;
                string @class = vs[pKeyClassCount];
                if (!classes.Contains(@class + "."))
                {
                    classes.Add(@class + ".");
                }
                else if (!classes.Contains(@class))
                {
                    classes.Add(@class);
                }

            }
            if (classes.Count == 0) classes.Add(string.Empty);
            return classes;
        }

        public List<string> FindUniquePaths()
        {
            List<string> classes = new List<string>();
            foreach (var item in dictionary)
            {
                var @class = item.Key.Split('.').Select(s => (s as IEnumerable<char>).ToList()).ToList();
                if (@class.Count < 2) continue;
                for (int i = 0; i < 2; i++)
                {
                    if (@class.Count - 1 >= 0) @class.RemoveAt(@class.Count - 1);
                }
                @class.ForEach(s => s.Add('.'));
                var reformed = new string(@class.SelectMany(s => s).ToArray());
                reformed.Remove(reformed.Length - 1);
                if (!classes.Contains(reformed))
                {
                    classes.Add(reformed);
                }
            }
            return classes;
        }

        public List<string> FindAllSuffix()
        {
            List<string> classes = new List<string>();
            foreach (var item in dictionary)
            {
                string suffix = item.Key.Split('.').LastOrDefault();
                if (!classes.Contains(suffix)) { Debug.Log(suffix); classes.Add(suffix); }
            }
            return classes;
        }




        /// <summary>
        /// 语言文件解释器, 加载语言包
        /// </summary>
        public IEnumerator LoadLang()
        {
            Debug.Log("loading language pack " + Language);
            TextAsset LanguagePack = Resources.Load("Lang/" + Language, typeof(TextAsset)) as TextAsset;
            yield return LanguagePack;
            string lang = LanguagePack.text;
            string[] langtoarray = lang.Split('\n');
            foreach (string wordset in langtoarray)
            {
                if (wordset.StartsWith("#") || wordset.StartsWith("//"))
                {
                    continue;
                }
                if (!wordset.Contains("="))
                {
                    continue;
                }
                string[] word = wordset.Split('=');
                if (!Dictionary.ContainsKey(word[0]))
                {
                    Dictionary.Add(word[0], word[1]);
                }
            }
        }
        [ContextMenu("Load Pack")]
        /// <summary>
        /// 强行加载语言包
        /// </summary>
        public void ForceLoadLang()
        {
            Debug.Log("loading language pack " + Language);
            Dictionary = new Dictionary();
            TextAsset LanguagePack = Resources.Load("Lang/" + Language, typeof(TextAsset)) as TextAsset;
            string lang = LanguagePack.text;
            string[] langtoarray = lang.Split('\n');
            foreach (string wordset in langtoarray)
            {
                if (wordset.StartsWith("#") || wordset.StartsWith("//"))
                {
                    continue;
                }
                if (!wordset.Contains("="))
                {
                    continue;
                }
                string[] word = wordset.Split('=');
                if (!Dictionary.ContainsKey(word[0]))
                {
                    //Debug.Log("Add " + word[0] + ":" + word[1]);
                    Dictionary.Add(word[0], word[1]);
                }
            }
        }

        public string ConvertToPureString()
        {
            string output = "";
            foreach (var item in Dictionary)
            {
                output += item.Key + "=" + item.Value + "\n";
            }
            return output;
        }

        public void SaveToTXT()
        {
            string output = ConvertToPureString();
            string path = Application.dataPath + "/Resources/Lang/" + Language + ".txt";
            File.WriteAllText(path, output, System.Text.Encoding.UTF32);
        }
    }
}