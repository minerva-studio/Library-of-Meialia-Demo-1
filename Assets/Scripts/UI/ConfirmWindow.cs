using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Amlos
{
    public class ConfirmWindow : MonoBehaviour
    {
        public static ConfirmWindow instance;
        public static Action action;

        public Text info;

        // Use this for initialization
        void Start()
        {
            var pos = transform.position;
            pos.z = 1;
            transform.position = pos;
            GetComponent<Canvas>().overrideSorting = true;
            GetComponent<Canvas>().sortingLayerName = "UI";
            GetComponent<Canvas>().sortingOrder = 10000;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Confirm()
        {
            action?.Invoke();
            Close();
        }

        public void Close()
        {
            Destroy(gameObject);
            instance = null;
        }

        public void SetPosition()
        {
            transform.localPosition = new Vector3(0, 0, 1);
            transform.localScale = Vector3.one;
        }


        public static ConfirmWindow Create(Action action, string info)
        {
            ConfirmWindow.action = action;
            if (instance)
            {
                return instance;
            }

            instance = Instantiate(GameData.instance.confirmWindow).GetComponent<ConfirmWindow>();


            instance.info.text = info;
            instance.SetPosition();

            return instance;
        }
    }
}