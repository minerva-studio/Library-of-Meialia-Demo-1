using Minerva.Module;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEngine;

namespace MeialianFonts
{
    public class MeialianText : MonoBehaviour
    {
        public List<MeialianTextBlock> blocks;
        public GameObject meialianBlockPrefab;
        public string text;

        public void Start()
        {
            LoadText(text);
        }

        public void OnValidate()
        {
            LoadText(text);
        }

        public void LoadText(string text)
        {
            this.text = text;
            var split = text.SplitToMeialianFont();
            int currentTextCount = blocks.Count;
            int expectCount = split.Length;

            if (expectCount < currentTextCount)
            {
                ClearText(expectCount);
            }
            else
            {
                for (int i = currentTextCount; i < expectCount; i++)
                {
                    var block = Instantiate(meialianBlockPrefab, transform).GetComponent<MeialianTextBlock>();
                    blocks.Add(block);
                }
            }


            for (int i = 0; i < blocks.Count; i++)
            {
                MeialianTextBlock item = blocks[i];
                item.text = split[i];
                item.ReRender();
            }
        }

        public void ClearText()
        {
            Debug.Log(this.blocks.Count);
            var blocks = this.blocks.ShallowClone();
            this.blocks.Clear();
            foreach (var item in blocks)
            {
                Debug.Log(item);
                EditorCoroutineUtility.StartCoroutine(Destroy(item.gameObject), this);
            }
        }


        public void ClearText(int startIndex)
        {
            Debug.Log(this.blocks.Count);

            for (int i = blocks.Count - 1; i >= startIndex; i--)
            {
                MeialianTextBlock item = blocks[i];
                //Debug.Log(item);
#if UNITY_EDITOR
                EditorCoroutineUtility.StartCoroutine(Destroy(item.gameObject), this);
#else
                Destroy(item.gameObject);
#endif
                blocks.RemoveAt(i);
            }
        }


        IEnumerator Destroy(GameObject go)
        {
            yield return new WaitForEndOfFrame();
            DestroyImmediate(go);
        }
    }
}
