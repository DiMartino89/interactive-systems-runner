using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {

	// Function to switch Vector3 to Vector2 coordinates
	public static Vector2 toVector2(this Vector3 vec3) {
		return new Vector2(vec3.x, vec3.y);
	}
}
