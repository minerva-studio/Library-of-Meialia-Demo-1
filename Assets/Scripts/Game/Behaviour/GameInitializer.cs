using Amlos.Core;
using Minerva.Localization;
using Minerva.Module;
using UnityEngine;

namespace Amlos.Behaviour
{
    public class GameInitializer : SingletonScript<GameInitializer>
    {
        public GameData data;

        protected override void Awake()
        {
            base.Awake();
            GameData.instance = data;
        }
    }
}