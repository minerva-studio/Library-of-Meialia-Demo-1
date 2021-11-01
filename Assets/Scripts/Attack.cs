using UnityEngine;

namespace Amlos
{
    [RequireComponent(typeof(Collider2D))]
    public class Attack : MonoBehaviour
    {
        public new Collider2D collider;
        public int damage;

        protected virtual void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        public virtual void SetDamage(int damage)
        {
            this.damage = damage;
            if (this.damage < 0) DeleteObject();
        }

        public virtual bool Precondition(Health health)
        {
            return true;
        }


        public virtual void DeleteObject()
        {
            Destroy(gameObject);
        }

        public virtual void DamageTo(Health health)
        {
            if (health && Precondition(health))
            {
                health.HP -= damage;
                health.CheckHPStatus();
                DeleteObject();
            }
        }
    }
}