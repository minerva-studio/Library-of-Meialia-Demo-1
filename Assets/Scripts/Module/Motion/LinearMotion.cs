using UnityEngine;

namespace Minerva.Module
{
    public class LinearMotion : Motion
    {
        public float second = 1;
        public float currentSecond;

        public Vector3 Delta => finalPos - startingPos;
        public float Percentage => currentSecond / second;

        // Start is called before the first frame update
        private void Start()
        {
            startingPos = curPos = transform.position;
            currentSecond = 0;
        }

        // Update is called once per frame
        public override void Update()
        {
            Debug.Log(Delta * Time.deltaTime);
            currentSecond += Time.deltaTime;
            Move();
        }

        public override void Move()
        {
            curPos = transform.position;
            transform.position = startingPos + Delta * Percentage;

            if (currentSecond > second)
            {
                transform.position = finalPos;
                Arrive();
            }
        }


        public static new LinearMotion SetMotion(GameObject obj, Vector3 finalPos, EndMotion endMotionevent = null)
        {
            LinearMotion motion = obj.GetComponent<LinearMotion>();
            if (!obj.GetComponent<LinearMotion>()) { motion = obj.AddComponent<LinearMotion>(); }
            motion.MotionEndEvent += endMotionevent;
            motion.finalPos = finalPos;

            return motion;
        }

        public static new void StopMotion(GameObject gameObject)
        {
            if (gameObject.GetComponent<LinearMotion>())
            {
                Destroy(gameObject.GetComponent<LinearMotion>());
            }
        }
    }
}