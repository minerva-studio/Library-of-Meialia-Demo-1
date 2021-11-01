using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minerva.Localization
{
    [RequireComponent(typeof(Text))]
    public abstract class LanguageLoaderBase : MonoBehaviour
    {
        public static List<LanguageLoaderBase> loaders = new List<LanguageLoaderBase>();
        public LanguageFileManager languageFileManager;
        public Text textField;

        [Header("Key")]
        public string key;

        public void Awake()
        {
            loaders.Add(this);
        }
        public void Start()
        {
            Load();
        }

        public void OnDestroy()
        {
            loaders.Remove(this);
        }



        public void Load()
        {
            if (textField) textField.text = key.Lang();
        }

        public abstract void Load(string key);


        public static void ReloadAll()
        {
            foreach (var item in loaders)
            {
                if (item) item.Load();
            }
        }

    }
}