using Canute.Module;
using System.Collections.Generic;
using UnityEngine;


namespace Minerva.Module
{
    public delegate void EndMotion();
    public class Motion : MonoBehaviour
    {
        public static List<Motion> ongoingMotions = new List<Motion>();

        public event EndMotion MotionEndEvent;

        protected Vector3 startingPos;
        protected Vector3 finalPos;
        protected Vector3 curPos;

        public float speed = 1;
        public float minimumDistance = 0.02f;

        private void Awake()
        {
            if (!ongoingMotions.Contains(this)) ongoingMotions.Add(this);
        }


        // Start is called before the first frame update
        private void Start()
        {
            if (!ongoingMotions.Contains(this)) ongoingMotions.Add(this);
            startingPos = curPos = transform.position;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            Move();
        }

        public virtual void Move()
        {
            curPos = transform.position;
            transform.position = Vector3.Lerp(curPos, finalPos, 6 * speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, finalPos.z);

            if (Vector3.Magnitude(transform.position - finalPos) < minimumDistance)
            {
                transform.position = finalPos;
                Arrive();
            }
        }

        public virtual void Arrive()
        {
            MotionEndEvent?.Invoke();
            ongoingMotions.Remove(this);
            Destroy(this);
        }

        public void OnDestroy()
        {
            ongoingMotions.Remove(this);
        }

        public static Motion SetMotion(GameObject obj, Vector3 finalPos)
        {
            var motion = obj.GetComponent<Motion>();
            if (!obj.GetComponent<Motion>()) { motion = obj.AddComponent<Motion>(); }
            motion.finalPos = finalPos;
            ongoingMotions.Add(motion);

            return motion;
        }

        public static Motion SetMotion(GameObject obj, Vector3 finalPos, EndMotion endMotionevent = null)
        {
            Motion motion = SetMotion(obj, finalPos);

            motion.MotionEndEvent += endMotionevent;
            return motion;
        }

        public static void StopMotion(GameObject gameObject)
        {
            if (gameObject.GetComponent<Motion>())
            {
                Destroy(gameObject.GetComponent<Motion>());
            }
        }
    }
}