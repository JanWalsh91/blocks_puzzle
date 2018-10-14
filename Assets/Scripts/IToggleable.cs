using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IToggleable  {
	void Toggle(bool value, string method, string layerName);
}
