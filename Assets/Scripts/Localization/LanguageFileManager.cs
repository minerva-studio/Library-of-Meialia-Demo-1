using Minerva.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Minerva.Localization
{

    [Serializable]
    [CreateAssetMenu(fileName = "Lang File Manager", menuName = "Localization/Language File Manager")]
    public class LanguageFileManager : ScriptableObject
    {
        [Header("Standard Text File")]
        public LanguageFile standard;
        [Header("Text File")]
        public List<LanguageFile> list;
        [Header("Word Pair")]
        public List<SingleWordPair> textContentList;

        [Header("Missing")]
        public List<SingleWordPair> missingKeys;

        private char seperator = '\t';




        private string GetPath()
        {
            string v = Application.dataPath;
            var currentPath = AssetDatabase.GetAssetPath(this).Split('/').ToList();
            currentPath.RemoveAt(0);
            if (currentPath.Count > 0) currentPath.RemoveAt(currentPath.Count - 1);
            foreach (var item in currentPath)
            {
                v = v + "/" + item;

            }
            return v + "/" + GetFileName();
        }

        private string GetFileName()
        {
            return name + ".csv";
        }



        //[ContextMenu("Load all")]
        public void LoadAll()
        {
            textContentList.Clear();
            foreach (var item in standard.Dictionary)
            {
                textContentList.Add(new SingleWordPair() { id = item.Key });
            }
            foreach (var languageList in list)
            {
                foreach (var item in languageList.Dictionary)
                {
                    var singleWordPairList = textContentList.Where((p) => p.id == item.Key);
                    if (singleWordPairList.Count() != 0) { singleWordPairList.First().content.Add(new Argument(languageList.Language.ToString(), item.Value.Replace("\r", "").Replace("\n", ""))); }
                }
            }
            textContentList.Remove(textContentList.FirstOrDefault((p) => p.id == "Language pack"));
        }

        public void AddMissingKey(string key)
        {
            if (missingKeys.FirstOrDefault(p => p.id == key) != null) return;

            SingleWordPair item = new SingleWordPair();
            item.id = key;
            item.content = new List<Argument>();

            foreach (var pair in textContentList[0].content)
            {
                item.content.Add(new Argument(pair.Key, ""));
            }
            missingKeys.Add(item);
        }




        public string ConvertToPureString()
        {
            string output = "Language pack" + seperator;
            foreach (var text in list) output += text.Language.ToString() + seperator;

            output = output.Remove(output.Length - 1);
            output += "\n";

            foreach (var item in textContentList)
            {
                output += item.id + seperator;
                foreach (var text in item.content)
                {
                    output += text.Value + seperator;
                }
                output = output.Remove(output.Length - 1);
                output += "\n";
            }
            return output;
        }




        public LanguageFile GetLanguageFile(string name)
        {
            foreach (var item in list)
            {
                if (item.Language.ToString() == name)
                {
                    return item;
                }
            }
            return null;
        }





        /// <summary>
        /// Get the matched display language from dictionary by key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Lang(string language, string key, bool useDefault = false)
        {
            LanguageFile textFile = useDefault ? standard : GetLanguageFile(language);
            List<string> commonEnding = new List<string> { "name", "info", "title", "subtitle" };
            try
            {
                string v = textFile.Dictionary[key];
                if (v != null) return v;
                v = textFile.Dictionary[key + "."];
                if (v != null) return v;
                throw new KeyNotFoundException();
            }
            catch (KeyNotFoundException)
            {
                Debug.LogWarning(key);
                AddMissingKey(key);
                return KeyNotFoundSolution(textFile, key, commonEnding);
            }
        }

        private string KeyNotFoundSolution(LanguageFile textFile, string key, List<string> commonEnding)
        {
            if (missingKeys.Find(s => s.id == key) == null)
            {
                Debug.LogWarning(key);
            }

            switch (textFile.MissingKeySolution)
            {
                case KeyMatchMode.AllowEmpty:
                    return "";
                case KeyMatchMode.RawDisplay:
                    return key;
                case KeyMatchMode.ForceDisplay:
                default:
                    string[] vs = key.Split('.');
                    return commonEnding.Contains(vs[vs.Length - 1]) ? vs[vs.Length - 2] : vs[vs.Length - 1];
            }
        }





        [ContextMenu("Export to CSV")]
        public void Export()
        {
            string output = ConvertToPureString();
            string path = GetPath();
            Debug.Log(path);
            File.WriteAllText(path, output, System.Text.Encoding.Unicode);
        }

        [ContextMenu("Import from CSV")]
        public void Import()
        {
            EditorUtility.SetDirty(this);

            textContentList = new List<SingleWordPair>();
            string path = GetPath();
            string input = File.ReadAllText(path);
            var lines = input.Split('\n').ToList();
            Debug.Log(lines[0]);
            var pack = lines[0].Split(seperator).ToList();

            foreach (var line in lines)
            {
                var word = line.Split(seperator).ToList();
                if (word[0] == "Language pack") continue;
                SingleWordPair item = new SingleWordPair
                {
                    id = word[0],
                    content = new List<Argument>()
                };
                int count = word.Count;
                Debug.Log(count);
                for (int i = 1; i < Mathf.Min(pack.Count, count); i++)
                {
                    item.content.Add(new Argument(pack[i], word[i]));
                }
                textContentList.Add(item);
            }
            textContentList.Remove(textContentList.FirstOrDefault((p) => p.id == "Language pack"));
            //EditorUtility.ClearDirty(this);
        }

        [ContextMenu("Save to all small file")]
        public void SaveToTextFiles()
        {
            foreach (var item in list)
            {
                EditorUtility.SetDirty(item);
            }

            foreach (var textFile in list)
            {
                textFile.Dictionary.Clear();
            }

            foreach (var item in textContentList)
            {
                foreach (var textFile in list)
                {
                    string value = item.GetValue(textFile.Language.ToString());
                    Debug.LogError(textFile.Language.ToString());
                    Debug.LogError(value);
                    Argument pair = new Argument(item.id, value);
                    textFile.Dictionary.Add(pair);
                }
            }
        }


        [ContextMenu("Sort")]
        public void Sort() => textContentList.Sort();

        [ContextMenu("Sort Missing Keys")]
        public void SortMissing() => missingKeys.Sort();

        [ContextMenu("Clear Obsolete Missing Keys")]
        public void ClearObsoleteMissingKeys()
        {
            foreach (var item in missingKeys.ShallowClone())
            {
                if (textContentList.Contains(item)) missingKeys.Remove(item);
            }
        }
    }
}