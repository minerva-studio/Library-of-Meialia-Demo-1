using System.Collections.Generic;
using UnityEngine;

namespace Minerva.Module
{
    public class LocalMotion : MonoBehaviour
    {
        public static List<LocalMotion> ongoingMotions = new List<LocalMotion>();

        public event EndMotion MotionEndEvent;

        public Vector3 startingPos;
        public Vector3 finalPos;
        public Vector3 curPos;

        public bool isUIMotion;
        public float speed = 6;
        public float minimumDistance = 0.5f;

        // Start is called before the first frame update
        private void Start()
        {
            if (isUIMotion) ongoingMotions.Add(this);
            else ongoingMotions.Remove(this);
            startingPos = curPos = transform.localPosition;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            Move();
        }

        public virtual void Move()
        {
            curPos = transform.localPosition;
            Vector3 newPos = Vector3.Lerp(curPos, finalPos, speed * Time.deltaTime);
            transform.localPosition = newPos;
            //Debug.Log(newPos);

            if (Vector3.Magnitude(curPos - finalPos) < minimumDistance)
            {
                transform.localPosition = finalPos;
                Arrive();
            }
        }

        public virtual void Arrive()
        {
            MotionEndEvent?.Invoke();
            ongoingMotions.Remove(this);
            Debug.Log("Arrived");
            Destroy(this);
        }

        public void OnDestroy()
        {
            ongoingMotions.Remove(this);
        }

        public static LocalMotion SetMotion(GameObject obj, Vector3 finalPos)
        {
            LocalMotion motion = obj.GetComponent<LocalMotion>();
            if (!obj.GetComponent<LocalMotion>()) { motion = obj.AddComponent<LocalMotion>(); }

            Debug.Log(finalPos);
            motion.finalPos = finalPos;

            return motion;
        }

        public static LocalMotion SetMotion(GameObject obj, Vector3 finalPos, bool isUIMotion, EndMotion endMotionevent = null)
        {
            LocalMotion motion = SetMotion(obj, finalPos);

            motion.MotionEndEvent += endMotionevent;
            motion.isUIMotion = isUIMotion;
            return motion;
        }

        public static void StopMotion(GameObject gameObject)
        {
            if (gameObject.GetComponent<LocalMotion>())
            {
                Destroy(gameObject.GetComponent<LocalMotion>());
            }
        }
    }
}