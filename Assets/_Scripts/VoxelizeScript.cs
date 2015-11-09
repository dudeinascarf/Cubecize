using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class VoxelizeScript : MonoBehaviour {


	public enum State {Active, Ready, Charging};
	
	public Color A = Color.magenta;
	public Color B = Color.blue;
	public float speed = 2.0f;
	
	public Image cube;
	
	public TouchScript ts;
	public CameraColor cc;
	
	public float activeTime;
	public float chargingTime = 2f;
	
	private float counter;
	
	public State status;
	
	GameObject mainCam;
	
	public Text text;

	void Start () {
	
		mainCam = GameObject.FindWithTag("MainCamera");
	
		status = State.Ready;
		cube.color = Color.Lerp(A, B, Mathf.PingPong(Time.time, speed) / speed);
		
		text.text = "PRESS TO VOXELIZE";
		
		if(PlayerPrefs.GetFloat("UpgradeCounter") < 0){
			status = State.Charging;
			counter = 0;
			//counter = PlayerPrefs.GetFloat("UpgradeCounter");
		}
		else if(PlayerPrefs.GetFloat("UpgradeCounter") > 0){
			counter = PlayerPrefs.GetFloat("UpgradeCounter");
		}
	
	}
	

	void Update () {
	
		if(status == State.Active){
			
			counter += Time.deltaTime;
			
			cube.fillAmount = 1 - counter / activeTime;
			cc.duration = 1.0f;
			cc.color1 = Color.red;
			cc.color2 = Color.blue;
			
			mainCam.GetComponentInParent<AudioSource>().pitch = 1.5f;
			
			text.text = "TAP WIH ALL FINGERS";
			
			
			PlayerPrefs.SetFloat("UpgradeCounter", -counter);
			
			if(counter >= activeTime){
				status = State.Charging;
				counter = 0;
				ts.maxFingers--;
			}
		}
		
		if(status == State.Ready){
		
			counter = 0;
			cube.fillAmount = 1;
			cube.color = Color.Lerp(A, B, Mathf.PingPong(Time.time, speed) / speed);
			
			text.text = "PRESS TO VOXELIZE";
		}
		
		if(status == State.Charging){
		
			cube.color = Color.Lerp(A, B, Mathf.PingPong(Time.time, speed) / speed);

			counter += Time.deltaTime;
			
			cc.duration = 3.0f;
			cc.color1 = new Color(0.894f, 1.000f, 0.773f, 1.000f);
			cc.color2 = new Color(0.000f, 1.000f, 1.000f, 1.000f);
			mainCam.GetComponentInParent<AudioSource>().pitch = 1.0f;
			
			text.text = "WATCH AD AND RESTORE";
			
			
			
			PlayerPrefs.SetFloat("UpgradeCounter", counter);
			
			cube.fillAmount = counter / chargingTime;
			
			if(counter >= chargingTime){
			
				PlayerPrefs.SetFloat("UpgradeCounter", 0);
				status = State.Ready;
			}
		} 
	
	}
	
	public void Pressed(){
		
		if(status == State.Ready){
			
			status = State.Active;
			ts.maxFingers++;
		}
		
		if(status == State.Charging){
			
			if(Advertisement.IsReady("rewardedVideo")){
				var options = new ShowOptions { resultCallback = HandleShowResult };
				Advertisement.Show ("rewardedVideo", options);
			}
		}	
		
	}
	
	public void HandleShowResult(ShowResult result){
		
		switch(result){
		
			case ShowResult.Finished:
				Debug.Log ("The ad was successfully shown.");
				status = State.Ready;
				break;
			case ShowResult.Skipped:
				Debug.Log ("The ad was skipped before reaching the end.");
				break;
			case ShowResult.Failed:
				Debug.Log ("The ad failed to be shown.");
				break;
		}
	}
}