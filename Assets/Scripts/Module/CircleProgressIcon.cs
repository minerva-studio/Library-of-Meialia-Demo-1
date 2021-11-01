using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Minerva.Module
{
    public class CircleProgressIcon : MonoBehaviour
    {
        public float progress;
        public Image shade;
        public Image background;
        public Image icon;
        public Text info;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"> the value, between 0, 1</param>
        public void SetProgress(float value)
        {
            shade.fillAmount = 1 - value;
        }
    }
}