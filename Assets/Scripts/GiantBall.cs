using UnityEngine;

namespace Amlos
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class GiantBall : MonoBehaviour
    {
        protected const float minMoveDistance = 0.001f;
        protected const float shellRadius = 0.01f;
        protected Rigidbody2D body;

        public Vector2 velocity;

        public float Angle => GetAngle();
        public float FacingAngle => Angle - (velocity.x <= 0 ? 180 : 0);

        public float speed;

        public void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }


        void FixedUpdate()
        {
            FixSpeed();
            PerformMovement(velocity * Time.deltaTime);
        }


        private void FixSpeed()
        {
            if (velocity.magnitude != speed) velocity = velocity.normalized * speed;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            ChangeDirection(collision);
        }
        public void OnCollisionStay2D(Collision2D collision)
        {
        }


        private void ChangeDirection(Collision2D collision)
        {
            Debug.Log(transform.position + "; " + collision.contacts[0].point);
            Vector2 pos = transform.position;
            var direction = collision.contacts[0].point - pos;
            Debug.DrawRay(transform.position, direction);
            RaycastHit2D[] hits = new RaycastHit2D[8];
            Physics2D.Raycast(transform.position, direction, new ContactFilter2D().NoFilter(), hits);
            var hit = hits[0];
            foreach (var item in hits)
            {
                if (item.collider.gameObject.name == "Ball") continue;
                else hit = item;
                return;
            }
            Debug.Log(velocity);
            velocity = Vector2.Reflect(velocity, hit.normal);
            Debug.Log(velocity);
        }



        void PerformMovement(Vector2 move)
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
    }
}
