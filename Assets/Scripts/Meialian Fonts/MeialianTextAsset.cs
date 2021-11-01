using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeialianFonts
{
    [CreateAssetMenu]
    public class MeialianTextAsset : ScriptableObject
    {
        public List<TextPair> textPairs = new List<TextPair>();
        public Sprite Get(string key)
        {
            return textPairs.Where(p => p.text == key).FirstOrDefault()?.sprite;
        }

        public bool isInAlphabet(string item)
        {
            return textPairs.FirstOrDefault(p => p.text == item) != null || textPairs.FirstOrDefault(p => p.text == item.ToUpper()) != null;
        }
    }
}
