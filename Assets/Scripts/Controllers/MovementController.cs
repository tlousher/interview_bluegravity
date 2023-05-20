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
        private readonly List<Vector2> _directions = new List<Vector2>();
        private Rigidbody2D _rigidbody;
    
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
            var direction = character.Direction;
            var addedDirection = true;

            // Control if a key has been pressed.
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                direction = Vector2.down;
            }
            else addedDirection = false;

            // Control if a key has been released.
            if (Input.GetKeyUp(KeyCode.A))
            {
                _directions.Remove(Vector2.left);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _directions.Remove(Vector2.right);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                _directions.Remove(Vector2.up);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                _directions.Remove(Vector2.down);
            }
            else if (!addedDirection) return;

            if (addedDirection)
            {
                _directions.Add(direction);
            }
            // Set the direction to the character.
            character.SetDirection(_directions.Count > 0 ? _directions.Last() : direction);
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