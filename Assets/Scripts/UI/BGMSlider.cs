using Amlos.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{
    public class BGMSlider : MonoBehaviour
    {
        public Slider slider;
        public Text soundInfo;
        public float GameBGMSoundVolumn { set => Simulation.GetModel<GameSoundData>().SetGameBGMSoundVolumn(value); }

        private void Start()
        {
            slider.value = Simulation.GetModel<GameSoundData>().BGMValue;
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