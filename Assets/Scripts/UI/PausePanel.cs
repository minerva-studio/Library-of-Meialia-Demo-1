using Amlos.Core;
using Minerva.Module;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{
    public class PausePanel : SingletonScript<PausePanel>
    {
        public bool isPausing = false;
        public Button backToMainHall;

        private void OnEnable()
        {
            backToMainHall.interactable = Simulation.GetModel<Game>().roomID != RoomID.mainHall;
        }

        public void Pause(bool pause)
        {
            isPausing = pause;
            if (isPausing)
            {
                Time.timeScale = 0;
                if (!gameObject.activeSelf) gameObject.SetActive(true);
            }
            else
            {
                if (gameObject.activeSelf) gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }



        public void TryQuit()
        {
            var window = ConfirmWindow.Create(Quit, "Quit will back to the main hall, and lost all current progress, Are you sure?");
            window.transform.SetParent(GetComponent<Image>().canvas.transform);
            window.SetPosition();

        }



        public void Quit()
        {
            Simulation.GetModel<Game>().BackToMainHall();
            Pause(false);
        }


    }
}