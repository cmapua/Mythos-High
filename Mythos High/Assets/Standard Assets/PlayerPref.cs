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
 
public class PlayerPref : MonoBehaviour {
 
	public static PlayerPref current;
	
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
    //Player Data
    public class PlayerData
    {
        //Players name
         public string name;
        //Ability
         public bool a1;
         public bool a2;
         public bool a3;
		//Player gender
	 	 public int gender;
		//Level Progress
		 public bool lvl1;
		 public bool lvl2;
		 public bool lvl3;
		 public bool lvl4;
		 public bool lvl5;
    }	
	
	public List<PlayerData> playerDataList = new List<PlayerData>();
	public PlayerData PData = new PlayerData();
	
	public void setName(String n){
		PData.name = n;
		SaveData();
	}
	public void setGender(int g){
		PData.gender = g;
		SaveData();
	}	
	
	public void SaveData()
    {
		playerDataList.Clear();
		playerDataList.Add(PData);
        //Get a binary formatter
        var b = new BinaryFormatter();
        //Create an in memory stream
        var m = new MemoryStream();
        //Save the scores
        b.Serialize(m, playerDataList);
        //Add it to player prefs
        PlayerPrefs.SetString("PDat", 
            Convert.ToBase64String(
                m.GetBuffer()
            )
        );
		print ("Data Saved.");
    }
 
    void Start()
    {
        //Get the data
        var data = PlayerPrefs.GetString("PDat");
        //If not blank then load it
        if(!string.IsNullOrEmpty(data))
        {
            //Binary formatter for loading back
            var b = new BinaryFormatter();
            //Create a memory stream with the data
            var m = new MemoryStream(Convert.FromBase64String(data));
            //Load back the scores
            playerDataList = (List<PlayerData>)b.Deserialize(m);
			PData = playerDataList.First();
			print (PData.name);
        } else{
			SaveData();
		}
    }
 
}