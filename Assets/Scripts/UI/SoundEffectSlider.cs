using Amlos.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{
    public class SoundEffectSlider : MonoBehaviour
    {
        public Slider slider;
        public Text soundInfo;
        public float GameEffectSoundVolumn { set => Simulation.GetModel<GameSoundData>().SetGameEffectSoundVolumn(value); }

        private void Start()
        {
            slider.value = Simulation.GetModel<GameSoundData>().EffectSoundVolume;
            SetInfo(slider.value);
            slider.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>(SetInfo));
        }

        public void SetInfo(float value)
        {
            int txt = (int)(value * 100);
            soundInfo.text = ": " + txt.ToString();
        }
    }
}