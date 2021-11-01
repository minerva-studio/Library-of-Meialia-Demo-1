using System;
using UnityEngine;

namespace Minerva.Module
{
    public class Rotation : MonoBehaviour
    {
        public Vector3 finalAngle;
        public Vector3 curAngle;
        public float Speed = 20;
        public bool constant = false;

        // Start is called before the first frame update
        private void Start()
        {
            curAngle = transform.eulerAngles;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!constant)
            {
                LinearRotation();
            }
            else
            {
                ConstantRotation();
            }
        }

        private void ConstantRotation()
        {
            transform.localEulerAngles += Vector3.forward * Speed / 2;
        }

        public void LinearRotation()
        {
            curAngle = transform.eulerAngles;
            Vector3 fixedFinal;
            Vector3 fixedCur;

            fixedFinal = finalAngle.z > 0 ? finalAngle : new Vector3(finalAngle.x, finalAngle.y, 360 + finalAngle.z);
            fixedCur = curAngle.z > 0 ? curAngle : new Vector3(curAngle.x, curAngle.y, 360 + curAngle.z);

            transform.eulerAngles = fixedFinal.z - fixedCur.z < 180 ? Vector3.Lerp(fixedCur, fixedFinal, Speed * Time.deltaTime) : transform.eulerAngles = Vector3.Lerp(fixedCur, finalAngle, Speed * Time.deltaTime);//使用原来的Final

            if (curAngle.z - fixedFinal.z < 1)
            {
                transform.eulerAngles = fixedFinal;
                Destroy(this);
            }
        }

        public static void SetRotation(GameObject obj, Vector3 finalAngle)
        {
            if (finalAngle.z > 360)
            {
                while (finalAngle.z > 360)
                {
                    finalAngle.z -= 360;
                }
            }

            if (finalAngle.z < 0)
            {
                while (finalAngle.z < 0)
                {
                    finalAngle.z += 360;
                }
            }

            (obj.GetComponent<Rotation>() ?? obj.AddComponent<Rotation>()).finalAngle = finalAngle;
        }

        public static void SetRotation(GameObject obj, int speed = 20)
        {
            var c = obj.GetComponent<Rotation>() ?? obj.AddComponent<Rotation>();
            c.constant = true;
            c.Speed = speed;
        }
    }
}