using Minerva.Localization;
using UnityEditor;
using UnityEngine;

namespace Minerva.Localization.Editor
{
    [CustomEditor(typeof(LanguageLoader)), CanEditMultipleObjects]
    public class LanguageLoaderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            LanguageLoader textFileManager = (LanguageLoader)target;
            base.OnInspectorGUI();

            GUILayout.Space(10);



            if (textFileManager.HasReferenceKey())
                if (GUILayout.Button("Use selected reference Key"))
                {
                    textFileManager.UseReferenceKey();
                    textFileManager.OnValidate();
                    textFileManager.ResetEditor();
                }

            if (textFileManager.HasPossibleKeys())
                if (GUILayout.Button("Use selected Key"))
                {
                    textFileManager.UseSelectedKey();
                    textFileManager.OnValidate();
                    textFileManager.ResetEditor();
                }

            if (textFileManager.HasPossibleNextClass())
                if (GUILayout.Button("Open selected class"))
                {
                    textFileManager.OpenSelectedClass();
                    textFileManager.OnValidate();
                    textFileManager.ResetEditor();
                }

            if (GUILayout.Button("Clear Key"))
            {
                textFileManager.key = "";
                textFileManager.OnValidate();
                textFileManager.ResetEditor();
            }
            //GUILayout.Box("sele");
            //GUILayout.Box("");
        }
    }
}