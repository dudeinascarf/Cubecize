using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CubeHandler : MonoBehaviour {

	public float totalCubes;
	public float cps;
	public Text totalCubeText;
	public Text cpsText;
	
	private int itemCount;
	
	public bool dependOnItems;
	public bool upgradedTouch;
	private float tapValue;
	
	//public Button buySomething;
	
	public float updateTime;			//Interval to cubes to update
	private float counter;
	
	public bool minimumUpdateTime;
	
	public ShopHandler shopHandler;
	
	
	

	void Awake(){
		
		//PlayerPrefs.DeleteAll();
		LoadPrefs();
		CubesSecond();
	}
	

	void Update () {
	
		//CubesSecond();
	
		/*if(totalCubes < 5)
			buySomething.interactable = false;
		
		else
			buySomething.interactable = true;
		*/	
		
	
		if(minimumUpdateTime)
			updateTime = Time.deltaTime;
			
	
		cpsText.text = cps + " Cubes/Sec";
		totalCubeText.text = totalCubes.ToString("F0") + " Cubes";
		
		if(counter < updateTime)
			counter += Time.deltaTime;
		else{
			totalCubes += cps * updateTime;
			PlayerPrefs.SetFloat("Score", totalCubes);
			
			counter = 0;
		}
	}
	
	public float ScreenTapped(){
		
		tapValue = 1;
		
		if(dependOnItems)
			tapValue = itemCount * .4f;
			
		if(upgradedTouch){
			tapValue = tapValue * 4;
				
		}
		
		totalCubes += tapValue;
		PlayerPrefs.SetFloat("Score", totalCubes);
		
		return tapValue;
	}
	
	public void LoadPrefs(){
		
		totalCubes = PlayerPrefs.GetFloat("Score");
		foreach(ShopHandler.Item item in shopHandler.shopItems){
			
			item.cost = PlayerPrefs.GetFloat(item.name + "Cost", item.cost);
			item.count = PlayerPrefs.GetInt(item.name + "Count");		
		}
		
	}
	
	/*public void moreCubes(){
		
		totalCubes += 200;
		cps += 10;
	}*/
	
	public void CubesSecond(){
		
		cps = 0;
		itemCount = 0;
		
		foreach(ShopHandler.Item item in shopHandler.shopItems){
			
			
			itemCount += item.count;
			cps = cps + item.count * item.gain;
			
			
			PlayerPrefs.SetInt(item.name + "Count", item.count);
			PlayerPrefs.SetFloat(item.name + "Cost", item.cost);
		}
	}
	
}
