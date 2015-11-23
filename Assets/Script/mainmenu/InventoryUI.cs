using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour 
{
	public List<InventoryItemUI> itemList = new List<InventoryItemUI>();

	void Awake()
	{
		InventoryManager._instance.OnInventoryChange += this.OnInventoryChange;
	}

	void OnDestory()
	{
		InventoryManager._instance.OnInventoryChange -= this.OnInventoryChange;
	}

	void OnInventoryChange()
	{
		UpdateShow();
	}

	void UpdateShow()
	{
		for(int i=0;i<InventoryManager._instance.inventoryItemList.Count;i++)
		{
			InventoryItem it = InventoryManager._instance.inventoryItemList[i];
			itemList[i].SetInventoryItem(it);
		}
		for(int i=InventoryManager._instance.inventoryItemList.Count;i<itemList.Count;i++)
		{
			itemList[i].Clear();
		}
	}
}
