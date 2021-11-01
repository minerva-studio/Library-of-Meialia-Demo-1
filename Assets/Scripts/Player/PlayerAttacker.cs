using Amlos.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Amlos
{
    public class PlayerAttacker : MonoBehaviour
    {
        public Animator animator;
        public GameObject ballPrefab;
        public GameObject mouth;
        public float nextFireballTime;
        public float time;

        public UnityEvent allowAttackEvent;
        public UnityEvent endAttackEvent;
        public const int maxAngle = 30;

        public float FireWaitTimePercentage => time / nextFireballTime;

        private bool CanPlayerAttack;

        private void Awake()
        {
            Simulation.GetModel<Player>().attacker = this;
            animator = GetComponent<Animator>();
            this.enabled = false;
        }

        private void Start()
        {

        }

        public void Update()
        {
            CheckAttackStatus();
        }

        private void CheckAttackStatus()
        {
            switch (Simulation.GetModel<Player>().controller.moveState)
            {
                case PlayerController.MoveState.Flying:
                case PlayerController.MoveState.FlyingIdle:
                    ChangeAttackStatus(!false);
                    break;
                case PlayerController.MoveState.Idle:
                case PlayerController.MoveState.PrepareToJump:
                case PlayerController.MoveState.Jumping:
                case PlayerController.MoveState.Moving:
                case PlayerController.MoveState.PrepareToFly:
                case PlayerController.MoveState.Falling:
                default:
                    ChangeAttackStatus(false);
                    break;
            }
        }

        public void FixedUpdate()
        {
            time += Time.deltaTime;
            if (time > nextFireballTime && CanPlayerAttack)
            {
                StartCoroutine(Attack());
                time = 0;
            }
        }

        IEnumerator Attack()
        {
            yield return new WaitForFixedUpdate();
            PlayerController.MoveState moveState = Simulation.GetModel<Player>().controller.moveState;
            while (moveState != PlayerController.MoveState.Flying && moveState != PlayerController.MoveState.FlyingIdle)
            {
                yield return new WaitForFixedUpdate();
            }

            yield return PerformAttackAnimation();
            CreateFireball();

        }

        private void CreateFireball()
        {
            var ball = Instantiate(ballPrefab, mouth.transform.parent.parent);
            Vector3 normalized = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouth.transform.position).normalized;
            {
                Vector3 dist_normalized = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                var angle = Mathf.Atan2(dist_normalized.y, dist_normalized.x) * Mathf.Rad2Deg;
                if (angle > 90) angle -= 180;
                if (angle < -90) angle += 180;
                //Debug.Log(angle);
                if (Mathf.Abs(angle) > maxAngle)
                {
                    var axis = new Vector2(normalized.x, 0).normalized;
                    //Debug.Log(normalized);
                    normalized = new Vector2(axis.x, axis.x * Mathf.Sign(angle) * Mathf.Tan(30 * Mathf.Deg2Rad)).normalized;
                    //Debug.Log(normalized);
                }
            }
            if (Mathf.Sign(transform.localScale.x) != Mathf.Sign(normalized.x))
            {
                normalized *= -1;
            }
            ball.name = "Ball";
            ball.transform.position = mouth.transform.position + normalized * 1;
            ball.GetComponent<PlayerFireBallController>().SetBallLevel(4);
            ball.GetComponent<PlayerFireBallController>().velocity = normalized * ball.GetComponent<PlayerFireBallController>().Speed;
            //ball.GetComponent<Ball>().body.velocity = normalized * ball.GetComponent<Ball>().speed;
            //ball.GetComponent<Ball>().force = normalized * ball.GetComponent<Ball>().force.magnitude;
        }

        public void ChangeAttackStatus(bool attackStatus)
        {
            if (attackStatus != CanPlayerAttack)
            {
                if (attackStatus) allowAttackEvent?.Invoke();
                else endAttackEvent?.Invoke();
            }
            CanPlayerAttack = attackStatus;
        }

        IEnumerator PerformAttackAnimation()
        {
            animator.SetBool("attacking", true);
            if (Simulation.GetModel<Player>().controller.moveState == PlayerController.MoveState.Flying) animator.Play("Flying Attack");
            else animator.Play("FlyingIdle Attack");
            for (int i = 0; i < 8; i++) yield return new WaitForFixedUpdate();
            animator.SetBool("attacking", false);
        }
    }
}