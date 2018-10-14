using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	public enum teleporterTypes {IN, OUT};
	public teleporterTypes teleporterType;
	public GameObject link;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			collision.gameObject.transform.position = link.transform.position;
		}
	}
}
