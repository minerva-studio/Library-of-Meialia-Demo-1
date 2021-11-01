using UnityEngine;

namespace Minerva.Module
{
    public class FollowMouseMove : MonoBehaviour
    {
        public bool followX = true;
        public bool followY = true;
        public bool followZ = true;

        public Vector3 lastPos;
        public Vector3 input;

        public static Vector3 UserInputPosition => GetUserInput();

        public void Start()
        {
            lastPos = UserInputPosition;
        }

        public void Update()
        {
            Vector3 delta = GetMovementDelta();

            transform.position += delta;
            input = UserInputPosition;
            lastPos = UserInputPosition;
        }

        private Vector3 GetMovementDelta()
        {
            Vector3 delta = UserInputPosition - lastPos;
            if (!followX) { delta.x = 0; }
            if (!followY) { delta.y = 0; }
            if (!followZ) { delta.z = 0; }

            return delta;
        }

        private static Vector3 GetUserInput()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 100;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}