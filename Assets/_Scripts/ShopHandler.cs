using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;
using System.Collections;


public class ShopHandler : MonoBehaviour {

	[System.Serializable]
	public class Item{
		public string name;
		public Sprite image;
		public float cost;
		public float gain;
		public int count;
		
		//public Button.ButtonClickedEvent btnClicked;
		
	}
	
	public CubeHandler cubeHandler;
	
	public GameObject button;
	
	public GameObject[] buttonItems;
	public Item[] shopItems;
	
	int counter;

	void Start () {
	
		buttonItems = new GameObject[shopItems.Length];
		
		counter = 0;
		
		foreach(Item i in shopItems){
		
			GameObject btn = Instantiate (button) as GameObject;
			
			buttonItems[counter] = btn;
			
			ItemScript scp = btn.GetComponent<ItemScript>();
			scp.name.text = i.name;
			scp.cost.text = "Cost: " + i.cost.ToString("F0");
			scp.count.text = i.count.ToString();
			scp.gain.text = "CPS: " + i.gain.ToString("F0");
			scp.image.sprite = i.image;
			
			
			Item thisItem = i;
			
			scp.thisButton.onClick.AddListener( () => Purchase(thisItem) );
			
			//scp.thisButton.onClick = i.btnClicked;
			
			btn.transform.SetParent(this.transform, false);
			
			counter++;
		}
	
	}
	
	void Purchase(Item bought){
		
		
		cubeHandler.totalCubes -= bought.cost;
		bought.count++;
		bought.cost = bought.cost * 1.2f;
		
		
		
		
		UpdateItems();
		cubeHandler.CubesSecond();
	}
	
	
	void UpdateItems(){
		
		counter = 0;
		
		foreach(Item i in shopItems){
			
			
			ItemScript scp = buttonItems[counter].GetComponent<ItemScript>();
			
			scp.name.text = i.name;
			scp.cost.text = "Cost: " + i.cost.ToString("F0");
			scp.count.text = i.count.ToString();
			scp.gain.text = "CPS: " + i.gain.ToString("F0");
			scp.image.sprite = i.image;
			
			
			counter++;
		}
	}
	void Update(){
		
		counter = 0;
		foreach(Item i in shopItems){
			if(i.cost > cubeHandler.totalCubes){
			
				buttonItems[counter].GetComponent<Button>().interactable = false;
			}
			else{ 
				buttonItems[counter].GetComponent<Button>().interactable = true; 
			}
			
			counter++;
		}
	}




}
