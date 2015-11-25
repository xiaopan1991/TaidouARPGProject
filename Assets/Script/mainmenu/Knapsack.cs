using UnityEngine;
using System.Collections;

public class Knapsack : MonoBehaviour {

	private EquipPopup equipPopup;

	void Awake()
	{
		equipPopup = transform.Find("EquipPopup").GetComponent<EquipPopup>();
	}

	public void OnInventoryClick(object[] objectArray)
	{
		InventoryItem it = objectArray[0] as InventoryItem;
		bool isLeft = (bool)objectArray[1];

		equipPopup.Show(it, isLeft);
	}
}
