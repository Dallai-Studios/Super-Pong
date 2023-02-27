using System;
using DallaiStudios.SuperPong.Enums;
using DallaiStudios.SuperPong.Events;
using DallaiStudios.SuperPong.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DallaiStudios.SuperPong.Managers
{
    /// <summary>
    /// Manager class that controls the flux of the game and the state of the game.
    /// </summary>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    public class GameManager : MonoBehaviour
    {
        [Header("Define the game configurations")]
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private GameScore gameScore;
        
        public static GameManager Instace;
        public static event Action OnResetGame;

        private GameState currentGameState = GameState.STOPPED;
        private GameDifficult currentGameDifficult;
        
        private void Awake() => Instace ??= this;

        private void Start()
        {
            this.SetGameTargetFrameRate();
        }

        /// <summary>
        /// Method that can be used to define a new game state and raise the
        /// game state change event across the game.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void SetGameState(GameState newGameState)
        {
            if (newGameState != this.currentGameState)
            {
                this.currentGameState = newGameState;
                GameStateEvents.RaiseEventOnGameStateChange(newGameState);
            }
        }

        /// <summary>
        /// Method to retrieve the current game state.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public GameState GetGameState() => this.currentGameState;

        public void SetGameDifficult(GameDifficult newDifficult) => this.currentGameDifficult = newDifficult;
        
        public GameDifficult GetGameDifficult() => this.currentGameDifficult;
        
        /// <summary>
        /// Method that can be used to define the target frame rate of the game.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void SetGameTargetFrameRate()
        {
            Application.targetFrameRate = this.gameConfiguration.GameFrameRate;
        }

        /// <summary>
        /// Method that reset's the game and send the reset game event across the game.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void ResetGame()
        {
            this.SetGameState(GameState.STOPPED);
            OnResetGame?.Invoke();
        }
        
        /// <summary>
        /// Method that restarts the game, the score, and send the reset game event across the game.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void RestartGame()
        {
            this.SetGameState(GameState.STOPPED);
            this.gameScore.ResetScore();
            OnResetGame?.Invoke();
        }

        /// <summary>
        /// Method to move the player to the game scene depending on the game type.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void GoToGame(string gameTypeName)
        {
            this.gameScore.ResetScore();
            SceneManager.LoadScene(gameTypeName);
        }

        /// <summary>
        /// Method to move the player to a scene by name.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}