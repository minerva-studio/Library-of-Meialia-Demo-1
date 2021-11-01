using Minerva.Localization;
using UnityEditor;
using UnityEngine;

namespace Minerva.Localization.Editor
{
    [CustomEditor(typeof(LanguageFileManager))]
    public class LanguageFileManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {

            LanguageFileManager textFileManager = (LanguageFileManager)target;

            if (GUILayout.Button("Import from .csv"))
            {
                textFileManager.Import();
            }
            if (GUILayout.Button("Save to all Text File"))
            {
                textFileManager.SaveToTextFiles();
            }
            if (GUILayout.Button("Import and save"))
            {
                textFileManager.Import();
                textFileManager.SaveToTextFiles();
            }
            GUILayout.Space(10);
            if (GUILayout.Button("Sort list"))
            {
                textFileManager.Sort();
            }
            if (GUILayout.Button("Sort missing keys"))
            {
                textFileManager.SortMissing();
            }
            GUILayout.Space(10);
            if (GUILayout.Button("Clear obsolete missing keys"))
            {
                textFileManager.ClearObsoleteMissingKeys();
            }
            if (GUILayout.Button("Export to .csv"))
            {
                textFileManager.Export();
            }
            GUILayout.Space(10);
            base.OnInspectorGUI();
        }
    }
}