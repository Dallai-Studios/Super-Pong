using System;
using DallaiStudios.SuperPong.Enums;
using DallaiStudios.SuperPong.Managers;
using DallaiStudios.SuperPong.Types;
using UnityEngine;

namespace DallaiStudios.SuperPong.Components
{
    /// <summary>
    /// Component that handles the Racket Movement.
    /// </summary>
    /// <requires>Rigidbody</requires>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    [RequireComponent(typeof(Rigidbody))]
    public class RacketMovement : MonoBehaviour
    {
        [Header("Define the IA parameters")]
        [SerializeField] private bool controlledByPlayer = true;
        [SerializeField] private float correctionOffset = 0.2f;
        
        [Header("Define the racket movement speed")]
        [SerializeField] private float movementSpeed;
        
        [Header("Define the Keys that will move the racket if not controlled by the AI")]
        [SerializeField] private KeyCode upKey;
        [SerializeField] private KeyCode downKey;

        private Rigidbody rigidbody;
        private Transform ballTransform;
        private float AImovementSpeed;

        private void Start()
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
            this.ballTransform = GameObject.FindGameObjectWithTag(GameTags.BALL).transform;
            if (GameManager.Instace.GetGameDifficult() == GameDifficult.NORMAL) this.AImovementSpeed = 10;
            if (GameManager.Instace.GetGameDifficult() == GameDifficult.HARD) this.AImovementSpeed = 15;
            if (GameManager.Instace.GetGameDifficult() == GameDifficult.UNFAIR) this.AImovementSpeed = 20;
        }

        private void Update()
        {
            if (GameManager.Instace.GetGameState() == GameState.STOPPED) return;
            
            if (this.controlledByPlayer)
            {
                this.MoveRacketByPlayer();
                return;
            }
            
            this.MoveRacketByAI();
        }

        /// <summary>
        /// Private method that handles the player inputs and the racket movement.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void MoveRacketByPlayer()
        {
            bool upKeyPressed = Input.GetKey(this.upKey);
            bool downKeyPressed = Input.GetKey(this.downKey);

            this.rigidbody.velocity = upKeyPressed ? 
                Vector3.forward * this.movementSpeed : 
                downKeyPressed ? 
                Vector3.back * this.movementSpeed : 
                Vector3.zero;
        }

        /// <summary>
        /// Private method that handles the player inputs and the racket movement.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void MoveRacketByAI()
        {
            this.rigidbody.velocity = 
                this.ballTransform.position.z > this.transform.position.z + this.correctionOffset ? 
                Vector3.forward * this.AImovementSpeed : 
                this.ballTransform.position.z < this.transform.position.z + this.correctionOffset ? 
                Vector3.back * this.AImovementSpeed : 
                Vector3.zero;
        }
    }
}