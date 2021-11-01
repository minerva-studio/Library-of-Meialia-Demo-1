using Amlos.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Amlos
{
    public class Fireballs
    {
        public List<FireBallControllerBase> fireBalls = new List<FireBallControllerBase>();

        public bool HasBallWithinDistance(Vector2 pos, float radius)
        {
            foreach (var item in fireBalls)
            {
                if (item)
                {
                    var dist = (item.transform.position - new Vector3(pos.x, pos.y)).magnitude;
                    if (dist > radius) continue;
                    else return true;
                }
            }
            return false;
        }
    }
}