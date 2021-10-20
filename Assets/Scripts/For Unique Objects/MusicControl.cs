using System;
using For_UI;
using UnityEngine;

namespace For_Unique_Objects
{
    public class MusicControl : MonoBehaviour
    {
        public static MusicControl Instance { get; private set; }

        [SerializeField]
        AudioLowPassFilter LowPass;

        [SerializeField]
        AudioSource MainTheme;

        float Volume;
        float LowPassFilterVolume;
        [SerializeField] [Range(0, 1)] float modifierVolume;
        [SerializeField] [Range(30, 100)] float modifierLowPasFilter;

        [SerializeField]
        float lowestVolumePoint;

        private bool _muted;

        private void Awake()
        {
            Instance = this;      
        }

        void Start()
        {
            Volume = MainTheme.volume;
            LowPassFilterVolume = LowPass.cutoffFrequency;
        }

        public void MusicStart()
        {
            MainTheme.Play();
        }
        public void VolumingDownDeath()
        {
            if (MainTheme.volume > 0) MainTheme.volume -= modifierVolume;
        }
        public void VolumingDown()
        {
            if (MainTheme.volume > lowestVolumePoint) MainTheme.volume -= modifierVolume;
            if (LowPass.cutoffFrequency > 750) LowPass.cutoffFrequency -= modifierLowPasFilter;
        }
        public void VolumingUp()
        {
            if (MainTheme.volume < Volume) MainTheme.volume += modifierVolume;
            if (LowPass.cutoffFrequency < LowPassFilterVolume) LowPass.cutoffFrequency += modifierLowPasFilter;
        }

        public void Mute()
        {
            _muted = true;
        }

        public void UnMute()
        {
            _muted = false;
        }
        
        void Update()
        {
            if (_muted)
            {
                MainTheme.volume = 0;
                return;
            }

            if (MenuScript.GameIsPaused) VolumingDown();
            else if (!MenuScript.GameIsPaused && EnviromentAct.Instance.GameIsStarted) VolumingUp();

            if (PlayerControl.Instance.BoolOnDeath2) VolumingDownDeath();
        }
    }
}
