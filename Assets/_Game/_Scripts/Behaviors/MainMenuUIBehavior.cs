using System;
using DallaiStudios.SuperPong.Enums;
using DallaiStudios.SuperPong.Managers;
using DallaiStudios.SuperPong.ScriptableObjects;
using DallaiStudios.SuperPong.Types;
using UnityEngine;
using UnityEngine.UI;

namespace DallaiStudios.SuperPong.Behaviors
{
    /// <summary>
    /// Behavior that handles all the main menu actions
    /// </summary>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    public class MainMenuUIBehavior : MonoBehaviour
    {
        [Header("Define the game configurations")]
        [SerializeField] private GameConfiguration gameConfiguration;

        [Header("Define the main menu attributes")]
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private Button playButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button quitButton;

        [Header("Define the play menu attributes")]
        [SerializeField] private GameObject playMenuUI;
        [SerializeField] private Button playWithAIButton;
        [SerializeField] private Button playWithPlayerButton;

        [Header("Define the difficult choose menu attributes")]
        [SerializeField] private GameObject gameDifficultMenuUI;
        [SerializeField] private Button normalDifficultButton;
        [SerializeField] private Button hardDifficultButton;
        [SerializeField] private Button unfairDifficultButton;

        [Header("Define the options menu attributes")]
        [SerializeField] private GameObject optionsUI;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Button controlsButton;
        [SerializeField] private Button returnFromOptionsButton;

        [Header("Define the controls menu attributes")]
        [SerializeField] private GameObject controlsMenuUI;
        [SerializeField] private Button returnFromControlsButton;

        [Header("Define the quit menu attributes")]
        [SerializeField] private GameObject confirmExitUI;
        [SerializeField] private Button quitYesButton;
        [SerializeField] private Button quitNoButton;

        private void Start()
        {
            this.SetupButtons();
            this.OpenMainMenu();
        }
        
        /// <summary>
        /// Method that hides all the sub menus of the main menu
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void InactivateAllUIs()
        {
            this.mainMenuUI.SetActive(false);
            this.playMenuUI.SetActive(false);
            this.gameDifficultMenuUI.SetActive(false);
            this.optionsUI.SetActive(false);
            this.controlsMenuUI.SetActive(false);
            this.confirmExitUI.SetActive(false);
        }
        
        /// <summary>
        /// Display the main menu on the UI
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void OpenMainMenu()
        {
            this.InactivateAllUIs();
            this.mainMenuUI.SetActive(true);
        }

        /// <summary>
        /// Display the play sub menu on the UI
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void OpenPlayMenu()
        {
            this.InactivateAllUIs();
            this.playMenuUI.SetActive(true);
        }

        private void OpenGameDifficultMenu()
        {
            this.InactivateAllUIs();
            this.gameDifficultMenuUI.SetActive(true);
        }
        
        /// <summary>
        /// Display the options sub menu on the UI
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void OpenOptionsMenu()
        {
            this.InactivateAllUIs();
            this.optionsUI.SetActive(true);
        }

        /// <summary>
        /// Display the controls sub menu on the UI
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void OpenControlsMenu()
        {
            this.InactivateAllUIs();
            this.controlsMenuUI.SetActive(true);
        }
        
        /// <summary>
        /// Display the exit confirmation sub menu on the UI
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void OpenExitConfirmationMenu()
        {
            this.InactivateAllUIs();
            this.confirmExitUI.SetActive(true);
        }

        /// <summary>
        /// Method that build all the main menu event listeners
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void SetupButtons()
        {
            this.playButton.onClick.AddListener(this.OpenPlayMenu);
            this.playWithAIButton.onClick.AddListener(this.OpenGameDifficultMenu);
            this.playWithPlayerButton.onClick.AddListener(this.GoToGameWithPlayer);
            
            this.normalDifficultButton.onClick.AddListener(() => this.GoToGameWithAI(GameDifficult.NORMAL));
            this.hardDifficultButton.onClick.AddListener(() => this.GoToGameWithAI(GameDifficult.HARD));
            this.unfairDifficultButton.onClick.AddListener(() => this.GoToGameWithAI(GameDifficult.UNFAIR));

            this.optionsButton.onClick.AddListener(this.OpenOptionsMenu);
            this.controlsButton.onClick.AddListener(this.OpenControlsMenu);            
            this.returnFromOptionsButton.onClick.AddListener(this.UpdateConfigurationParameters);
            
            this.returnFromControlsButton.onClick.AddListener(this.OpenOptionsMenu);
            
            this.quitButton.onClick.AddListener(this.OpenExitConfirmationMenu);
            this.quitYesButton.onClick.AddListener(this.QuitGame);
            this.quitNoButton.onClick.AddListener(this.OpenMainMenu);
        }

        /// <summary>
        /// Move the player to the game against AI
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void GoToGameWithAI(GameDifficult gameDifficult)
        {
            GameManager.Instace.SetGameDifficult(gameDifficult);
            GameManager.Instace.GoToGame(GameScenes.GAME_WITH_AI);
        }

        /// <summary>
        /// Move the player to the game against another player
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void GoToGameWithPlayer()
        {
            GameManager.Instace.SetGameDifficult(GameDifficult.NORMAL);
            GameManager.Instace.GoToGame(GameScenes.GAME_WITH_PLAYER);
        }

        /// <summary>
        /// Retrieve the data from the game configuration SO and builds the options UI
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void SetupConfigurationParameters()
        {
            this.musicVolumeSlider.value = this.gameConfiguration.MusicVolume;
            this.sfxVolumeSlider.value = this.gameConfiguration.SFXVolume;
        }

        /// <summary>
        /// Updates the game configuration SO using the UI sliders 
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void UpdateConfigurationParameters()
        {
            this.gameConfiguration.MusicVolume = this.musicVolumeSlider.value;
            this.gameConfiguration.SFXVolume = this.sfxVolumeSlider.value;
            this.gameConfiguration.RaiseEventOnChangeVolumes();
            this.InactivateAllUIs();
            this.mainMenuUI.SetActive(true);
        }

        /// <summary>
        /// Method that closes the application
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void QuitGame() => Application.Quit();
    }
}