using System;
using System.Collections;
using UnityEngine;

namespace Amlos
{
    public class FireballTail : MonoBehaviour
    {
        public int tailParticleCount = 40;
        public new ParticleSystem particleSystem;

        public void SetTailActive(bool active)
        {
            var emission = particleSystem.emission;
            if (active) { emission.rateOverTime = tailParticleCount; }
            else { emission.rateOverTime = 0; }
        }
    }
}
