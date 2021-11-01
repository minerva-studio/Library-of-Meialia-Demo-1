using Canute.Module;
using UnityEngine;

namespace Minerva.Module
{
    public class ConstantMotion : Motion
    {
        public Vector3 displacePerSecond;
        public float second;
        public float currentSecond;


        // Start is called before the first frame update
        private void Start()
        {
            startingPos = curPos = transform.position;
        }

        // Update is called once per frame
        public override void Update()
        {
            currentSecond += Time.deltaTime;
            Move();
        }

        public override void Move()
        {
            curPos = transform.position;
            transform.position += displacePerSecond / 60;

            if (currentSecond > second)
            {
                transform.position = finalPos;
                Arrive();
            }
        }

        public static void SetMotion(GameObject obj, Vector3 displacePerSecond, float sec, EndMotion endMotionevent = null)
        {
            ConstantMotion motion = obj.GetComponent<ConstantMotion>();
            if (!obj.GetComponent<ConstantMotion>()) { motion = obj.AddComponent<ConstantMotion>(); }
            motion.MotionEndEvent += endMotionevent;
            motion.second = sec;
            motion.displacePerSecond = displacePerSecond;
        }
    }
}