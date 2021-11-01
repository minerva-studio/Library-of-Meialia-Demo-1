using Amlos.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{
    public class PlayerHPBar : MonoBehaviour
    {
        public LegacyProgressBar progressBar;
        public Text healthInfo;

        public Health health => Simulation.GetModel<Player>().health;

        public void Update()
        {
            if (health)
            {
                progressBar.SetProgress(health.HP, health.MaxHealth);
                healthInfo.text = health.HP + "/" + health.MaxHealth;
            }
        }
    }
}