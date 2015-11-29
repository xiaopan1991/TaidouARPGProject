using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour 
{
	public static InventoryUI _instance;
	public List<InventoryItemUI> itemList = new List<InventoryItemUI>();

	void Awake()
	{
		_instance = this;
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
	public void AddInventoryItem(InventoryItem it)
	{
		foreach(InventoryItemUI itUI in itemList)
		{
			if(itUI.it == null)
			{
				itUI.SetInventoryItem(it);
				break;
			}
		}
	}
}









