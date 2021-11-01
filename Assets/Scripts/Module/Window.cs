using System.Collections.Generic;
using UnityEngine;

namespace Minerva.Module
{
    /// <summary>
    /// Basic Display Window Script
    /// <para> Reminder: Most Window are One-Instance Only</para>
    /// </summary>
    public class Window : MonoBehaviour
    {
        public static List<Window> windows = new List<Window>();

        [SerializeField] private bool isForceOpened;
        [SerializeField] private Window outerWindow;
        [SerializeField] private Window innerWindow;

        public bool IsTopMenu => InnerWindow == null;
        public bool IsForceOpened { get => isForceOpened; set => isForceOpened = value; }
        public Window InnerWindow { get => innerWindow; set => innerWindow = value; }
        public Window OuterWindow { get => outerWindow; set => outerWindow = value; }


        protected virtual void Awake()
        {
            windows.Add(this);
        }
        // Use this for initialization
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {
            ESCClose();
        }

        protected virtual void OnDestroy()
        {
            windows.Remove(this);
        }

        public virtual void ESCClose()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && IsTopMenu && !IsForceOpened)
            {
                Close();
            }
        }

        public virtual void Close()
        {
            OuterWindow.Exist()?.InnerWindowClosed();
            gameObject.SetActive(false);
        }
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void OpenAsInnerWindow(Window window)
        {
            InnerWindow = window;
            window.OuterWindow = this;
        }

        public virtual void OnDisable()
        {
            windows.Remove(this);
        }

        public virtual void OnEnable()
        {
            windows.Add(this);
        }

        public void InnerWindowClosed()
        {

            InnerWindow?.OutterWindowLeave();
            InnerWindow = null;
        }

        public void OutterWindowLeave()
        {
            OuterWindow = null;

        }
    }
}