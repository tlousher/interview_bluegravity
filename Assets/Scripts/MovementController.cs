using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Character4D character;
    public bool initDirection;
    public int movementSpeed;

    private bool _moving;
    private readonly List<Vector2> _directions = new List<Vector2>();
    private Rigidbody2D _rigidbody;

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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else addedDirection = false;

        // Control if a key has been released.
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _directions.Remove(Vector2.left);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _directions.Remove(Vector2.right);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _directions.Remove(Vector2.up);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.DownArrow))
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