using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public GameObject[] fullHearts;
    public GameObject[] emptyHearts;
    
    public int health = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.tag == "Extinguish") {
            health -= 1;
            if (health <= 0) {
                //Debug.Log("YOU DEAD");
                LevelManager.PlayerDied();
                Destroy(gameObject);
            } else {
                fullHearts[health].GetComponent<Renderer>().enabled = false;
            }
        } else if (collisionObject.tag == "Health") {
            fullHearts[health].GetComponent<Renderer>().enabled = true;
            health++;
            Destroy(collisionObject);
        } else if (collisionObject.tag == "EndGoal") {
            //Debug.Log("WON!");
            LevelManager.StageClear();
            Destroy(gameObject);
        }
    }
}
