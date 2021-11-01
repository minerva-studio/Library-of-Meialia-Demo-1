using Amlos.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Amlos
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class BookSoldierController : EnemyControllerBase
    {
        public float speed;
        public float minTeleportDistance;
        public float maxTeleportDistance;
        public float playerMinimumDistance;
        public float fireballSafeDistance;
        public int floatGroundDistance;

        public Vector2 destination;
        public List<Vector3Int> pathNode;
        public SpriteRenderer bookSoldierRenderer;
        public Collider2D bookSoldierCollider;
        public Rigidbody2D body;
        public SoundEffectController soundEffect;


        public float Speed { get => speed; set => speed = value; }
        private Vector3Int TileCoordinate => Simulation.GetModel<Map>().GetTileCoordinate(transform.position);

        protected override void Awake()
        {
            base.Awake();
            bookSoldierCollider = GetComponent<Collider2D>();
            body = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            Physics2D.IgnoreCollision(bookSoldierCollider, Simulation.GetModel<Map>().mapColliders.platformCollider);
            StartCoroutine(Move());
        }

        void Update()
        {

        }

        void FixedUpdate()
        {
            PerformMovement();
            Vector3 displacement = Simulation.GetModel<Player>().controller.transform.position - transform.position;
            if (displacement.magnitude < playerMinimumDistance && pathNode.Count == 0) MoveNext();
            bookSoldierRenderer.flipX = displacement.x < 0;
        }

        void PerformMovement()
        {
            Vector2 pos = transform.position;
            Vector2 distance = destination - pos;
            var normal = distance.normalized;
            var instantMove = normal * distance.magnitude;
            var expectVelocity = normal * speed;
            //Debug.Log(instantMove.magnitude);
            if (instantMove.magnitude > speed)
            {
                var velocity = expectVelocity;
                body.position = body.position + velocity * Time.deltaTime;

            }
            else if (instantMove.magnitude > speed / 60)
            {
                var velocity = expectVelocity;
                body.position = body.position + velocity * Time.deltaTime;
            }
            else
            {
                body.position = destination;
                if (pathNode.Count > 0)
                {
                    Vector3Int vector3Int = pathNode.FirstOrDefault();
                    destination = new Vector2(vector3Int.x, vector3Int.y);
                    pathNode.RemoveAt(0);
                }
            }
        }

        IEnumerator Move()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                MoveNext();
                yield return new WaitForFixedUpdate();
            }
        }

        void MoveNext()
        {
            var planning = PlanForDestination();
            SetDestination(planning);
        }


        void SetDestination(Vector2 pos)
        {
            var destinationCoordinate = Simulation.GetModel<Map>().GetTileCoordinate(pos);
            pathNode = PathFinder.GetPath(TileCoordinate, destinationCoordinate, (int)maxTeleportDistance * 2);
            Vector3Int first = pathNode.FirstOrDefault();
            destination = new Vector2(first.x, first.y);
        }

        Vector2 PlanForDestination()
        {
            Vector2 pos;
            float x, y, dist;
            bool hasBallWith, hasTile, hasAGround, isCloseToPlayer;
            int count = 0;
            do
            {
                x = Random.Range(transform.position.x - maxTeleportDistance, transform.position.x + maxTeleportDistance);
                y = Random.Range(transform.position.y - maxTeleportDistance, transform.position.y + maxTeleportDistance);
                pos = new Vector2(x, y);
                dist = (new Vector2(transform.position.x, transform.position.y) - pos).magnitude;
                hasBallWith = Simulation.GetModel<Fireballs>().HasBallWithinDistance(pos, fireballSafeDistance);
                hasTile = Simulation.GetModel<Map>().IsCoordinateHasTile(pos);
                hasAGround = Simulation.GetModel<Map>().IsOnGround(pos, floatGroundDistance);
                isCloseToPlayer = (Simulation.GetModel<Player>().controller.transform.position - new Vector3(pos.x, pos.y, 0)).magnitude < playerMinimumDistance;

            }
            while (count < 100 && (hasBallWith || hasTile || !hasAGround || isCloseToPlayer || dist > maxTeleportDistance || dist < minTeleportDistance));
            //Debug.Log(hasTile);
            //Debug.Log(hasBallWith);
            //Debug.Log(dist > maxTeleportDistance);
            //Debug.Log(dist < minTeleportDistance);
            //Debug.Log(isCloseToPlayer);
            return pos;
        }
    }
}