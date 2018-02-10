using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rigidBody;

    /* Direction to send the water in */
    public enum Direction: int {  // Directon + Degrees
        DownRight=315,
        Down =270,
        DownLeft =225,
        Left =180,
        UpLeft =135,
        Up =90,
        UpRight =45,
        Right =0,
        None
    }

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
        Direction inputDirection = WhichInputDirection();

        //Debug.Log(inputDirection);

        if (inputDirection != Direction.None) {

            //Debug.Log("NOT NONE");

            // We want to fire the player in the opposite direction
            Direction oppositeDirection = TheOppositeDirection(inputDirection);
            Vector2 oppositeForce = DirectionalForce(oppositeDirection);

            //Debug.Log(oppositeForce);

            rigidBody.AddForce(oppositeForce);
        }
	}

    Vector2 DirectionalForce(Direction direction)
    {
        if (direction == Direction.None) { return new Vector2(0f, 0f); }
        int directionAngle = (int)direction;
        float scalar = 100.0f;

        float x = 0.0f;
        float y = 0.0f;

        if (directionAngle > 90 && directionAngle < 270) {  // -x side
            x = -scalar;
        } else if ((directionAngle < 90 && directionAngle >= 0) || (directionAngle > 270 && directionAngle < 360)) { // +x side
            x = scalar;
        }

        if (directionAngle > 0 && directionAngle < 180) {
            y = scalar;
        } else if (directionAngle > 180 && directionAngle < 360) {
            y = -scalar;
        }

        Vector2 force = new Vector2(x, y);
        return force;
    }

    Direction WhichInputDirection() {
        Direction inputDirection = Direction.None;

        // This will change depending on Controller vs. Keyboard
        bool leftIsDown = Input.GetKeyDown(KeyCode.LeftArrow);
        bool downIsDown = Input.GetKeyDown(KeyCode.DownArrow);
        bool rightIsDown = Input.GetKeyDown(KeyCode.RightArrow);
        bool upIsDown = Input.GetKeyDown(KeyCode.UpArrow);

        if (downIsDown) {  // NOTE: Down will trump Up
            Debug.Log("DOWN");

            if (rightIsDown) {
                inputDirection = Direction.DownRight;
            } else if (leftIsDown) {
                inputDirection = Direction.DownLeft;
            } else {
                inputDirection = Direction.Down;
            }
        } else if (upIsDown) {
            Debug.Log("UP");

            if (rightIsDown) {
                inputDirection = Direction.UpRight;
            } else if (leftIsDown) {
                inputDirection = Direction.UpLeft;
            } else {
                inputDirection = Direction.Up;
            }
        } else if (rightIsDown) {  // NOTE: Right will trump Left
            Debug.Log("RIGHT");
            inputDirection = Direction.Right;
        } else if (leftIsDown) {
            Debug.Log("LEFT");
            inputDirection = Direction.Left;
        }

        return inputDirection;
    }

    Direction TheOppositeDirection(Direction direction)
    {
        Direction oppositeDirection = Direction.None;
        switch (direction) {
            case Direction.DownRight:
                oppositeDirection = Direction.UpLeft;
                break;
            case Direction.Down:
                oppositeDirection = Direction.Up;
                break;
            case Direction.DownLeft:
                oppositeDirection = Direction.UpRight;
                break;
            case Direction.Left:
                oppositeDirection = Direction.Right;
                break;
            case Direction.UpLeft:
                oppositeDirection = Direction.DownRight;
                break;
            case Direction.Up:
                oppositeDirection = Direction.Down;
                break;
            case Direction.UpRight:
                oppositeDirection = Direction.DownLeft;
                break;
            case Direction.Right:
                oppositeDirection = Direction.Left;
                break;
            case Direction.None:  // exhaustive now, yay! :)
                oppositeDirection = Direction.None;
                break;
            
        }
        return oppositeDirection;
    }
}
