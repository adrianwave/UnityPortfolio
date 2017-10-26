using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour {

	public static ItemDatabase instance;
	private List<Item> items { get; set; }
	// Use this for initialization
	void Awake () {
		if(instance != null && instance != this) {


		} else {
		
			instance = this;
			BuildDatabase();
		}
	}
	
	private void BuildDatabase()
	{
		items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
	}

	public Item getItem(string itemSlug)
	{
		foreach(Item item in items) {
		
			if(item.ObjectSlug == itemSlug) {
			
				return item;
			}
		}
		Debug.LogWarning("Couldn't find item:" + itemSlug);
		return null;
	}
}
