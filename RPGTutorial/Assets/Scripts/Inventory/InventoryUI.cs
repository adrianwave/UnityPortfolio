using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
	public RectTransform inventoryPanel;
	public RectTransform scrollViewContent;
	public ScrollRect scrollRect;
	InventoryUIItem itemContainer { get; set; }
	bool menuIsActive { get; set; }
	Item currentSelectedItem {get; set; }

	void Start() {
		itemContainer = Resources.Load<InventoryUIItem>("UI/Item_Container");
		UIEventHandler.OnItemAddedToInventory += ItemAdded;
		inventoryPanel.gameObject.SetActive(false);

	}

	public void ItemAdded(Item item)
	{
		InventoryUIItem emptyItem = Instantiate(itemContainer);
		emptyItem.SetItem(item);
		emptyItem.transform.SetParent(scrollViewContent);

	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.I)) {
			menuIsActive = !menuIsActive;
			inventoryPanel.gameObject.SetActive(menuIsActive);
		}
		//scrollRect.verticalNormalizedPosition = 0f;
		//scrollRect.horizontalNormalizedPosition = 0f;
	}
}
