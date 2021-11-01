using Amlos.Core;
using Minerva.Module;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Amlos
{
    [Serializable]
    public class Gate
    {
        public int id;
        public string destinationRoom;
    }


    public class GateBehaviour : MonoBehaviour
    {
        public static List<GateBehaviour> gates = new List<GateBehaviour>();

        public enum GateState
        {
            open,
            close,
        }

        public int id;
        public float enterRoomTimeRequire = 5;
        public float enterRoomCountDown;
        public bool setGateConstant;
        public bool disable;
        public GateState gateState;
        public SpriteRenderer gateRenderer;
        public Sprite open;
        public Sprite close;


        private bool ReadyEnterNextLevel => gateState == GateState.open && !disable && GateInfo != null;
        public bool isEnemyCleared => Simulation.GetModel<Game>().isEnemyCleared;
        public Gate GateInfo => Simulation.GetModel<Game>().currentLevelData.gatesInfo.Find(g => g.id == id);


        private void Awake()
        {
            gates.Add(this);
        }

        private void Start()
        {
            gateRenderer = GetComponent<SpriteRenderer>();
        }


        private void Update()
        {
            if (gateState == GateState.close) enterRoomCountDown = 0;
            if (ReadyEnterNextLevel)
            {
                enterRoomCountDown += Time.deltaTime;
                GameMesseger.Instance.SetMessage("Enter Next Level in " + (int)(enterRoomTimeRequire - enterRoomCountDown));
                if (enterRoomCountDown > enterRoomTimeRequire)
                {
                    GameMesseger.Instance.ClearMessage();
                    Simulation.GetModel<Game>().EnterLevel(GateInfo.destinationRoom);
                }
            }
        }


        private void OnValidate()
        {
            gateRenderer = GetComponent<SpriteRenderer>();
            SetState(gateState);
        }


        public void OnTriggerEnter2D(Collider2D collision)
        {
            string name = collision.gameObject.name;
            if (name == "Amlos" && isEnemyCleared)
            {
                //Debug.Log("Hit!");
                SetState(GateState.open);
            }
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            string name = collision.gameObject.name;
            if (name == "Amlos" && isEnemyCleared)
            {
                //Debug.Log("Hit!");
                SetState(GateState.open);
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            string name = collision.gameObject.name;
            if (name == "Amlos")
            {
                //Debug.Log("Hit!");
                SetState(GateState.close);
                GameMesseger.Instance.ClearMessage();
            }
        }

        private void OnDestroy()
        {
            gates.Remove(this);
        }

        public void SetState(GateState gateState)
        {
            if (setGateConstant) return;

            this.gateState = gateState;
            gateRenderer.sprite = GetTile();
        }

        private Sprite GetTile()
        {
            switch (gateState)
            {
                case GateState.open:
                    return open;
                case GateState.close:
                    return close;
                default:
                    return default;
            }
        }



        public static GateBehaviour FindGate(int id)
        {
            foreach (var item in gates.ShallowClone())
            {
                if (!item) gates.Remove(item);
            }

            return gates.Find(g => g.id == id);
        }
    }
}