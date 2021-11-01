using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Minerva.Localization
{
    [RequireComponent(typeof(Text))]
    public class DynamicLanguageLoader : LanguageLoaderBase
    {
        public string parameter;
        public string referenceKey = "";
        public string suffix = "";


        [Space(10)]
        [Header("Editor")]
        private string[] keys = System.Array.Empty<string>();
        private string[] suffixs = System.Array.Empty<string>();
        [Dropdown("keys"), SerializeField] private string selectedReferenceKey;
        [Dropdown("suffixs"), SerializeField] private string selectedSuffix;


        public void OnValidate()
        {
            textField = GetComponent<Text>();
            keys = languageFileManager.standard.FindUniquePaths().ToArray();
            suffixs = languageFileManager.standard.FindAllSuffix().ToArray();
        }

        public void UseSelectedKey()
        {
            referenceKey = selectedReferenceKey.Remove(selectedReferenceKey.Length - 1);
        }

        public void UseSuffix()
        {
            suffix = selectedSuffix;
        }

        public bool HasPossibleKeys()
        {
            return keys.Length > 0 && keys[0] != string.Empty;
        }

        public void ResetEditor()
        {
            selectedReferenceKey = keys.FirstOrDefault();
            suffix = suffixs.FirstOrDefault();
        }

        public override void Load(string parameter)
        {
            this.parameter = parameter;
            this.key = referenceKey + "." + parameter + "." + suffix;
            Load();
        }

#if UNITY_EDITOR

        [MenuItem("CONTEXT/Text/Add Dynamic Language Loader")]
        public static void AddComponent(MenuCommand command)
        {
            Text body = (Text)command.context;
            body.gameObject.AddComponent<LanguageLoader>();
        }

#endif
    }
}
