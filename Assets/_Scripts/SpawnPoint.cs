using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public GameObject backCube;

	void Update () {
	
		//SpawnCube();
	
	}
	
	public void SpawnCube(){
		
		Vector3 position = new Vector3(Random.Range(-6.0f, 1.0f), 3.5f, 4.0f);
		Instantiate(backCube, position, Quaternion.identity);
	}
}
