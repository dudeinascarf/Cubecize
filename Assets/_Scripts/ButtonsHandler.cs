using UnityEngine;
using System.Collections;

public class ButtonsHandler : MonoBehaviour {


	public Animator shopAnim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ShopControl(){
		
		if(shopAnim.GetBool ("isActive")){
			shopAnim.SetBool("isActive", false);
		}
		else{
			shopAnim.SetBool("isActive", true);
		}
	}
}
