
//Touch detection script
//Goes on cube gameObject or Main Camera

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchScript : MonoBehaviour {


	public CubeHandler ch;
	public SpawnPoint sp;
	
	public int maxFingers;		//max fingers that will be detected at certain time
	private int fingerCount;		//ammount of fingers on screen in certain time
	
	public bool screenPressed;
	
	public GameObject explosion;	//Explosion particleSystem prefab
	
	public GameObject plusCubes;
	
	private Vector3 worldPos;		//Contains x,y,z position		
	
	private AudioSource source;
	public AudioClip tapSound;


	void Awake () {
	
		source = GetComponent<AudioSource>();
	
	}
	
	
	void Update () {
		
		if(screenPressed){
			fingerCount = 0;
			
			foreach(Touch touch in Input.touches){
				if(touch.phase == TouchPhase.Began && fingerCount < maxFingers)	//Detecting touches if fingerCount is smaller than maxFingers
				{
				
					this.GetComponent<Animator>().SetBool ("Tapped", true);		//play cube animation on finger tap
					//Debug.Log (touching);
				
					Vector3 fingerPos = Input.GetTouch(fingerCount).position;	
					fingerPos.z = 3;
				
					worldPos = Camera.main.ScreenToWorldPoint(fingerPos);	//Finds Main Camera and colculates screen pos to world pos acording to Main Camera
					Instantiate(explosion, worldPos, Quaternion.identity);	//Instantiating prefab on touch
				
					GameObject pCubes = (GameObject)Instantiate(plusCubes, worldPos, Quaternion.identity);
					pCubes.GetComponentInChildren<Text>().text = "+" + ch.ScreenTapped();
					
					sp.SpawnCube();
					PlaySound();
					
				}
			 
				fingerCount++;
				screenPressed = false;
			}
		}
	}
	
	public void TappedFalse(){
		
		this.GetComponent<Animator>().SetBool ("Tapped", false);
	}
	
	public void CheckFingers(){
		
		screenPressed = true;
	}
	
	void PlaySound(){
		
		source.PlayOneShot(tapSound, 1f);
	}
	
}
