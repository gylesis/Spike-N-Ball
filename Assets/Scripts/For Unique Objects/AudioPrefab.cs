using UnityEngine;

namespace For_Unique_Objects
{
    public class AudioPrefab : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void Play(AudioClip audioClip, float volume)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;
            _audioSource.Play();
        }
        
    }
}