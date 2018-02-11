using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStats : MonoBehaviour {

    private int health = 5;  // basically a health pool
    private int sprayCounter = 0; // has to get to 60 to put ding health
    private bool tookDamageThisFrame = false;

    public void Sprayed() {
        // if (tookDamageThisFrame) { return; }  // Do nothing

        // tookDamageThisFrame = true;

        sprayCounter += 1;

        if (sprayCounter == 7) {
            sprayCounter = 0;
            health -= 1;

            if (health > 0) {

                Vector3 currentScale = transform.localScale;
                // Vector3 currentPosition = transform.position;

                transform.localScale = new Vector3(currentScale.x / 2, currentScale.y, currentScale.z);
            } else { 
                Destroy(gameObject);
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // tookDamageThisFrame = false;  // reset	
	}
}
