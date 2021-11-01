using UnityEngine;

namespace Minerva.Module
{
    public class Scaler : MonoBehaviour
    {
        public Vector3 finalScale;
        public Vector3 curScale;

        public Vector3 dv => Vector3.Normalize(finalScale - curScale) / 30 * Speed;
        public float Speed = 20;

        // Start is called before the first frame update
        private void Start()
        {
            curScale = transform.localScale;
        }

        // Update is called once per frame
        private void Update()
        {
            ChangeScale();
        }

        private void ChangeScale()
        {
            curScale = transform.localScale;
            transform.localScale += dv;

            if (Vector3.Magnitude(transform.localScale - finalScale) < 1)
            {
                Destroy(this);
            }
        }

        public static void SetScaler(GameObject obj, Vector3 finalSize)
        {
            (obj.GetComponent<Scaler>() ?? obj.AddComponent<Scaler>()).finalScale = finalSize;
        }
    }
}