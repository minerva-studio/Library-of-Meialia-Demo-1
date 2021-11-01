using UnityEngine;
using UnityEngine.UI;

namespace Minerva.Module
{

    public class ProgressBar : MonoBehaviour
    {
        public float progress;
        public Slider slider;
        public GameObject background;
        public GameObject processing;

        public Image bg => background.GetComponent<Image>();
        public Image progressImage => processing.GetComponent<Image>();


        public float Progress { get => progress; set => progress = value > 0 ? value : 0; }



        public void SetFull()
        {
            SetProgress(1);
        }


        /// <summary> Set the progress of bar </summary>
        /// <param name="f"> progress </param>
        public void SetProgress(float f)
        {
            slider.value = f;
        }

    }

}