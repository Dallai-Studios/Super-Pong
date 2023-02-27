using System;
using DallaiStudios.SuperPong.ScriptableObjects;
using UnityEngine;

namespace DallaiStudios.SuperPong.Managers
{
    /// <summary>
    /// Manager class that handles all the sound effects on the game.
    /// </summary>
    /// <requires>AudioSource</requires>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectManager : MonoBehaviour
    {
        [Header("Define the Game Configurations")]
        [SerializeField] private GameConfiguration gameConfiguration;

        public static SoundEffectManager Instance;
        
        private AudioSource audioSource;

        private void Awake() => Instance ??= this;
        
        private void Start()
        {
            this.audioSource = this.GetComponent<AudioSource>();
            this.gameConfiguration.OnChangeVolumes += this.UpdateVolume;
        }

        private void OnDisable()
        {
            this.gameConfiguration.OnChangeVolumes -= this.UpdateVolume;
        }

        /// <summary>
        /// Method that receives and audio clip and play this audio clip as a sound effect.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void Play(AudioClip clip)
        {
            this.audioSource.clip = clip;
            this.audioSource.loop = false;
            this.audioSource.volume = this.gameConfiguration.SFXVolume;
            this.audioSource.Play();
        }
        
        private void UpdateVolume()
        {
            this.audioSource.volume = this.gameConfiguration.SFXVolume;
        }
    }
}