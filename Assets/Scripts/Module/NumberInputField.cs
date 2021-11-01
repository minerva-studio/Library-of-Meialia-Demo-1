using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Minerva.Module
{
    [RequireComponent(typeof(InputField))]
    public class NumberInputField : MonoBehaviour
    {
        public int step;
        public int max;
        public bool hasMax;
        public int min;
        public bool hasMin;

        public UnityEvent<int> onInputFieldFinishEdit;

        public InputField InputField => GetComponent<InputField>();

        private void Awake()
        {
        }

        public void Add()
        {
            int newValue = GetValue() + step;
            InputField.text = (newValue > max && hasMax ? max : newValue) + "";
            InputField.onEndEdit?.Invoke(InputField.text);
        }

        public void Minus()
        {
            int newValue = GetValue() - step;
            InputField.text = (newValue < min && hasMin ? min : newValue) + "";
            InputField.onEndEdit?.Invoke(InputField.text);
        }
        public void PentupleAdd()
        {
            int newValue = GetValue() + step * 5;
            InputField.text = (newValue > max && hasMax ? max : newValue) + "";
            InputField.onEndEdit?.Invoke(InputField.text);
        }

        public void PentupleMinus()
        {
            int newValue = GetValue() - step * 5;
            InputField.text = (newValue < min && hasMin ? min : newValue) + "";
            InputField.onEndEdit?.Invoke(InputField.text);
        }

        public void OnValueEditDone(string value)
        {
            int a = int.Parse(InputField.text);
            onInputFieldFinishEdit?.Invoke(a);
        }

        public int GetValue()
        {
            int a;
            if (int.TryParse(InputField.text, out a))
            {
                return a;
            }
            return 0;
        }

        public void SetValue(int v)
        {
            InputField.text = v.ToString();
        }
    }
}