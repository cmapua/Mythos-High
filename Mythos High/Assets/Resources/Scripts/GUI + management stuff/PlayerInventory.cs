using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
//You must include these namespaces
//to use BinaryFormatter
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Permissions;
 
public class PlayerInventory : MonoBehaviour {
	string[] nameArray ={
		"Sword",
		"Dagger",
		"Bow",
		"Staff",
		"Shield",
		"Boomerang"
	};
	string[] descriptionArray ={
		"Swords are sharp",
		"Daggers are also sharp",
		"Bows can fire from long away",
		"Staffs are made of wood",
		"Shields are made of iron",
		"Boomerangs don't fly back"
	};
	int[] priceArray ={
		50,
		30,
		60,
		40,
		100,
		1000
	};
 
	public static PlayerInventory current;
	
	void Awake(){
		if(current != null && current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            current = this;
        }
	}
	
		[SerializableAttribute]
    //Player Item
    public class PlayerItem
    {
        //Item name
         public string itemName;
        //Item Description
         public string description;
		//Item Count
	 	 public int itemCount;
		//Item Count
	 	 public int itemPrice;
		//Item Type
		 public string itemType;
		//Item Strength
		 public int itemStrength;
		
    }

	List<PlayerItem> playerItemList = new List<PlayerItem>();
	
	public void addItem(PlayerItem item, int itemCount){
		if (itemExists(item.itemName)){
			PlayerItem b = getItem (item.itemName);
			b.itemCount = b.itemCount + itemCount;
		} else{
			item.itemCount = itemCount;
			playerItemList.Add(item);
		}
		if(getItem (item.itemName).itemCount<0){
			getItem (item.itemName).itemCount=0;
		}
	}
	
	public PlayerItem getItem(String itemName){
		foreach(PlayerItem b in playerItemList){
			if(b.itemName == itemName){
				return b;
			}
		}
		return null;
	}
	
	public bool itemExists(String itemName){
		foreach(PlayerItem b in playerItemList){
			if (b.itemName == itemName){
				return true;
			}
		}
		return false;
	}
	
	public List<PlayerItem> getItemList(){
		foreach(PlayerItem b in playerItemList){
			print(b.itemName + b.description + b.itemPrice + b.itemCount);
		}
		return playerItemList;
		
	}
	
	public int getItemCount(String itemN){
		if(itemExists(itemN)){
			return getItem (itemN).itemCount;
		}
		else return 0;
	}
	
	public int getItemPrice(String itemN){
		if(itemExists(itemN)){
			return getItem (itemN).itemPrice;
		}
		else return 0;
	}
	
	public String getItemDescription(String itemN){
		if(itemExists(itemN)){
			return getItem (itemN).description;
		}
		else return "This item does not have any description.";
	}
	
	public List<PlayerItem> getAllItems(){
		List<PlayerItem> moreThanOne = new List<PlayerItem>();
		foreach(PlayerItem item in playerItemList){
			if(item.itemCount>0){
				moreThanOne.Add(item);
				print (item.itemName + item.description + item.itemPrice + item.itemCount);
			}
		}
		return moreThanOne;
	}
	
	public void setItemCost(String itemN, int itemP){
		if(itemExists(itemN)){
			PlayerItem b = getItem (itemN);
			b.itemPrice = itemP;
		} else{
			addItem (new PlayerItem{itemName = itemN, itemPrice = itemP}, 0);
		}
	}
	
	public void setItemCount(String itemN, int itemC){
		if(itemExists(itemN)){
			PlayerItem b = getItem (itemN);
			b.itemCount = itemC;
		} else{
			addItem (new PlayerItem{itemName = itemN}, itemC);
		}
	}
	
	public void setItemDescription(String itemN, String itemD){
		if(itemExists(itemN)){
			PlayerItem b = getItem (itemN);
			b.description = itemD;
		} else{
			addItem (new PlayerItem{itemName = itemN, description = itemD}, 0);
		}
	}
	
	public void SaveData()
    {
        //Get a binary formatter
        var b = new BinaryFormatter();
        //Create an in memory stream
        var m = new MemoryStream();
        //Save the scores
        b.Serialize(m, playerItemList);
        //Add it to player prefs
        PlayerPrefs.SetString("PInventory", 
            Convert.ToBase64String(
                m.GetBuffer()
            )
        );
    }
 
    void Start()
    {
        //Get the data
        var data = PlayerPrefs.GetString("PInventory");
        //If not blank then load it
        if(!string.IsNullOrEmpty(data))
        {
            //Binary formatter for loading back
            var b = new BinaryFormatter();
            //Create a memory stream with the data
            var m = new MemoryStream(Convert.FromBase64String(data));
            //Load back the scores
            playerItemList = (List<PlayerItem>)b.Deserialize(m);
			SaveData ();
		}
		if (playerItemList.Count<nameArray.Length){
			addAllItems();
		}
    }
 
	void addAllItems(){
		playerItemList.Clear();
		for(int i = 0; i< nameArray.Length; i++){
			addItem (new PlayerItem{itemName = nameArray[i], itemPrice = priceArray[i], itemCount = 0, description = descriptionArray[i]},0);
		}
	}
}