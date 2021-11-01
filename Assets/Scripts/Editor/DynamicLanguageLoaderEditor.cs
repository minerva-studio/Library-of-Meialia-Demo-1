using Minerva.Localization;
using UnityEditor;
using UnityEngine;

namespace Minerva.Localization.Editor
{
    [CustomEditor(typeof(DynamicLanguageLoader))]
    public class DynamicLanguageLoaderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DynamicLanguageLoader textFileManager = (DynamicLanguageLoader)target;

            base.OnInspectorGUI();

            GUILayout.Space(10);
            if (textFileManager.HasPossibleKeys())
                if (GUILayout.Button("Use selected Key"))
                {
                    textFileManager.UseSelectedKey();
                    textFileManager.OnValidate();
                    textFileManager.ResetEditor();
                }

            if (GUILayout.Button("Use selected suffix"))
            {
                textFileManager.UseSuffix();
                textFileManager.OnValidate();
                textFileManager.ResetEditor();
            }

            if (GUILayout.Button("Clear Key"))
            {
                textFileManager.referenceKey = "";
                textFileManager.OnValidate();
                textFileManager.ResetEditor();
            }
            //GUILayout.Box("sele");
            //GUILayout.Box("");
        }
    }
}