using Amlos.Core;
using System;
using UnityEngine;

namespace Amlos
{

    [Serializable]
    public class GameSoundData
    {
        [SerializeField] private float bGMValue;
        [SerializeField] private float effectSoundVolume;
        static float BGMValueMultiplier = .4f;
        static float effectSoundValueMultiplier = .3f;

        public float BGMValue { get => bGMValue; }
        public float EffectSoundVolume { get => effectSoundVolume; }
        private GameSound GameSound => Simulation.GetModel<GameSound>();

        public void SetGameBGMSoundVolumn(float value)
        {
            bGMValue = value;
            GameSound.BGMManager.SetGameBGMSoundVolumn(bGMValue * BGMValueMultiplier);
        }


        public void SetGameEffectSoundVolumn(float value)
        {
            effectSoundVolume = value;
            foreach (var item in GameSound.effectSoundController)
            {
                if (item) item.SetVolume(effectSoundVolume * effectSoundValueMultiplier);
            }
        }

        public void SetGameBGMSoundVolumn()
        {
            Simulation.GetModel<GameSound>().BGMManager.SetGameBGMSoundVolumn(bGMValue * BGMValueMultiplier);
        }

        public void SetGameEffectSoundVolumn(SoundEffectController item)
        {
            item.SetVolume(effectSoundVolume * effectSoundValueMultiplier);
        }
    }
}