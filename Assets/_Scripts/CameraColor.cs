using UnityEngine;
using System.Collections;

public class CameraColor : MonoBehaviour {

	public Color color1 = Color.red;
	public Color color2 = Color.blue;
	public float duration = 3.0f;
	
	Camera camera;
	
	void Awake() {
		camera = GetComponent<Camera>();
		camera.clearFlags = CameraClearFlags.SolidColor;
		
	}
	
	void Update() {
		float t = Mathf.PingPong(Time.time, duration) / duration;
		camera.backgroundColor = Color.Lerp(color1, color2, t);
		
	}
}
