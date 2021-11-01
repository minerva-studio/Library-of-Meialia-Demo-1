using UnityEngine;
using UnityEngine.UI;

namespace MeialianFonts
{

    public class MeialianTextBlock : MonoBehaviour
    {
        public string text;
        public MeialianTextAsset meialianTextAsset;
        public Image normalPos;
        public Image lowerPos;
        public Image duplicatePos;

        public Text replacement;

        private void Start()
        {
            ReRender();
        }

        public void ReRender()
        {
            duplicatePos.enabled = false;
            lowerPos.enabled = false;
            normalPos.enabled = true;
            replacement.text = "";
            Render();
        }

        public void Render()
        {
            duplicatePos.enabled = false;
            lowerPos.enabled = false;
            if (text.Length == 0) normalPos.enabled = false;
            else if (string.IsNullOrWhiteSpace(text)) normalPos.enabled = false;
            else if (!meialianTextAsset.isInAlphabet(text[0].ToString()))
            {
                normalPos.enabled = false;
                replacement.text = text;
                if (text.Length > 1)
                    if (text[0] == text[1])
                    {
                        duplicatePos.enabled = true;
                    }
            }
            //one letter only
            else if (text.Length == 1)
            {
                if (text[0].isLowerlVowel()) normalPos.sprite = meialianTextAsset.Get(text[0].ToString());
                else normalPos.sprite = meialianTextAsset.Get(text[0].ToString().ToUpper());
            }
            //vowel start combo
            else if (text[0].isLowerlVowel())
            {
                if (text[1].isLowerlVowel())
                {
                    normalPos.sprite = meialianTextAsset.Get(text[0].ToString());
                    lowerPos.sprite = meialianTextAsset.Get(text[1] + "2");
                    lowerPos.enabled = true;
                }
                else
                {
                    normalPos.sprite = meialianTextAsset.Get(text[0].ToString());
                    lowerPos.sprite = meialianTextAsset.Get("_" + text.Remove(0, 1));
                    lowerPos.enabled = true;
                }
            }
            //duplicate
            else if (text[0] == text[1])
            {
                normalPos.sprite = meialianTextAsset.Get(text[0].ToString().ToUpper());
                duplicatePos.enabled = true;
            }
            else
            {
                Sprite sprite = meialianTextAsset.Get(text.ToString().ToUpper());
                if (sprite) normalPos.sprite = sprite;
                else
                {
                    normalPos.enabled = false;
                    replacement.text = text;
                }
            }
        }
    }
}
