using System;
using UnityEngine;

namespace DallaiStudios.SuperPong.ScriptableObjects
{
    /// <summary>
    /// Scriptable Object class that deals with the game score.
    /// </summary>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    [CreateAssetMenu(fileName = "Game Score", menuName = "Super Pong!/Game Score")]
    public class GameScore : ScriptableObject
    {
        public int LeftScore;
        public int RightScore;
        public int WinCondition;

        public event Action OnScoreChange;

        /// <summary>
        /// Method that raises the score event. This must be used when the score is changed.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void RaiseEventOnScoreChange() => this.OnScoreChange?.Invoke();
        
        /// <summary>
        /// Method to reset the game score.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void ResetScore()
        {
            this.LeftScore = 0;
            this.RightScore = 0;
            this.OnScoreChange?.Invoke();
        }
    }
}