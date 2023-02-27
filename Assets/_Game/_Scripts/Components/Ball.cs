using System;
using System.Collections;
using System.Collections.Generic;
using DallaiStudios.SuperPong.Enums;
using DallaiStudios.SuperPong.Events;
using DallaiStudios.SuperPong.Managers;
using DallaiStudios.SuperPong.Types;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DallaiStudios.SuperPong.Components
{
    /// <summary>
    /// Component that handles the ball movement.
    /// </summary>
    /// <requires>Rigidbody</requires>
    /// <author>Renan Souza</author>
    /// <version>1.0.0</version>
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [Header("Define the Ball parameters")]
        [SerializeField] private float ballDefaultSpeed = 10f;
        [SerializeField] private float ballSpeedIncrement = 0.2f;
        [SerializeField] private float minDirection = 0.5f;

        [Header("Define the Ball Hit Effect")] 
        [SerializeField] private ParticleSystem hitParticles;

        [Header("Define the Ball Hit Sound Effect")] 
        [SerializeField] private AudioClip hitSoundEffect;
        
        private Rigidbody rigidbody;
        private Vector3 direction;
        private float ballSpeed;
        private bool ballCanTravel = false;
        private Vector3 ballStartPosition;

        private void Start()
        {
            this.SetupComponent();
            this.SetRandomBallStartingDirection();
            GameManager.OnResetGame += this.ResetBallPosition;
            GameStateEvents.OnGameStateChange += this.DefineBallActivitie;
            this.ballCanTravel = GameManager.Instace.GetGameState() == GameState.PLAYING;
        }

        private void OnDisable()
        {
            GameManager.OnResetGame -= this.ResetBallPosition;
            GameStateEvents.OnGameStateChange -= this.DefineBallActivitie;
        }

        private void FixedUpdate()
        {
            if (!this.ballCanTravel) return;
            this.MoveBall();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.WALL))
            {
                this.DisplayHitEffect();
                this.PlayHitSound();
                this.InverseBallDirectionOnZ();
            }
            if (other.CompareTag(GameTags.RACKET))
            {
                this.DisplayHitEffect();
                this.PlayHitSound();
                this.InverseBallDirectionOnX(other);
                this.IncrementBallSpeed();
            }
        }

        /// <summary>
        /// Private event listener that defines if the ball can be active on the game.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void DefineBallActivitie(GameState newGameState)
        {
            this.ballCanTravel = newGameState == GameState.PLAYING;
        }
        
        /// <summary>
        /// Private method that setup all the necessary components and properties
        /// in order to make this component works.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void SetupComponent()
        {
            this.rigidbody = GetComponent<Rigidbody>();
            this.ballSpeed = this.ballDefaultSpeed;
            this.ballStartPosition = this.transform.position;
        }

        /// <summary>
        /// Private method that defines a random start position for the ball.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void SetRandomBallStartingDirection()
        {
            float xRandomValue = Mathf.Sign(Random.Range(-1f, 1f)) * 0.5f;
            float zRandomValue = Mathf.Sign(Random.Range(-1f, 1f)) * 0.5f;
            this.direction = new Vector3(xRandomValue, 0, zRandomValue);
        }

        /// <summary>
        /// Private method that makes the ball move.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void MoveBall()
        {
            this.rigidbody.MovePosition(this.rigidbody.position + this.direction * (this.ballSpeed * Time.fixedDeltaTime));
        }

        /// <summary>
        /// Private method that plays the ball hit particle effect.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void DisplayHitEffect()
        {
            this.hitParticles.Play();
        }

        /// <summary>
        /// Private method that plays the ball hit sound effect.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void PlayHitSound()
        {
            SoundEffectManager.Instance.Play(this.hitSoundEffect);
        }
        
        /// <summary>
        /// Private method that inverts the Z axis direction of the ball.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void InverseBallDirectionOnZ() => this.direction.z = this.direction.z * -1;

        /// <summary>
        /// Private method that inverts the X axis direction of the ball and corrects the
        /// angle of the ball to be more smooth.
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void InverseBallDirectionOnX(Collider collider)
        {
            Vector3 newDirection = (this.transform.position - collider.transform.position).normalized;
            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), this.minDirection);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(Mathf.Abs(newDirection.z), this.minDirection);
            this.direction = newDirection;
        }

        /// <summary>
        /// Private method that increments the ball speed;
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void IncrementBallSpeed()
        {
            this.ballSpeed += this.ballSpeedIncrement + GameManager.Instace.GetGameDifficult().Value();
        }

        /// <summary>
        /// Private method that returns the ball to the initial position;
        /// </summary>
        /// <author>Renan Souza</author>
        /// <version>1.0.0</version>
        private void ResetBallPosition()
        {
            this.transform.position = this.ballStartPosition;
            this.ballSpeed = this.ballDefaultSpeed;
            this.SetRandomBallStartingDirection();
        }
    }
}