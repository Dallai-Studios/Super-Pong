using System;
using DallaiStudios.SuperPong.Enums;

namespace DallaiStudios.SuperPong.Events
{
    /// <summary>
    /// Static class that handles all the game state events.
    /// </summary>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    public class GameStateEvents
    {
        public static event Action<GameState> OnGameStateChange;

        /// <summary>
        /// Method that raise the game state events
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        public static void RaiseEventOnGameStateChange(GameState newGameState)
        {
            OnGameStateChange?.Invoke(newGameState);
        }
    }
}