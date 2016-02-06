using UnityEngine;
using System.Collections;
using TaidouCommon.Model;

public class InventoryItem {

	private Inventory inventory;
	private int level;
	private int count;
	private bool isDressed = false;
	private InventoryItemDB inventoryItemDB;

	public InventoryItem()
	{}

	public InventoryItem(InventoryItemDB itemDB)
	{
		this.inventoryItemDB = itemDB;
		Inventory inventoryTemp;
		InventoryManager._instance.inventoryDic.TryGetValue(itemDB.InventoryID, out inventoryTemp);
		inventory = inventoryTemp;
		Level = itemDB.Level;
		Count = itemDB.Count;
		IsDressed = itemDB.IsDressed;
	}

	public InventoryItemDB CreateInventoryItemDB()
	{
		inventoryItemDB = new InventoryItemDB();
		inventoryItemDB.Count = Count;
		inventoryItemDB.Level = Level;
		inventoryItemDB.InventoryID = Inventory.ID;
		inventoryItemDB.IsDressed = IsDressed;
		return inventoryItemDB;
	}

	public InventoryItemDB InventoryItemDB
	{
		get{return inventoryItemDB;}
	}

	public Inventory Inventory
	{
		get{return inventory;}
		set{inventory = value;}
	}
	public int Level
	{
		get{return level;}
		set{level = value;}
	}
	public int Count
	{
		get{return count;}
		set{count = value;
			inventoryItemDB.Count = value;
		}
	}

	public bool IsDressed
	{
		get{return isDressed;}
		set{isDressed = value;}
	}
}
