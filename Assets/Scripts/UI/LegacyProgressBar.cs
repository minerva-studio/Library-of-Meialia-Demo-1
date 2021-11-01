using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{
    public class LegacyProgressBar : MonoBehaviour
    {
        public GameObject background;
        public GameObject processing;

        public bool towardRight;
        public float progress;
        public Vector3 initialPosition;



        public Image bg => background.GetComponent<Image>();
        public Image progressImage => processing.GetComponent<Image>();


        private float Distance => -initialPosition.x;

        public float Progress { get => progress; set => progress = value > 0 ? value : 0; }

        public void Awake()
        {
            initialPosition = processing.transform.localPosition;
        }

        public virtual void Start()
        {

        }

        public void SetFull()
        {
            SetProgress(1f);
        }

        /// <summary> Set the progress of bar </summary>
        /// <param name="f"> progress [0,1] </param>
        public void SetProgress(float f)
        {
            //Debug.Log(f);
            var a = initialPosition;
            if (!towardRight) a.x += f > 1 ? Distance : Distance * f;
            else a.x = -a.x - (f > 1 ? Distance : Distance * f);
            processing.transform.localPosition = a;
        }
        /// <summary> Set the progress of bar </summary>
        /// <param name="f"> progress [0,1] </param>
        public void SetProgress(float p, float total)
        {
            var f = p / total; SetProgress(f);
        }

    }

}