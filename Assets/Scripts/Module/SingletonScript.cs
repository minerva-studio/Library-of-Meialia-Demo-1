using System.Collections;
using UnityEngine;

namespace Minerva.Module
{
    public class SingletonScript<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance { get => instance; set => instance = value; }

        protected virtual void Awake()
        {
            Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}