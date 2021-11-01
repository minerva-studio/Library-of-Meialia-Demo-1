using UnityEngine;

namespace Amlos
{
    public class BookSoldierFireballController : FireBallControllerBase
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health) { attacker.DamageTo(health); }
            Destroy(gameObject);
        }
    }
}