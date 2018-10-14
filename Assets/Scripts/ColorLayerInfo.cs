using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColorLayerInfo {
	
	public enum Layers { Default, Red, Yellow, Blue };
	public static string[] layerNames = { "Default", "Red", "Yellow", "Blue" };
	public static Color[] colors = {
		new Color( 1f, 1f, 1f ),
		new Color( 0.84f, 0.27f, 0.26f ),
		new Color( 0.7f, 0.61f, 0.22f ),
		new Color( 0.145f, 0.24f, 0.37f )
	};

	public static Color GetColorByLayerName(string layerName) {
		for (int i = 0; i < layerNames.Length; i++) {
			if (layerNames[i].Equals(layerName)) {
				return colors[i];
			}
		}
		return colors[0];
	}

	public static string GetPlayerColor(string layerName) {
		if (layerName.Equals("RedPlayer")){
			return "Red";
		} else if (layerName.Equals("YellowPlayer")){
			return "Yellow";
		} else if (layerName.Equals("BluePlayer")){
			return "Blue";
		} else {
			return "Default";
		}
	}
}
