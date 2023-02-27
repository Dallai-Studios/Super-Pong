using DallaiStudios.SuperPong.ScriptableObjects;
using UnityEngine;

namespace DallaiStudios.SuperPong.Managers
{
    /// <summary>
    /// Manager class that handles all the musics on the game.
    /// </summary>
    /// <requires>AudioSource</requires>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [Header("Define the game configurations")]
        [SerializeField] private GameConfiguration gameConfiguration;

        [Header("Define the music to play")] 
        [SerializeField] private AudioClip sceneMusic;
        
        private AudioSource audioSource;

        private void Start()
        {
            this.audioSource = this.GetComponent<AudioSource>();
            this.Play();
            this.gameConfiguration.OnChangeVolumes += this.UpdateVolume;
        }
        
        private void OnDisable()
        {
            this.gameConfiguration.OnChangeVolumes -= this.UpdateVolume;
        }
        
        /// <summary>
        /// Method that plays the defined audio clip as a music.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void Play()
        {
            if (this.audioSource.isPlaying) this.audioSource.Stop();
            this.audioSource.clip = this.sceneMusic;
            this.audioSource.loop = true;
            this.audioSource.volume = this.gameConfiguration.MusicVolume;
            this.audioSource.Play();
        }

        private void UpdateVolume()
        {
            this.audioSource.volume = this.gameConfiguration.MusicVolume;
        }
    }
}