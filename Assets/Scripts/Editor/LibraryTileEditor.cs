using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Amlos
{
#if UNITY_EDITOR
    /// <summary>
    /// The Editor for a DestroyableTile.
    /// </summary>
    [CustomEditor(typeof(LibraryTile), true)]
    [CanEditMultipleObjects]
    public class LibraryTileEditor : UnityEditor.Editor
    {
        /// <summary>
        /// The RuleTile being edited
        /// </summary>
        public LibraryTile tile => target as LibraryTile;

        /// <summary>
        /// Preview Utility for rendering previews
        /// </summary>
        public PreviewRenderUtility m_PreviewUtility;
        /// <summary>
        /// Grid for rendering previews
        /// </summary>
        public Grid m_PreviewGrid;
        /// <summary>
        /// List of Tilemaps for rendering previews
        /// </summary>
        public List<Tilemap> m_PreviewTilemaps;
        /// <summary>
        /// List of TilemapRenderers for rendering previews
        /// </summary>
        public List<TilemapRenderer> m_PreviewTilemapRenderers;

        /// <summary>
        /// Draws the Inspector GUI for the RuleTileEditor
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            tile.sprite = EditorGUILayout.ObjectField("Default Sprite", tile.sprite, typeof(Sprite), true) as Sprite;
            tile.gameObject = EditorGUILayout.ObjectField("Default GameObject", tile.gameObject, typeof(GameObject), true) as GameObject;
            tile.colliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup("Default Collider", tile.colliderType);

            //DrawCustomFields(false);

            EditorGUILayout.Space();

            if (EditorGUI.EndChangeCheck())
                SaveTile();
        }

        /// <summary>
        /// Saves any changes to the RuleTile
        /// </summary>
        public void SaveTile()
        {
            EditorUtility.SetDirty(target);
            SceneView.RepaintAll();
        }

        /// <summary>
        /// Renders a static preview Texture2D for a RuleTile asset
        /// </summary>
        /// <param name="assetPath">Asset path of the RuleTile</param>
        /// <param name="subAssets">Arrays of assets from the given Asset path</param>
        /// <param name="width">Width of the static preview</param>
        /// <param name="height">Height of the static preview </param>
        /// <returns>Texture2D containing static preview for the RuleTile asset</returns>
        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            if (tile.sprite != null)
            {
                Type t = GetType("UnityEditor.SpriteUtility");
                if (t != null)
                {
                    MethodInfo method = t.GetMethod("RenderStaticPreview", new Type[] { typeof(Sprite), typeof(Color), typeof(int), typeof(int) });
                    if (method != null)
                    {
                        object ret = method.Invoke("RenderStaticPreview", new object[] { tile.sprite, Color.white, width, height });
                        if (ret is Texture2D)
                            return ret as Texture2D;
                    }
                }
            }
            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }


        /// <summary>
        /// Draw editor fields for custom properties for the RuleTile
        /// </summary>
        /// <param name="isOverrideInstance">Whether override fields are drawn</param>
        public void DrawCustomFields(bool isOverrideInstance)
        {
            //var customFields = tile.GetCustomFields(isOverrideInstance);

            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            //foreach (var field in customFields)
            //{
            //    var property = serializedObject.FindProperty(field.Name);
            //    if (property != null)
            //        EditorGUILayout.PropertyField(property, true);
            //}

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                DestroyPreview();
                CreatePreview();
            }


        }
        /// <summary>
        /// Whether the RuleTile has a preview GUI
        /// </summary>
        /// <returns>True</returns>
        public override bool HasPreviewGUI()
        {
            return true;
        }

        /// <summary>
        /// Draws the preview GUI for the RuleTile
        /// </summary>
        /// <param name="rect">Rect to draw the preview GUI</param>
        /// <param name="background">The GUIStyle of the background for the preview</param>
        public override void OnPreviewGUI(Rect rect, GUIStyle background)
        {
            if (m_PreviewUtility == null)
                CreatePreview();

            if (Event.current.type != EventType.Repaint)
                return;

            m_PreviewUtility.BeginPreview(rect, background);
            m_PreviewUtility.camera.orthographicSize = 2;
            if (rect.height > rect.width)
                m_PreviewUtility.camera.orthographicSize *= (float)rect.height / rect.width;
            m_PreviewUtility.camera.Render();
            m_PreviewUtility.EndAndDrawPreview(rect);
        }


        /// <summary>
        /// Handles cleanup for the Preview GUI
        /// </summary>
        protected virtual void DestroyPreview()
        {
            if (m_PreviewUtility != null)
            {
                m_PreviewUtility.Cleanup();
                m_PreviewUtility = null;
                m_PreviewGrid = null;
                m_PreviewTilemaps = null;
                m_PreviewTilemapRenderers = null;
            }
        }


        /// <summary>
        /// Creates a Preview for the RuleTile.
        /// </summary>
        protected virtual void CreatePreview()
        {
            m_PreviewUtility = new PreviewRenderUtility(true);
            m_PreviewUtility.camera.orthographic = true;
            m_PreviewUtility.camera.orthographicSize = 2;
            m_PreviewUtility.camera.transform.position = new Vector3(0, 0, -10);

            var previewInstance = new GameObject();
            m_PreviewGrid = previewInstance.AddComponent<Grid>();
            m_PreviewUtility.AddSingleGO(previewInstance);

            m_PreviewTilemaps = new List<Tilemap>();
            m_PreviewTilemapRenderers = new List<TilemapRenderer>();

            for (int i = 0; i < 4; i++)
            {
                var previewTilemapGo = new GameObject();
                m_PreviewTilemaps.Add(previewTilemapGo.AddComponent<Tilemap>());
                m_PreviewTilemapRenderers.Add(previewTilemapGo.AddComponent<TilemapRenderer>());

                previewTilemapGo.transform.SetParent(previewInstance.transform, false);
            }

            for (int x = -2; x <= 0; x++)
                for (int y = -1; y <= 1; y++)
                    m_PreviewTilemaps[0].SetTile(new Vector3Int(x, y, 0), tile);

            for (int y = -1; y <= 1; y++)
                m_PreviewTilemaps[1].SetTile(new Vector3Int(1, y, 0), tile);

            for (int x = -2; x <= 0; x++)
                m_PreviewTilemaps[2].SetTile(new Vector3Int(x, -2, 0), tile);

            m_PreviewTilemaps[3].SetTile(new Vector3Int(1, -2, 0), tile);
        }


        /// <summary>
        /// Draw editor fields for custom properties for the RuleTile
        /// </summary>
        /// <param name="isOverrideInstance">Whether override fields are drawn</param>
        //public void DrawCustomFields(bool isOverrideInstance)
        //{
        //    var customFields = tile.GetCustomFields(isOverrideInstance);

        //    serializedObject.Update();
        //    EditorGUI.BeginChangeCheck();
        //    foreach (var field in customFields)
        //    {
        //        var property = serializedObject.FindProperty(field.Name);
        //        if (property != null)
        //            EditorGUILayout.PropertyField(property, true);
        //    }

        //    if (EditorGUI.EndChangeCheck())
        //    {
        //        serializedObject.ApplyModifiedProperties();
        //        DestroyPreview();
        //        CreatePreview();
        //    }

        //}

        private static Type GetType(string TypeName)
        {
            var type = Type.GetType(TypeName);
            if (type != null)
                return type;

            var currentAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            foreach (var assemblyName in referencedAssemblies)
            {
                var assembly = Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    type = assembly.GetType(TypeName);
                    if (type != null)
                        return type;
                }
            }
            return null;
        }



        /// <summary>
        /// OnEnable  
        /// </summary>
        public virtual void OnEnable()
        {
        }
        /// <summary>
        /// OnDisable  
        /// </summary>
        public virtual void OnDisable()
        {
            DestroyPreview();
        }
    }
#endif
}

