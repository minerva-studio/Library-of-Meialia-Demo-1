using Amlos.Core;
using Minerva.Module;
using System.Collections;
using UnityEngine;


namespace Amlos.Behaviour
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary> 
    public class GameController : SingletonScript<GameController>
    {

        public Game game;
        public GameData data;

        protected override void Awake()
        {
            base.Awake();
            GameData.instance = data;
            game = Simulation.GetModel<Game>();
            game.LoadConfiguration();
            StartCoroutine(AutoSave());
        }

        void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

        }

        void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            if (Instance == this) Simulation.Tick();
        }



        IEnumerator AutoSave()
        {
            while (true)
            {
                ConfigurationLoader.Save(game.configuration);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}


