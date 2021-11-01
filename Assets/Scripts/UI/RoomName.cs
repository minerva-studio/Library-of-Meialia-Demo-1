using Amlos.Core;
using Minerva.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{
    public class RoomName : MonoBehaviour
    {
        public Text text;
        public Game game;

        private void Start()
        {
            game = Simulation.GetModel<Game>();
        }

        private void Update()
        {
            if (game != null)
                text.text = ("Amlos.Room." + game.roomID + ".name").Lang();
        }
    }
}