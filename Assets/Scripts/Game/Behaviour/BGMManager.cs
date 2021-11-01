using Amlos.Core;
using System.Collections;
using UnityEngine;

namespace Amlos.Behaviour
{
    public class BGMManager : MonoBehaviour
    {
        public AudioSource audioSource;

        public AudioClip gameBGM;
        public AudioClip mainPageBGM;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            Simulation.GetModel<GameSound>().BGMManager = this;
        }
        // Use this for initialization
        void Start()
        {
            Simulation.GetModel<GameSoundData>().SetGameBGMSoundVolumn();
            PlayGameBGM();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayMainBGM()
        {
            SetClipAndPlay(mainPageBGM);
        }

        public void PlayGameBGM()
        {
            SetClipAndPlay(gameBGM);
        }

        public void SetClipAndPlay(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void SetGameBGMSoundVolumn(float value)
        {
            audioSource.volume = value;
        }
    }
}