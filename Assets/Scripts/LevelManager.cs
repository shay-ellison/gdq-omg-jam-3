using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKey(KeyCode.Escape)) {
            // switch to windowed
            Screen.fullScreen = false;
        }

        if (Input.GetKey(KeyCode.F)) {
            // off of windowed
            Screen.fullScreen = true;
        }
    }
}
