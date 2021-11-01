using Amlos.Core;
using UnityEngine;

namespace Amlos
{

    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectController : MonoBehaviour
    {
        public float volumnMultiple = 1;
        public float soundSideDistance = 1;
        public GameObject center;
        public AudioSource source;

        private GameSound GameSound => Simulation.GetModel<GameSound>();


        private void Awake()
        {
            source = GetComponent<AudioSource>();
            center = Simulation.GetModel<Player>().attacker.gameObject;
            GameSound.effectSoundController.Add(this);
            Simulation.GetModel<GameSoundData>().SetGameEffectSoundVolumn(this);
        }


        private void OnDestroy()
        {
            GameSound.effectSoundController.Remove(this);
        }

        public void Update()
        {
            UpdateSideDistanceEffect();
        }

        private void UpdateSideDistanceEffect()
        {
            Vector3 dist = transform.position - center.transform.position;
            var distance = dist.normalized.x * Mathf.Min(soundSideDistance, dist.magnitude) / soundSideDistance;
            source.panStereo = distance;
        }

        public void Play(params AudioClip[] audioClip)
        {
            source.clip = audioClip[Random.Range(0, audioClip.Length)];
            source.Play();
        }

        public void PlayNonOverride(params AudioClip[] audioClip)
        {
            if (!source.isPlaying)
            {
                source.clip = audioClip[Random.Range(0, audioClip.Length)];
                source.Play();
            }
        }

        public void SetVolume(float value)
        {
            source.volume = value * volumnMultiple;
        }
    }
}