using Amlos.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Amlos
{
    public class BookSoldierAttacker : MonoBehaviour
    {
        public Animator animator;
        public GameObject ballPrefab;
        public GameObject source;
        public float nextFireballTime;
        public float time;

        public const int maxAngle = 30;

        public float FireWaitTimePercentage => time / nextFireballTime;
        public Transform Target => Simulation.GetModel<Player>().controller.transform;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {

        }

        public void FixedUpdate()
        {
            time += Time.deltaTime;
            if (time > nextFireballTime)
            {
                StartCoroutine(Attack());
                time = 0;
            }
        }

        IEnumerator Attack()
        {
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
                CreateFireball();
            }
        }

        private void CreateFireball()
        {
            var ball = Instantiate(ballPrefab, transform);
            Vector3 normalized = (Target.position - source.transform.position).normalized;
            //if (Mathf.Sign(transform.localScale.x) == Mathf.Sign(normalized.x))
            //{
            //    normalized *= -1;
            //}
            ball.name = "Ball";
            ball.transform.position = source.transform.position + normalized * 1;
            ball.GetComponent<BookSoldierFireballController>().velocity = normalized * ball.GetComponent<BookSoldierFireballController>().Speed;
        }
    }
}