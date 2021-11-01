using Amlos.Core;
using System.Collections;
using UnityEngine;

namespace Amlos
{

    public class MapCollider : MonoBehaviour
    {
        public Collider2D platformCollider;

        private void Awake()
        {
            Simulation.GetModel<Map>().mapColliders = this;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}