using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Motion = Minerva.Module.Motion;

namespace Amlos
{
    /// <summary>
    /// 场景跳转界面核心脚本
    /// </summary>
    public class SceneJumper : MonoBehaviour
    {
        /// <summary> 最少加载场景过场时间 </summary>
        private const float minimunWaitTime = 2f;

        public const string storyDisplayer = "StoryLineLoader";
        public const string main = "Main";

        /// <summary> 异步对象,正在被加载的Scene应该被放在这里 </summary>
        private static AsyncOperation async;
        /// <summary> 进度 </summary>
        private static int progress;
        /// <summary> 加载的时间 </summary>
        private static float time = 0;
        /// <summary> 将要跳转的场景 </summary>
        private static string sceneName;
        /// <summary> 这个MonoBehaviour </summary>
        private static SceneJumper jumper;


        /// <summary> 显示加载进度的信息栏 </summary>
        public Image fadeInImage;
        public Text progressText;
        public Text tip;
        public LegacyProgressBar progressbar;

        /*
         * 部分组件
         * public Image title;
         * public Image background;
         * public Image underbar;
         */

        private void Awake()
        {
            jumper = this;
        }

        private void Start()
        {
            StartCoroutine(Load());//开启一个协程，名字为Load（在这个class内）
            StartCoroutine(FadeIn());//开启一个协程，名字为FadeIn（在这个class内）
            LoadTip();
        }

        // Update is called once per frame
        private void Update()
        {
            time += Time.deltaTime;

            if (async == null)//如果发现没有任何场景需要前往
            {
                SceneManager.UnloadSceneAsync("Loading");//退出Loading
                progressText.text = "100%";
                return;
            }

            //真实的加载速度
            int realProgress = (int)(async.progress / 9 * 1000);
            //假装的加载速度（如果速度太快）
            int pretentingProgress = (int)(time * 100 / minimunWaitTime);

            progress = time / minimunWaitTime * 100 > realProgress ? realProgress : pretentingProgress;

            //使progressBar显示进度
            progressText.text = progress + "%";
            progressbar.SetProgress(progress / 100f);

            //当progress大于99（理论上是100）时跳转场景
            async.allowSceneActivation = progress >= 99;

            if (progress >= 99)
            {
                //                enabled = false;
                SceneManager.UnloadSceneAsync("Loading");//退出Loading
                progressText.text = "100%";
                return;
            }
        }

        private IEnumerator FadeIn()
        {
            for (int i = 0; i < 11; i++)
            {
                fadeInImage.color = new Color(0, 0, 0, 1 - 0.1f * i);
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary> 加载的协程 </summary>
        private IEnumerator Load()
        {
            if (GetComponent<Motion>())
            {
                yield return new WaitForFixedUpdate();
            }
            if (sceneName.Length == 0 && SceneManager.sceneCount == 1)
            {
                sceneName = "Main";
            }
            (async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive)).allowSceneActivation = false;
            yield return null;
            /**
             * Ienumerator是Unity的协程可以运行的方法（只能是迭代器）
             * 迭代器必须有返回，但是如果迭代器真的没有什么可以返回的，可以返回null
             * 迭代器里放置迭代器不可以用普通的方法（直接调用），必须return另外一个迭代器
             * 迭代器的执行方式是unity的引擎把代码一个个扫描过去。
             */
        }

        public void LoadTip()
        {
            int tipCount = 16;
            //tip.text = ("Canute.Tips." + UnityEngine.Random.Range(0, tipCount)).UILang();
        }

        /// <summary>
        /// Load the scene by name
        /// <para>This will leave the current scene</para>
        /// </summary>
        /// <param name="sceneName">name of the scene</param>
        public static void Goto(string sceneName)
        {
            if (jumper)
            {
                jumper.enabled = true;
            }

            Initialize();
            SceneJumper.sceneName = sceneName;
            SceneManager.LoadScene("Loading");//直接加载加载场景
        }

        public static void Add(string name) => SceneManager.LoadScene(name, LoadSceneMode.Additive);

        public static void Remove(string name) => SceneManager.UnloadSceneAsync(name);

        public static void Initialize()
        {
            sceneName = string.Empty;
            async = null;
            progress = 0;
            time = 0;
        }

    }
}
