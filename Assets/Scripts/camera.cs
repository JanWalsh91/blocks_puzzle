using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	public GameObject selectedPlayer;
	public GameObject[] players;

	// Use this for initialization
	void Start () {
		Physics2D.gravity = new Vector2(0f, -9f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) {
			updateSelectedPlayer(0);
		} else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) {
			updateSelectedPlayer(1);
		} else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) {
			updateSelectedPlayer(2);
		}
		gameObject.transform.position = new Vector3 (selectedPlayer.transform.position.x, Mathf.Max(selectedPlayer.transform.position.y, -5), -10);
	}

	void updateSelectedPlayer(int i) {
		selectedPlayer = players[i];
	}

}
