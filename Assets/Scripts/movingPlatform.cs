using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour, IToggleable {

	public Vector2 startPosition;
	public Vector2 endPosition;
	public float speed;
	float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time + 3;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		transform.localPosition = new Vector2(
			startPosition.x + ((Mathf.Cos(speed * (startTime - Time.time)) + 1) / 2) * (endPosition.x - startPosition.x),
			startPosition.y + ((Mathf.Cos(speed * (startTime - Time.time)) + 1) / 2) * (endPosition.y - startPosition.y)
		);
	}

	public void Toggle(bool value, string method, string layerName) {
		updateLayerAndColor(layerName);
	}

	void updateLayerAndColor(string layerName) {
		gameObject.layer = LayerMask.NameToLayer(layerName);
		gameObject.GetComponent<Renderer>().material.color = ColorLayerInfo.GetColorByLayerName(layerName);
	}
}
