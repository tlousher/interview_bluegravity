using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;
using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour
    {
        public Character4D character;
        public bool initDirection;
        public int movementSpeed;

        private bool _moving;
        private List<Directions> _directions = new ();
        private Rigidbody2D _rigidbody;
        
        private enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }
    
        // List of the objects that blocks the movements of the character.
        public static List<string> CanMove = new List<string>();

        public void Start()
        {
            character.AnimationManager.SetState(CharacterState.Idle);
            _rigidbody = character.GetComponent<Rigidbody2D>();

            if (initDirection)
            {
                character.SetDirection(Vector2.down);
            }
        }

        public void Update()
        {
            if (CanMove.Count > 0) return;
            SetDirection();
            Move();
        }

        /// <summary>
        /// Assign the direction to the character based on the last key pressed.
        /// </summary>
        private void SetDirection()
        {
            // Control if a key has been pressed.
            if (Input.GetKeyDown(KeyCode.A))
            {
                _directions.Add(Directions.Left);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _directions.Add(Directions.Right);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _directions.Add(Directions.Up);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _directions.Add(Directions.Down);
            }

            // Control if a key has been released.
            if (Input.GetKeyUp(KeyCode.A))
            {
                _directions.RemoveAll(x => x == Directions.Left);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                _directions.RemoveAll(x => x == Directions.Right);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                _directions.RemoveAll(x => x == Directions.Up);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                _directions.RemoveAll(x => x == Directions.Down);
            }
            
            // Set the direction to the character.
            if (_directions.Count <= 0) return;
            var dir = _directions.Last();
            switch (dir)
            {
                case Directions.Up:
                    character.SetDirection(Vector2.up);
                    break;
                case Directions.Down:
                    character.SetDirection(Vector2.down);
                    break;
                case Directions.Left:
                    character.SetDirection(Vector2.left);
                    break;
                case Directions.Right:
                    character.SetDirection(Vector2.right);
                    break;
            }
        }

        /// <summary>
        /// Move the character in the direction of the last key pressed.
        /// </summary>
        private void Move()
        {
            if (movementSpeed == 0) return;

            var direction = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }

            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }

            if (direction == Vector2.zero)
            {
                if (!_moving) return;
                character.AnimationManager.SetState(CharacterState.Idle);
                _rigidbody.velocity = (Vector3) direction.normalized;
                _moving = false;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    character.AnimationManager.SetState(CharacterState.Run);
                    _rigidbody.velocity = movementSpeed * 1.5f * (Vector3) direction.normalized;
                    _moving = true;
                }
                else
                {
                    character.AnimationManager.SetState(CharacterState.Walk);
                    _rigidbody.velocity = movementSpeed * (Vector3) direction.normalized;
                    _moving = true;
                }
            }
        }
    }
}