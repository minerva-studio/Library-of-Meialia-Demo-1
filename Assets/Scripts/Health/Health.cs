using Amlos.Core;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Amlos
{
    [RequireComponent(typeof(Collider2D))]
    public class Health : MonoBehaviour
    {
        public bool immortal;
        [SerializeField] private int hp;
        [SerializeField] private int maxHealth;
        public new Collider2D collider;
        public UnityEvent DieEvent;

        public int HP { get => hp; set => SetHealth(value); }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
            hp = maxHealth;
        }

        public void SetHealth(int hp)
        {
            var before = this.hp;
            this.hp = hp;
            if (before > hp) Simulation.GetModel<GameEvent>().TriggerPlayerHurtEvent(this);
            if (this.hp <= 0) Die();
        }

        public void CheckHPStatus()
        {
            if (this.hp <= 0) Die();
        }

        public virtual void Die()
        {
            DieEvent?.Invoke();
        }
    }
}