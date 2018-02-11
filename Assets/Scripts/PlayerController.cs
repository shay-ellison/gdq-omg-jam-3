using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {
    public Rigidbody2D rigidBody;
    public DrawWaterStream drawWaterStream;

    private float strongSprayLength = 2.0f;
    private float weakSprayLength = 5.0f;

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
        if (rigidBody == null) {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        if (drawWaterStream == null) {
            drawWaterStream = GetComponent<DrawWaterStream>();
        }
	}

    // Update is called once per frame
    void FixedUpdate () {
        Direction inputDirection = WhichInputDirection();

        if (inputDirection != Direction.None) {
            // Need to see if something is within reach in direction
            Vector2 directionVector = DirectionToDirectionVector(inputDirection);

            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directionVector, strongSprayLength, 256);  // only looking at Sprayable layer (8 - 9th layer)

            drawWaterStream.StartPosition(transform.position);

            if (raycastHit.collider != null) {
                Debug.Log("HIT STRONG!");
                drawWaterStream.EndPosition(raycastHit.point);

                // We want to fire the player in the opposite direction
                Direction oppositeDirection = TheOppositeDirection(inputDirection);
                Vector2 oppositeForce = DirectionalForce(oppositeDirection, true);
                rigidBody.AddRelativeForce(oppositeForce, ForceMode2D.Force);
            }
            else {
                // Look longer for weak spray
                raycastHit = Physics2D.Raycast(transform.position, directionVector, weakSprayLength, 256);

                if (raycastHit.collider != null) {
                    drawWaterStream.EndPosition(raycastHit.point);
                    Debug.Log("HIT WEAK!");
                    // We want to fire the player in the opposite direction
                    Direction oppositeDirection = TheOppositeDirection(inputDirection);
                    Vector2 oppositeForce = DirectionalForce(oppositeDirection, false);
                    rigidBody.AddRelativeForce(oppositeForce, ForceMode2D.Force);
                }
                else {
                    // Need to go up to the weak spray length since we didn't hit anything
                    int degrees = (int)inputDirection;
                    float rads = degrees * Mathf.Deg2Rad;
                    float x = Mathf.Cos(rads);
                    float y = Mathf.Sin(rads);

                    // lineRenderer.SetPosition(1, transform.position + (new Vector3(directionVector.x, directionVector.y) * weakSprayLength));
                    Vector3 reachVector = transform.position + new Vector3(x, y) * weakSprayLength;
                    drawWaterStream.EndPosition(reachVector);
                }
            }
        } else {

        }

        // TODO: Make this much fancier!!!
        if (rigidBody.velocity.magnitude > 14.0f) {  // AKA top speed
            rigidBody.velocity = rigidBody.velocity.normalized * 14.0f;  // normalized is basing it on 1?
        }
	}

    Vector2 DirectionToDirectionVector(Direction direction) {
        Vector2 directionVector = Vector2.zero;
        switch (direction) {
            case Direction.Down:
                directionVector = Vector2.down;
                break;
            case Direction.DownLeft:
                directionVector = new Vector2(-0.5f, -1.0f);
                break;
            case Direction.DownRight:
                directionVector = new Vector2(0.5f, -1.0f);
                break;
            case Direction.Left:
                directionVector = Vector2.left;
                break;
            case Direction.Right:
                directionVector = Vector2.right;
                break;
            case Direction.Up:
                directionVector = Vector2.up;
                break;
            case Direction.UpLeft:
                directionVector = new Vector2(-0.5f, 1.0f);
                break;
            case Direction.UpRight:
                directionVector = new Vector2(0.5f, 1.0f);
                break;
            case Direction.None:
                directionVector = Vector2.zero;
                break;
        }
        return directionVector;
    }

    Vector2 DirectionalForce(Direction direction, bool strong) {
        if (direction == Direction.None) { return new Vector2(0f, 0f); }
        int directionAngle = (int)direction;

        float xPower = 100.0f;
        float yPower = 100.0f;

        if (!strong) {
            xPower = 60.0f;
            yPower = 60.0f;
        }

        float x = 0.0f;
        float y = 0.0f;

        if (directionAngle > 90 && directionAngle < 270) {  // -x side
            x = -xPower;
        } else if ((directionAngle < 90 && directionAngle >= 0) || (directionAngle > 270 && directionAngle < 360)) { // +x side
            x = xPower;
        }

        if (directionAngle > 0 && directionAngle < 180) {
            y = yPower;
        } else if (directionAngle > 180 && directionAngle < 360) {
            y = -yPower;
        }

        Vector2 force = new Vector2(x, y);
        return force;
    }

    Direction WhichInputDirection() {
        Direction inputDirection = Direction.None;

        // This will change depending on Controller vs. Keyboard
        bool leftIsDown = Input.GetKey(KeyCode.LeftArrow);
        bool downIsDown = Input.GetKey(KeyCode.DownArrow);
        bool rightIsDown = Input.GetKey(KeyCode.RightArrow);
        bool upIsDown = Input.GetKey(KeyCode.UpArrow);

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
