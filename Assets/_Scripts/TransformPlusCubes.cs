using UnityEngine;
using System.Collections;

public class TransformPlusCubes : MonoBehaviour {

	public float speed;
	public Vector3 direction = Vector3.up;
	
	// Update is called once per frame
	void Update () {
	
		transform.position += direction * speed;
	}
}
