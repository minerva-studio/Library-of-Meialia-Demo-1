using Minerva.Module;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{
    public class GameMesseger : SingletonScript<GameMesseger>
    {
        public Text text;

        public void SetMessage(string text, float time)
        {
            StartCoroutine(SetMessageInSecond(text, time));
        }

        public void SetMessage(string text)
        {
            this.text.text = text;
        }
        public void ClearMessage()
        {
            this.text.text = "";
        }

        IEnumerator SetMessageInSecond(string text, float time)
        {
            this.text.text = text;
            yield return new WaitForSeconds(time);
            this.text.text = "";
        }
    }
}