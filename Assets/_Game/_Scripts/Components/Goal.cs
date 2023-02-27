using System;
using DallaiStudios.SuperPong.Enums;
using DallaiStudios.SuperPong.Managers;
using DallaiStudios.SuperPong.ScriptableObjects;
using DallaiStudios.SuperPong.Types;
using UnityEngine;

namespace DallaiStudios.SuperPong.Components
{
    /// <summary>
    /// Component that handles the Goal behavior.
    /// </summary>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    public class Goal : MonoBehaviour
    {
        [Header("Define the side of the goal")] 
        [SerializeField] private GoalSide goalSide;

        [Header("Define the Game Score reference")] 
        [SerializeField] private GameScore gameScore;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.BALL))
            {
                if (this.goalSide == GoalSide.LEFT) this.gameScore.RightScore++;
                if (this.goalSide == GoalSide.RIGHT) this.gameScore.LeftScore++;
                this.gameScore.RaiseEventOnScoreChange();
                GameManager.Instace.ResetGame();
            }
        }
    }
}