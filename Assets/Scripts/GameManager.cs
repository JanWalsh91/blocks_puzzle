using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject[] players;

	public bool gamePlaying;

	// Use this for initialization
	void Start () {
		gamePlaying = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (players[0].GetComponent<playerScript_ex01>().success &&
		    players[1].GetComponent<playerScript_ex01>().success &&
		    players[2].GetComponent<playerScript_ex01>().success) {
			Debug.Log("You win!");
			LoadNextScene();
		}

		if (Input.GetKeyDown(KeyCode.Tab)) {
			LoadNextScene();
		}

		if (gamePlaying == false) {
			Debug.Log("Sorry you lose!");
		}
		if (Input.GetKey(KeyCode.R)) {
			Reset();
		}
    }

	void LoadNextScene() {
		int index = SceneManager.GetActiveScene().buildIndex + 1;
		index = index >= SceneManager.sceneCountInBuildSettings ? 0 : index;
		SceneManager.LoadScene(index);
	}

	public void Reset() {
		players[0].GetComponent<playerScript_ex01>().Reset();
		players[1].GetComponent<playerScript_ex01>().Reset();
		players[2].GetComponent<playerScript_ex01>().Reset();
	}
}
