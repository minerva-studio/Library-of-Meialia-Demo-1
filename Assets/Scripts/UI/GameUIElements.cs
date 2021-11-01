using Amlos.Core;
using Minerva.Module;
using UnityEngine;

namespace Amlos
{

    public class GameUIElements : MonoBehaviour
    {
        public CircleProgressIcon fireWaitProgress;
        public PausePanel pausePanel;

        private void Awake()
        {
            Simulation.GetModel<UI>().elements = this;
        }

        private void Update()
        {
            fireWaitProgress.SetProgress(Simulation.GetModel<Player>().attacker.FireWaitTimePercentage);
            //fireWaitProgress.info.text = Instance<PlayerAttack>.instance.time.ToString( + "s";
        }

    }
}