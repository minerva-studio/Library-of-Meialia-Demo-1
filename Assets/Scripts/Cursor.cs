using Amlos.Core;
using UnityEngine;

namespace Amlos
{
    public class Cursor : MonoBehaviour
    {
        public Texture2D cursorIcon;
        public Texture2D onAttackCursor;

        public bool isInNormal = true;

        public void Awake()
        {
            Simulation.GetModel<UI>().cursor = this;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.SetCursor(cursorIcon, new Vector2(.5f, .5f), CursorMode.Auto);
        }

        public void Start()
        {
            Simulation.GetModel<Player>().attacker.allowAttackEvent.AddListener(new UnityEngine.Events.UnityAction(SetAttackingCursor));
            Simulation.GetModel<Player>().attacker.endAttackEvent.AddListener(new UnityEngine.Events.UnityAction(SetNormalCursor));
        }

        public void SwitchCursor()
        {
            if (isInNormal) UnityEngine.Cursor.SetCursor(onAttackCursor, new Vector2(.5f, .5f), CursorMode.Auto);
            else UnityEngine.Cursor.SetCursor(cursorIcon, new Vector2(.5f, .5f), CursorMode.Auto);
        }

        public void SetAttackingCursor() { UnityEngine.Cursor.SetCursor(onAttackCursor, new Vector2(.5f, .5f), CursorMode.Auto); }
        public void SetNormalCursor() { UnityEngine.Cursor.SetCursor(cursorIcon, new Vector2(.5f, .5f), CursorMode.Auto); }
    }
}