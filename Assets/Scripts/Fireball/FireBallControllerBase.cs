using Amlos.Core;
using UnityEngine;

namespace Amlos
{

    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class FireBallControllerBase : MonoBehaviour
    {
        [Header("Base Data")]
        [SerializeField] protected float lifetime;
        [SerializeField] protected float maxLifetime;
        [SerializeField] protected float speed;

        public Vector2 velocity;
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D body;
        public Collider2D ballCollider;
        public Attack attacker;
        public SoundEffectController soundEffect;

        public float Angle => GetAngle();
        public float FacingAngle => Angle - (velocity.x <= 0 ? 180 : 0);
        public float Speed { get => speed; set => speed = value; }

        protected virtual void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            ballCollider = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            attacker = GetComponent<Attack>();
            soundEffect = GetComponent<SoundEffectController>();

            Simulation.GetModel<Fireballs>().fireBalls.Add(this);
        }

        protected virtual void FixedUpdate()
        {
            lifetime += Time.deltaTime;
            FixSpeed();
            PerformMovement(velocity * Time.deltaTime);
            UpdateLifeTime();
            UpdateClosePlayerSound();

            if ((transform.position - Simulation.GetModel<Player>().controller.transform.position).magnitude < 3)
            {
                Simulation.GetModel<GameEvent>().TriggerFireballCloseToPlayer(this);
            }
            //Debug.DrawRay(transform.position, velocity, Color.yellow);
        }


        protected virtual void PerformMovement(Vector2 move)
        {
            var distance = move.magnitude;
            body.position = body.position + move.normalized * distance;

        }

        private float GetAngle()
        {
            var angle = velocity.x == 0
                ? (velocity.y > 0 ? 90 : 270)
                : Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;
            return angle;
        }

        protected virtual void FixSpeed()
        {
            if (velocity.magnitude != Speed) velocity = velocity.normalized * Speed;
        }

        protected virtual void UpdateClosePlayerSound() { }

        protected virtual void InitializeBallStatus() { this.lifetime = 0; }

        protected virtual void ReloadBallStatus() { }

        protected virtual void UpdateLifeTime()
        {
            if (maxLifetime < lifetime) { Destroy(gameObject); }
        }

        protected virtual void OnDestroy()
        {
            Simulation.GetModel<Fireballs>().fireBalls.Remove(this);
        }
    }
}