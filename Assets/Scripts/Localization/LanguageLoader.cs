using Minerva.Module;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Minerva.Localization
{
    [RequireComponent(typeof(Text))]
    public class LanguageLoader : LanguageLoaderBase
    {
        [Space(10)]
        [Header("Editor")]
        private string[] referenceKeys = System.Array.Empty<string>();
        private string[] possibleKeys = System.Array.Empty<string>();
        private string[] possibleNextClass = System.Array.Empty<string>();
        [Dropdown("referenceKeys"), SerializeField] private string referenceKey;
        [Dropdown("possibleKeys"), SerializeField] private string selection;
        [Dropdown("possibleNextClass"), SerializeField] private string nextClass;




        public void OnValidate()
        {
            textField = GetComponent<Text>();
            referenceKeys = languageFileManager.standard.FindUniquePaths().ToArray();
            possibleKeys = languageFileManager.standard.FindMatchedKeys(key).ToArray();
            possibleNextClass = languageFileManager.standard.FindPossibleNextClass(key).ToArray();
        }



        public void UseReferenceKey()
        {
            key = referenceKey;
        }
        public void UseSelectedKey()
        {
            key = selection;
        }
        public void OpenSelectedClass()
        {
            key = key + nextClass;
        }

        public bool HasReferenceKey()
        {
            return referenceKeys.Length > 0 && referenceKeys[0] != string.Empty;
        }
        public bool HasPossibleKeys()
        {
            return possibleKeys.Length > 0 && possibleKeys[0] != string.Empty;
        }

        public bool HasPossibleNextClass()
        {
            return possibleNextClass.Length > 0 && possibleNextClass[0] != string.Empty;
        }

        public void ResetEditor()
        {
            selection = possibleKeys.FirstOrDefault();
            nextClass = possibleNextClass.FirstOrDefault();
        }






        public override void Load(string key)
        {
            this.key = key;
            Load();
        }


#if UNITY_EDITOR

        [MenuItem("CONTEXT/Text/Add Language Loader")]
        public static void AddComponent(MenuCommand command)
        {
            Text body = (Text)command.context;
            body.gameObject.AddComponent<LanguageLoader>();
        }

#endif
    }
}
