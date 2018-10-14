using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , IToggleable {

	public string color;

	public void Toggle(bool value, string method, string layerName) {
		if (value && layerName.Equals(color)) {
			transform.localEulerAngles = new Vector3(0, 0, 90);
		} else {
			transform.localEulerAngles = new Vector3(0, 0, 0);
		}
	}
}
