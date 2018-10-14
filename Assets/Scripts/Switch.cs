using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

	public string layer;

	public GameObject[] toggleables;
	public bool toggled;

	// Use this for initialization
	void Start () {
		updateLayerAndColor(layer);
		toggled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!toggled)
			transform.localPosition = new Vector3(0.629f, 0.62f, 0f);
		else
			transform.localPosition = new Vector3(0.629f, 0.30f, 0f);
	}

	void updateLayerAndColor(string layerName) {
		gameObject.layer = LayerMask.NameToLayer(layerName);
		gameObject.GetComponent<Renderer>().material.color = ColorLayerInfo.GetColorByLayerName(layerName);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			string playerColor = ColorLayerInfo.GetPlayerColor(LayerMask.LayerToName(collision.gameObject.layer));
			if (!layer.Equals("Default") && !playerColor.Equals(layer)) return;
			if (toggleables.Length > 0) {
				foreach (GameObject toggleable in toggleables) {
					toggleable.GetComponent<IToggleable>().Toggle(true, "Enter",  playerColor);
				}
			}
			toggled = true;
			updateLayerAndColor(playerColor);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			string playerColor = ColorLayerInfo.GetPlayerColor(LayerMask.LayerToName(collision.gameObject.layer));
			if (!layer.Equals("Default") && !playerColor.Equals(layer)) return;
			if (toggleables.Length > 0) {
				foreach (GameObject toggleable in toggleables) {
					toggleable.GetComponent<IToggleable>().Toggle(false, "Exit", playerColor);
				}
			}
			toggled = false;
			updateLayerAndColor("Default");
		}
	}
}
