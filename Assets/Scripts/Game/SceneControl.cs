using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Amlos
{
    public enum MainScene
    {
        mainHall,
        settings,
    }

    [CreateAssetMenu(fileName = "Scene Control", menuName = "Game Data/Scene Control", order = 1)]
    public class SceneControl : ScriptableObject
    {
        public const string main = "mainHall";
        public const string settings = "Settings";

        public static List<string> lastScene = new List<string>();

        private static string GetName(MainScene scene)
        {
            string ans = "";
            switch (scene)
            {
                case MainScene.mainHall:
                    ans = main;
                    break;
                case MainScene.settings:
                    ans = settings;
                    break;
                default:
                    break;
            }
            return ans;
        }

        public static void GotoScene(MainScene scene)
        {
            lastScene.Add(SceneManager.GetActiveScene().name);
            SceneJumper.Goto(GetName(scene));
        }

        public static void GotoSceneImmediate(MainScene scene)
        {
            lastScene.Add(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(GetName(scene));
        }

        public static void AddScene(MainScene scene) => SceneJumper.Add(GetName(scene));

        public static void RemoveScene(MainScene scene) => SceneJumper.Remove(GetName(scene));





        public void GotoLastScene()
        {
            if (SceneManager.sceneCount > 1)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                return;
            }
            else if (lastScene.Count > 0)
            {
                GotoScene(lastScene.Last());
            }
            else GotoScene(MainScene.mainHall);
        }

        public void GotoLastSceneImmediate()
        {
            if (SceneManager.sceneCount > 1)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                return;
            }
            else if (lastScene.Count > 0)
            {
                GotoSceneImmediate(lastScene.Last());
            }
            else GotoScene(MainScene.mainHall);
        }



        public static void GotoLast()
        {
            if (SceneManager.sceneCount > 1)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                return;
            }
            else if (lastScene.Count > 0)
            {
                Goto(lastScene.Last());
            }
            else GotoScene(MainScene.mainHall);
        }

        public static void GotoLastImmediate()
        {
            if (SceneManager.sceneCount > 1)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                return;
            }
            else if (lastScene.Count > 0)
            {
                GotoImmediate(lastScene.Last());
            }
            else GotoScene(MainScene.mainHall);
        }





        /// <summary>
        /// (used in UnityEditor) Goto another scene
        /// </summary>
        /// <param name="ans"></param>
        public static void Goto(string ans)
        {
            lastScene.Add(SceneManager.GetActiveScene().name);
            SceneJumper.Goto(ans);
        }

        /// <summary>
        /// (used in UnityEditor) Goto another scene
        /// </summary>
        /// 
        /// <param name="ans"></param>
        public static void GotoImmediate(string ans)
        {
            lastScene.Add(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(ans);
        }




        /// <summary>
        /// (used in UnityEditor) Goto another scene
        /// </summary>
        /// <param name="ans"></param>
        public void GotoScene(string ans)
        {
            lastScene.Add(SceneManager.GetActiveScene().name);
            SceneJumper.Goto(ans);
        }

        /// <summary>
        /// (used in UnityEditor) Goto another scene
        /// </summary>
        /// 
        /// <param name="ans"></param>
        public void GotoSceneImmediate(string ans)
        {
            lastScene.Add(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(ans);
        }

    }
}