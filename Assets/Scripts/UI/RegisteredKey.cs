using Amlos.Core;
using UnityEngine;

namespace Amlos
{
    public class RegisteredKey : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Simulation.GetModel<UI>().elements.pausePanel.Pause(!Simulation.GetModel<UI>().elements.pausePanel.isPausing);
            }
        }


    }
}