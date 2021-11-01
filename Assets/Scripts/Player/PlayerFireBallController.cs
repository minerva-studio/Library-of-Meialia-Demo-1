using Amlos.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Amlos
{


    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class PlayerFireBallController : FireBallControllerBase
    {
        [Header("Player Fireball Infomation")]
        [SerializeField] protected int level;
        [SerializeField] protected int bounceTime;
        [SerializeField] protected int period;
        [SerializeField] protected int collisionStayTime;
        public Animator animator;
        public Light2D light2D;
        public FireballTail fireballTail;

        [Header("Data")]
        public int maxLevel;
        public int maxBounceTime;
        public int maxPeriod;
        public float splitAngle;
        public float[] radiusSize;
        public float[] lightRadius;
        public Sprite[] fireBallSprites;
        public FireballAudioData audioData;

        [Header("For test")]
        //public bool doNotDecayMaxLevel;
        public bool doNotSplit;
        public bool doNotDestroy;

        public bool isMaxLevel => Level >= maxLevel - 1;
        public bool isMaxBounce => bounceTime >= maxBounceTime - 1;
        public int Level { get => level; set => level = value; }


        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            Simulation.GetModel<PlayerFireballs>().fireBalls.Add(this);
        }

        private void Start()
        {
            //audioData.deflect = deflectSound;
            //audioData.moving = movingSound;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Simulation.GetModel<PlayerFireballs>().fireBalls.Remove(this);
        }

        protected override void FixedUpdate()
        {
            lifetime += Time.deltaTime;
            FixSpeed();
            PerformMovement(velocity * Time.deltaTime);
            UpdateLifeTime();
            UpdateClosePlayerSound();
            UpdateFireballTail();
            spriteRenderer.flipX = body.velocity.x <= 0;
            //Debug.DrawRay(transform.position, velocity, Color.yellow);
        }

        protected virtual void UpdateFireballTail()
        {
            fireballTail.transform.position = transform.position;
            var z = FacingAngle - 180;
            fireballTail.transform.eulerAngles = new Vector3(0, 0, z);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log("ball call");
            ReloadBallStatus();
            if (collision.gameObject.name == "Ball") return;
            if (collision.gameObject.name == "Amlos") Debug.Log("hit player!");
            if (collision.collider.GetComponent<Health>()) attacker.DamageTo(collision.collider.GetComponent<Health>());
            if (isMaxLevel && isMaxBounce) StartCoroutine(SplitWait());
            if (isMaxLevel)
            {
                //Debug.Log(bounceTime);
                //Debug.Log(level);
                //Debug.Log(period);
            }
            ChangeDirection(collision);
            UpdateBounceTime();
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            //Debug.Log("ball Stayed?");
            collisionStayTime++;
            if (collisionStayTime > 5) Destroy(gameObject);
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            collisionStayTime = 0;
        }

        private void ChangeDirection(Collision2D collision)
        {
            Vector2 pos = transform.position;
            //Debug.Log(collision.contacts.Length);
            if (collision != null && collision.contactCount > 0)
            {
                var direction = collision.contacts[0].point - pos;
                Debug.DrawRay(transform.position, velocity.normalized * 5);
                var hit = Physics2D.Raycast(transform.position, direction);
                velocity = Vector2.Reflect(velocity, hit.normal);
                body.velocity = Vector2.Reflect(body.velocity, hit.normal);
                soundEffect.Play(audioData.deflect);
            }
        }

        private void UpdatePeriod()
        {
            this.period++;
            if (this.period == maxPeriod) { Destroy(gameObject); }
        }

        private void UpdateLevel()
        {
            //Debug.Log("update info");
            if (isMaxLevel)
            {
                level = 0;
                InitializeBallStatus();
                UpdatePeriod();
            }
            else Level++;
            ReloadBallStatus();
        }

        private void UpdateBounceTime()
        {
            bounceTime++;
            if (isMaxBounce)
            {
                bounceTime = 0;
                UpdateLevel();
            }
        }

        private IEnumerator SplitWait()
        {
            //Debug.Log("split");
            for (int i = 0; i < 3; i++) yield return new WaitForFixedUpdate();
            InitializeBallStatus();
            soundEffect.Play(audioData.explode);
            Split();
        }

        private void Split()
        {
            if (!doNotSplit)
            {
                Debug.Log("spliting");
                Debug.DrawRay(transform.position, velocity.normalized * 25, Color.white, 1);
                float newAngle;
                newAngle = FacingAngle + splitAngle;
                Vector2 upper = velocity.magnitude * new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
                CreateNewBall(upper);
                newAngle = FacingAngle - splitAngle;
                Vector2 lower = velocity.magnitude * new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
                CreateNewBall(lower);
                Destroy(gameObject);
            }
        }

        protected override void UpdateClosePlayerSound()
        {
            float distance = (Simulation.GetModel<Player>().controller.transform.position - transform.position).magnitude;
            if (distance < 10 && isMaxLevel) soundEffect.PlayNonOverride(audioData.moving);
        }

        public void SetBallLevel(int level)
        {
            this.Level = level > maxLevel ? maxLevel : (level < 0 ? 0 : level);
            ReloadBallStatus();
        }

        public void SetBallLevel(int level, int maxLevel)
        {
            this.Level = level > maxLevel ? maxLevel : (level < 0 ? 0 : level);
            this.maxLevel = maxLevel;
            ReloadBallStatus();
        }

        public void CreateNewBall(Vector2 velocity)
        {
            //Debug.Log("Create new"); 

            var newBall = Instantiate(gameObject, transform.parent);
            newBall.name = "Ball";
            newBall.transform.position = transform.position;
            newBall.GetComponent<PlayerFireBallController>().velocity = velocity;
            newBall.GetComponent<PlayerFireBallController>().PerformMovement(newBall.GetComponent<PlayerFireBallController>().velocity * Time.deltaTime);
            newBall.GetComponent<PlayerFireBallController>().ReloadBallStatus();
        }

        protected override void ReloadBallStatus()
        {
            animator.enabled = isMaxLevel;
            animator.Play("Ball Moving");
            fireballTail.SetTailActive(isMaxLevel);
            if (radiusSize.Length > Level) (ballCollider as CircleCollider2D).radius = radiusSize[Level];
            if (fireBallSprites.Length > Level) spriteRenderer.sprite = fireBallSprites[Level];
            if (lightRadius.Length > Level) light2D.pointLightOuterRadius = lightRadius[Level];
            light2D.color = isMaxLevel ? new Color(1, 100 / 255f, 50 / 255f) : new Color(1, 120 / 255f, 50 / 255f);
        }
    }
}
