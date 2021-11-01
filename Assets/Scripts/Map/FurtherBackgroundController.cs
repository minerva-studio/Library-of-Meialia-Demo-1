using Amlos.Core;
using UnityEngine;

namespace Amlos
{


    public class FurtherBackgroundController : MonoBehaviour
    {

        public void FixedUpdate()
        {
            var v3 = Simulation.GetModel<Player>().controller.transform.position;
            var dist = v3.x;
            var pos = transform.position;
            pos.x = dist * 0.5f;
            transform.position = pos;
        }

    }
}