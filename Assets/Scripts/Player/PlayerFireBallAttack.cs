using UnityEngine;

namespace Amlos
{
    [RequireComponent(typeof(PlayerFireBallController))]
    public class PlayerFireBallAttack : Attack
    {
        PlayerFireBallController fireBall;

        protected override void Awake()
        {
            base.Awake();
            fireBall = GetComponent<PlayerFireBallController>();
        }

        public override bool Precondition(Health health)
        {
            return fireBall.isMaxLevel;
        }


        public override void DamageTo(Health health)
        {
            base.DamageTo(health);
        }
    }
}
