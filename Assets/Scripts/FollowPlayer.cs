using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public Vector2 velocity;
    public GameObject player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {
        if (player != null) {
            Vector3 currentPosition = transform.position;
            Vector2 playerPosition = player.transform.position;

            float newX = Mathf.SmoothDamp(currentPosition.x, playerPosition.x, ref velocity.x, 0.2f);
            float newY = Mathf.SmoothDamp(currentPosition.y, playerPosition.y, ref velocity.y, 0.2f);

            transform.position = new Vector3(newX, newY, currentPosition.z);
        }
    }
}
