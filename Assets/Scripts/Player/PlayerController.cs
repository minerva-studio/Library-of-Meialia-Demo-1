using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Amlos.Core;

namespace Amlos
{
    public class PlayerController : MonoBehaviour
    {

        //public AudioClip jumpAudio;
        //public AudioClip respawnAudio;
        //public AudioClip ouchAudio;
        public Animator animator;
        public Rigidbody2D body;
        public SpriteRenderer spriteRenderer;
        public new BoxCollider2D collider;

        public float moveSpeed;
        public float flySpeed;
        //public Vector2 moveForce;
        public Vector2 jumpForce;
        public Vector2 maxVelocityMoving;
        public Vector2 maxVelocityFlying;
        public float gravityScale;

        public MoveState moveState;
        public bool isOnGround;

        public bool IsJumping;

        public bool IsVerticallyStanding => Mathf.Abs(body.velocity.y) < 0.1f;
        public bool IsFalling => body.velocity.y < -0.4;
        public bool IsFlying => moveState == MoveState.Flying || moveState == MoveState.FlyingIdle;
        public bool IsBackward => body.velocity.x * (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).x < 0;

        public float Speed => body.velocity.magnitude;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            body = GetComponent<Rigidbody2D>();
            collider = GetComponent<BoxCollider2D>();
            Simulation.GetModel<Player>().controller = this;
            Simulation.GetModel<Player>().health = GetComponent<Health>();
            this.enabled = false;
        }

        private void OnEnable()
        {
            StartCoroutine(UpdatePlatformCollision());
        }


        public void Update()
        {
            KeyCheck();
        }

        public void FixedUpdate()
        {
            UpdateState();
            UpdateSpeed();
            SwitchingDirection();
        }

