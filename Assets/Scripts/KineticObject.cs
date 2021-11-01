using UnityEngine;

namespace Amlos
{
    /// <summary>
    /// Implements game physics for some in game entity.
    /// </summary>
    public class KineticObject : MonoBehaviour
    {
        protected Rigidbody2D body;
        public Vector2 force;
        protected const float minMoveDistance = 0.001f;
        protected const float shellRadius = 0.01f;

        public bool IsGrounded { get => IsOnGround(); }

        protected virtual void OnEnable()
        {
            body = GetComponent<Rigidbody2D>();
        }

        protected virtual void Update()
        {

        }


        public void AddForce(Vector2 force)
        {
            this.force += force;
        }


        public void FixedUpdate()
        {
            body.AddForce(force);
        }


        public void Teleport(Vector3 position)
        {
            body.position = position;
            force *= 0;
            body.velocity *= 0;
        }

        public bool IsOnGround()
        {
            RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
            var count = body.Cast(Vector2.down, hitBuffer);
            for (var i = 0; i < count; i++)
            {
                var currentNormal = hitBuffer[i].normal;
                //is this surface flat enough to land on?
                if (currentNormal.y > .5f)
                {
                    return true;
                }
            }
            return false;
        }

    }
}