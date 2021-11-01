using UnityEngine;

namespace Minerva.Module
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject followingObject;

        public void FixedUpdate()
        {
            Vector3 position = followingObject.transform.position;
            position.z = transform.position.z;
            transform.position = position;
        }
    }
}