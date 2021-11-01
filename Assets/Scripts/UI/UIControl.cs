using Amlos.Core;
using Minerva.Module;
using System.Collections;
using UnityEngine;

namespace Amlos
{
    public class UIControl : SingletonScript<UIControl>
    {
        public GameObject startUI;
        public GameObject battleUI;

        protected override void Awake()
        {
            base.Awake();
            OpenGameStartUI();
        }

        // Use this for initialization
        void Start()
        {
            Simulation.GetModel<Player>().SetPlayerMovementBehaviours(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (!Simulation.GetModel<Game>().hasStarted && Input.GetKeyUp(KeyCode.Space))
            {
                OpenEnterMainHall();
                Simulation.GetModel<Player>().controller.SpaceKeyDown();
            }
        }

        public void OpenGameStartUI()
        {
            startUI.SetActive(true);
            battleUI.SetActive(false);
        }

        public void OpenEnterMainHall()
        {
            Simulation.GetModel<Game>().EnterMainHall();
            startUI.SetActive(false);
            battleUI.SetActive(true);
        }
    }
}