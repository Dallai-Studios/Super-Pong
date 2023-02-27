using System;
using DallaiStudios.SuperPong.Enums;
using DallaiStudios.SuperPong.Managers;
using DallaiStudios.SuperPong.ScriptableObjects;
using DallaiStudios.SuperPong.Types;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DallaiStudios.SuperPong.Behaviors
{
    /// <summary>
    /// Behavior that handles all the actions of game UI
    /// </summary>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    public class GameUIBehavior : MonoBehaviour
    {
        [Header("Define the Game Score")]
        [SerializeField] private GameScore gameScore;

        [Header("Define the both score texts")] 
        [SerializeField] private TMP_Text leftScoreText;
        [SerializeField] private TMP_Text rightScoreText;
        
        [Header("Define all the sound effects to play during the animation")]
        [SerializeField] private AudioClip countdownOneSfx;
        [SerializeField] private AudioClip countdownTwoSfx;
        [SerializeField] private AudioClip countdownThreeSfx;
        [SerializeField] private AudioClip countdownStartSfx;
        [SerializeField] private string animationClip;

        [Header("Define the 'Pause' menu attributes")]
        [SerializeField] private GameObject pauseGameMenuUI;
        [SerializeField] private Button resumeGameButton;
        [SerializeField] private Button restartGameButton;
        [SerializeField] private Button quitGameButton;
        
        private Animator animator;
        private bool canPauseGame = false;

        private void Start()
        {
            this.animator = this.GetComponent<Animator>();
            GameManager.OnResetGame += this.RestartCountdownAnimation;
            this.gameScore.OnScoreChange += this.UpdateScore;
            this.resumeGameButton.onClick.AddListener(this.ResumeGame);
            this.restartGameButton.onClick.AddListener(this.RestartGame);
            this.quitGameButton.onClick.AddListener(this.ReturnToMainMenu);
        }

        private void OnDisable()
        {
            GameManager.OnResetGame -= this.RestartCountdownAnimation;
            this.gameScore.OnScoreChange -= this.UpdateScore;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && this.canPauseGame)
            {
                GameManager.Instace.SetGameState(GameState.STOPPED);
                this.ShowPauseMenu(true);
            }
        }

        private void ShowPauseMenu(bool value)
        {
            this.pauseGameMenuUI.SetActive(value);
        }

        private void ResumeGame()
        {
            GameManager.Instace.SetGameState(GameState.PLAYING);
            this.ShowPauseMenu(false);
        }

        private void RestartGame()
        {
            this.ShowPauseMenu(false);
            GameManager.Instace.RestartGame();   
        }

        private void ReturnToMainMenu()
        {
            this.ShowPauseMenu(false);
            GameManager.Instace.LoadScene(GameScenes.MAIN_MENU);   
        }

        /// <summary>
        /// Method that updates the score of the player or the enemy
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void UpdateScore()
        {
            this.leftScoreText.text = this.gameScore.LeftScore.ToString();
            this.rightScoreText.text = this.gameScore.RightScore.ToString();
        }

        /// <summary>
        /// Makes the UI restart the countdown animation before starting the game again
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void RestartCountdownAnimation() => this.animator.Play("");

        /// <summary>
        /// Starts the game by sending a command to the Game Manager.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void StartGame()
        {
            this.canPauseGame = true;
            GameManager.Instace.SetGameState(GameState.PLAYING);
        }

        /// <summary>
        /// Play the sound effect of the number three countdown
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void PlayCountDownThreeSFX()
        {
            this.canPauseGame = false;
            SoundEffectManager.Instance.Play(this.countdownThreeSfx);
        }

        /// <summary>
        /// Play the sound effect of the number two countdown
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void PlayCountDownTwoSFX() => SoundEffectManager.Instance.Play(this.countdownTwoSfx);

        /// <summary>
        /// Play the sound effect of the number one countdown
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void PlayCountDownOneSFX() => SoundEffectManager.Instance.Play(this.countdownOneSfx);

        /// <summary>
        /// Play the sound effect of the start game
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public void PlayCountDownStartSFX() => SoundEffectManager.Instance.Play(this.countdownStartSfx);
    }
}