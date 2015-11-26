using UnityEngine;
using System.Collections;

public class Knapsack : MonoBehaviour {

	private EquipPopup equipPopup;
	private InventoryPopup inventoryPopup;

	void Awake()
	{
		equipPopup = transform.Find("EquipPopup").GetComponent<EquipPopup>();
		inventoryPopup = transform.Find("InventoryPopup").GetComponent<InventoryPopup>();
	}

	public void OnInventoryClick(object[] objectArray)
	{
		InventoryItem it = objectArray[0] as InventoryItem;
		bool isLeft = (bool)objectArray[1];
		InventoryItemUI itUI = objectArray[2] as InventoryItemUI;

		if(it.Inventory.InventoryTYPE == InventoryType.Equip)
		{
			equipPopup.Show(it, itUI, isLeft);
		}
		else
		{
			inventoryPopup.Show(it);
		}
	}
}
