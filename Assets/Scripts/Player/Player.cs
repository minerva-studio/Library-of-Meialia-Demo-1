using System;
using UnityEngine;

namespace Amlos
{
    public class Player
    {
        public PlayerController controller;
        public PlayerAttacker attacker;
        public Health health;


        public void SetPlayerMovementBehaviours(bool value)
        {
            controller.enabled = value;
        }

        public void SetPlayerAttackBehaviours(bool value)
        {
            Debug.Log("Set attack behaviour: " + value);
            attacker.enabled = value;
        }
        public void SetPlayerBehaviours(bool value)
        {
            controller.enabled = value;
            attacker.enabled = value;
        }

        public void ResetHealth()
        {
            health.HP = health.MaxHealth;
        }

        public void SetPosition(Vector3 position)
        {
            Debug.Log("Set position: " + position);
            controller.transform.position = position;
        }
    }
}