using System;
using UnityEngine;

namespace DallaiStudios.SuperPong.ScriptableObjects
{
    /// <summary>
    /// Scriptable Object Class that holds all the game configurations.
    /// </summary>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    [CreateAssetMenu(fileName = "Game Configuration", menuName = "Super Pong!/Game Configuration")]
    public class GameConfiguration : ScriptableObject
    {
        public float MusicVolume = 0.6f;
        public float SFXVolume = 0.6f;
        public int GameFrameRate = 60;

        public event Action OnChangeVolumes;

        public void RaiseEventOnChangeVolumes()
        {
            this.OnChangeVolumes?.Invoke();
        }
    }
}