        /// <summary>
        /// updating platform
        /// </summary>
        private IEnumerator UpdatePlatformCollision()
        {
            while (enabled)
            {
                if (IsFlying) Physics2D.IgnoreCollision(collider, Simulation.GetModel<Map>().mapColliders.platformCollider, true);
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    //Debug.LogError("shift down");
                    Physics2D.IgnoreCollision(collider, Simulation.GetModel<Map>().mapColliders.platformCollider, true);
                    yield return new WaitForSecondsRealtime(.3f);
                }
                else
                {
                    //Debug.LogError("shift up");
                    Physics2D.IgnoreCollision(collider, Simulation.GetModel<Map>().mapColliders.platformCollider, false);
                }

                yield return new WaitForFixedUpdate();
            }
        }

        /// <summary>
        /// fixing the speed
        /// </summary>
        private void UpdateSpeed()
        {
            if (IsFlying)
            {
                Vector2 maxFlying = maxVelocityFlying * (IsBackward ? .5f : 1);
                if (body.velocity.x > maxFlying.x) { body.velocity = new Vector2(maxFlying.x, body.velocity.y); }
                else if (body.velocity.x < -maxFlying.x) { body.velocity = new Vector2(-maxFlying.x, body.velocity.y); }
                if (body.velocity.y > maxFlying.y) { body.velocity = new Vector2(body.velocity.x, maxFlying.y); }
                else if (body.velocity.y < -maxFlying.y) { body.velocity = new Vector2(body.velocity.x, -maxFlying.y); }
            }
            else
            {
                Vector2 maxMoving = maxVelocityMoving * (IsBackward ? .5f : 1);
                if (body.velocity.x > maxMoving.x) { body.velocity = new Vector2(maxMoving.x, body.velocity.y); }
                else if (body.velocity.x < -maxMoving.x) { body.velocity = new Vector2(-maxMoving.x, body.velocity.y); }
                if (body.velocity.y > maxMoving.y) { body.velocity = new Vector2(body.velocity.x, maxMoving.y); }
            }
        }

        /// <summary>
        /// change the direction
        /// </summary>
        private void SwitchingDirection()
        {
            Vector3 normalized = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            if (normalized.x > 0) transform.localScale = Vector3.one;
            else transform.localScale = new Vector3(-1, 1, 1);
        }

        /// <summary>
        /// update the state of the player
        /// </summary>
        private void UpdateState()
        {
            switch (moveState)
            {
                case MoveState.Moving:
                    if (!Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D)) { SetMoveState(MoveState.Idle); }
                    if (IsFalling) SetMoveState(MoveState.Falling);
                    break;
                case MoveState.Idle:
                    if (IsFalling) SetMoveState(MoveState.Falling);
                    break;
                case MoveState.PrepareToJump:
                    SetMoveState(MoveState.Jumping);
                    break;
                case MoveState.Jumping:
                    if (IsVerticallyStanding) { SetMoveState(MoveState.Falling); }
                    break;
                case MoveState.Falling:
                    if (IsVerticallyStanding) { SetMoveState(MoveState.Idle); }
                    break;
                case MoveState.PrepareToFly:
                    SetMoveState(MoveState.Flying);
                    break;
                case MoveState.Flying:
                    if (isOnGround) { SetMoveState(MoveState.Idle); }
                    else if (Speed < .5) { SetMoveState(MoveState.FlyingIdle); }
                    break;
                case MoveState.FlyingIdle:
                    if (isOnGround) { SetMoveState(MoveState.Idle); }
                    else if (Speed >= 0.5) { SetMoveState(MoveState.Flying); }
                    break;
            }
        }

        /// <summary>
        /// checking keys
        /// </summary>
        private void KeyCheck()
        {
            if (!IsFlying)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    //Debug.Log("left");
                    //body.AddForce(-moveForce * (IsBackward ? .5f : 1));
                    body.velocity = new Vector2(-moveSpeed * (IsBackward ? .5f : 1), body.velocity.y);
                    if (moveState == MoveState.Idle) SetMoveState(MoveState.Moving);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    //Debug.Log("right");
                    //body.AddForce(moveForce * (IsBackward ? .5f : 1));
                    body.velocity = new Vector2(moveSpeed * (IsBackward ? .5f : 1), body.velocity.y);
                    if (moveState == MoveState.Idle) SetMoveState(MoveState.Moving);
                }
            }

            if (IsFlying)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    //body.AddForce(-moveForce * (IsBackward ? .5f : 1));
                    body.velocity = new Vector2(-moveSpeed * (IsBackward ? .5f : 1), body.velocity.y);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    //body.AddForce(moveForce * (IsBackward ? .5f : 1));
                    body.velocity = new Vector2(moveSpeed * (IsBackward ? .5f : 1), body.velocity.y);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    //  Debug.Log("up");

                    //if (body.velocity.y <= -maxVelocityMoving.y) { body.velocity = new Vector2(body.velocity.x, -maxVelocityFlying.y); }
                    //body.AddForce(-new Vector2(moveForce.y, moveForce.x));
                    body.velocity = new Vector2(body.velocity.x, -flySpeed * (IsBackward ? .5f : 1));
                }
                if (Input.GetKey(KeyCode.W))
                {
                    // Debug.Log("down");
                    //if (body.velocity.y >= maxVelocityMoving.y) { body.velocity = new Vector2(body.velocity.x, maxVelocityFlying.y); }
                    //body.AddForce(new Vector2(moveForce.y, moveForce.x)); 
                    body.velocity = new Vector2(body.velocity.x, flySpeed * (IsBackward ? .5f : 1));
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpaceKeyDown();
            }
        }

        public void SpaceKeyDown()
        {
            //Debug.Log("get space key");
            if (!IsVerticallyStanding)
            {
                if (moveState == MoveState.Falling || moveState == MoveState.Jumping)
                {
                    //Debug.Log("fly");
                    body.gravityScale = 0;
                    SetMoveState(MoveState.PrepareToFly);
                }
                else if (moveState == MoveState.Flying)
                {
                    //Debug.Log("fall");  
                    Physics2D.IgnoreCollision(collider, Simulation.GetModel<Map>().mapColliders.platformCollider, false);
                    body.gravityScale = gravityScale;
                    SetMoveState(MoveState.Idle);
                }
            }

            else
            {
                //Debug.Log("jump");
                body.AddForce(jumpForce);
                body.gravityScale = gravityScale;
                SetMoveState(MoveState.PrepareToJump);
            }
        }




        private void SetAnimation(string animationName, bool value)
        {
            animator.SetBool("idle", false);
            animator.SetBool(animationName, value);
        }

        private void SetAnimaitonIdle()
        {
            animator.SetBool("idle", true);
            SetAnimationClear();
        }

        private void SetAnimationClear()
        {
            animator.SetBool("jumping", false);
            animator.SetBool("flying", false);
            animator.SetBool("flyingIdle", false);
            animator.SetBool("falling", false);
            animator.SetBool("running", false);
        }

        private void SetMoveState(MoveState moveState)
        {
            this.moveState = moveState;
            switch (moveState)
            {
                case MoveState.Idle:
                    SetAnimaitonIdle();
                    break;
                case MoveState.Moving:
                    SetAnimationClear();
                    SetAnimation("running", true);
                    SetAnimation("jumping", false);
                    break;
                case MoveState.PrepareToJump:
                    SetAnimationClear();
                    animator.SetTrigger("jump");
                    SetAnimation("jumping", true);
                    SetAnimation("running", false);
                    break;
                case MoveState.PrepareToFly:
                    SetAnimationClear();
                    animator.SetTrigger("fly");
                    SetAnimation("flying", true);
                    SetAnimation("flyingIdle", false);
                    break;
                case MoveState.Flying:
                    SetAnimationClear();
                    SetAnimation("flying", true);
                    SetAnimation("flyingIdle", false);
                    break;
                case MoveState.FlyingIdle:
                    SetAnimation("flyingIdle", true);
                    break;
                case MoveState.Falling:
                    SetAnimationClear();
                    animator.SetTrigger("fall");
                    SetAnimation("falling", true);
                    break;
            }
        }





        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log("touch floor?");
            if (collision.transform.name == "Floor")
            {
                //Debug.Log("touch floor!");
                isOnGround = true;
                if (moveState == MoveState.Flying) { SetMoveState(MoveState.Idle); }
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            //Debug.Log("touch floor?");
            if (collision.transform.name == "Floor")
            {
                //Debug.Log("touch floor!");
                isOnGround = false;
            }
        }

        public enum MoveState
        {
            /// <summary> when Amlos is on the ground </summary>
            Idle,
            /// <summary> when Amlos is start to jump </summary>
            PrepareToJump,
            /// <summary> when Amlos is jumping </summary>
            Jumping,
            /// <summary> when Amlos is flying </summary>
            Flying,

            FlyingIdle,

            Moving,

            PrepareToFly,

            Falling,
        }
    }
}