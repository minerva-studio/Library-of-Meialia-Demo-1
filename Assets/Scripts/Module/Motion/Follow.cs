using UnityEngine;

namespace Minerva.Module
{
    /// <summary>
    /// Component to follow a GameObject
    /// </summary>
    public class Follow : MonoBehaviour
    {
        public GameObject followTo;
        public Vector3 startingPos;
        public Vector3 FinalPos => followTo.transform.position;
        public Vector3 curPos;
        public float speed = 6;
        public float minimumDistance = 1;

        // Start is called before the first frame update
        private void Start()
        {
            startingPos = curPos = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            LinearFollow();
        }

        public virtual void Move()
        {
            LinearFollow();
        }

        public virtual void LinearFollow()
        {
            curPos = transform.position;
            transform.position = Vector3.Lerp(curPos, FinalPos, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, FinalPos.z);

            if (Vector3.Magnitude(transform.position - FinalPos) < minimumDistance)
            {
                transform.position = FinalPos;
                Arrive();
            }
        }

        public virtual void Arrive()
        {
        }

        public static void SetFollow(GameObject obj, GameObject followTo)
        {
            Follow follow = obj.GetComponent<Follow>() ? obj.GetComponent<Follow>() : obj.AddComponent<Follow>();
            follow.followTo = followTo;
        }

        public static void StopFollow(GameObject gameObject)
        {
            if (gameObject.GetComponent<Follow>())
            {
                Destroy(gameObject.GetComponent<Follow>());
            }
        }
    }
}