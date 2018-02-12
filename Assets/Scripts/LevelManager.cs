using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    static private LevelManager instance = null;

    public GameObject stageClearText;
    public GameObject youDiedText;

    public static void PlayerDied()
    {
        instance.youDiedText.GetComponent<Text>().enabled = true;
    }

    public static void StageClear()
    {
        instance.stageClearText.GetComponent<Text>().enabled = true;
    }

    // Use this for initialization
    void Start () {
        stageClearText.GetComponent<Text>().enabled = false;
        youDiedText.GetComponent<Text>().enabled = false;

        if (LevelManager.instance == null) {
            LevelManager.instance = this;
        } else {
            Destroy(LevelManager.instance);
            LevelManager.instance = this;
        }
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
