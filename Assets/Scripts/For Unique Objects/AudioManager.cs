using UnityEngine;

namespace For_Unique_Objects
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _crystalPickUp;
        [SerializeField] private AudioClip _jump;
        [SerializeField] private AudioClip _deathSound;

        [SerializeField] private AudioPrefab _audioPrefab;

        public static AudioManager Instance;
        
        private AudioPrefab _spawnedCrystallAudio;
        private AudioPrefab _spawnedJumpAudio;
        private AudioPrefab _spawnedDeathSound;

        public bool Muted;
        
        private void Awake()
        {
            Instance = this;

            _spawnedCrystallAudio = Instantiate(_audioPrefab);
            _spawnedJumpAudio = Instantiate(_audioPrefab);
            _spawnedDeathSound = Instantiate(_audioPrefab);
            
            var state = PlayerPrefs.GetInt("AudioState") == 1;

            Muted = state;
            
            _spawnedCrystallAudio.gameObject.SetActive(state);
            _spawnedJumpAudio.gameObject.SetActive(state);
            _spawnedDeathSound.gameObject.SetActive(state);

            if (state)
            {
                MusicControl.Instance.UnMute();
            }
            else
            {
                MusicControl.Instance.Mute();
            }
        }

        public void SwitchState()
        {
            var state = PlayerPrefs.GetInt("AudioState") == 1;

            state = !state;
            
            _spawnedCrystallAudio.gameObject.SetActive(state);
            _spawnedJumpAudio.gameObject.SetActive(state);
            _spawnedDeathSound.gameObject.SetActive(state);

            if (state)
            {
                MusicControl.Instance.UnMute();
            }
            else
            {
                MusicControl.Instance.Mute();
            }

            Muted = state;
            
            PlayerPrefs.SetInt("AudioState", state ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void PlayCrystalPick(float volume)
        {
            _spawnedCrystallAudio.Play(_crystalPickUp, volume);
        }

        public void PlayJump(float volume)
        {
            _spawnedJumpAudio.Play(_jump, volume);
        }

        public void PlayDeathSound()
        {
            _spawnedDeathSound.Play(_deathSound, 0.1f);
        }
    }
}