using System.Collections;
using UnityEngine;

namespace Amlos
{
    [CreateAssetMenu]
    public class FireballAudioData : ScriptableObject
    {
        public AudioClip[] moving;
        public AudioClip[] deflect;
        public AudioClip[] explode;

    }
}