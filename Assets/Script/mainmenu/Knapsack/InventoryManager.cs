using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;

public class InventoryManager : MonoBehaviour 
{
	public static InventoryManager _instance;

	public TextAsset listinfo;
	public Dictionary<int, Inventory> inventoryDic = new Dictionary<int, Inventory>();
	//public Dictionary<int, InventoryItem> inventoryItemDict = new Dictionary<int, InventoryItem>();
	public List<InventoryItem> inventoryItemList = new List<InventoryItem>();

	public delegate void OnInventoryChangeEvent();
	public event OnInventoryChangeEvent OnInventoryChange;

	private InventoryItemDBController inventoryItemDBController;

	void Awake () {
		_instance = this;
		inventoryItemDBController = this.GetComponent<InventoryItemDBController>();
		inventoryItemDBController.OnGetInventoryItemDBList += this.OnGetInventoryItemDBList;
		inventoryItemDBController.OnAddInventoryItemDB += this.OnAddInventoryItemDB;
		ReadInventoryInfo();
	}

	void Start()
	{
		ReadInventoryItemInfo();
	}

	void Update()
	{
		PickUp();
	}


	/*ID 名称 图标 类型（Equip，Drug） 
		(Helm,Cloth,Weapon,Shoes,Necklace,Bracelet,Ring,Wing) 
		售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述*/
	void ReadInventoryInfo()
	{
		string str = listinfo.ToString();
		string[] itemStrArray = str.Split('\n');
		foreach(string itemStr in itemStrArray)
		{
			string[] proArray = itemStr.Split('|');
			Inventory inventory = new Inventory();
			inventory.ID = int.Parse(proArray[0]);
			inventory.Name = proArray[1];
			inventory.Icon = proArray[2];

			switch(proArray[3])
			{
				case "Equip":
					inventory.InventoryTYPE = InventoryType.Equip;
					break;
				case "Drug":
					inventory.InventoryTYPE = InventoryType.Drug;
					break;
				case "Box":
					inventory.InventoryTYPE = InventoryType.Box;
					break;
			}

			if(inventory.InventoryTYPE == InventoryType.Equip)
			{
				switch(proArray[4])
				{
					case "Helm":
						inventory.EquipTYPE = EquipType.Helm;
						break;
					case "Cloth":
						inventory.EquipTYPE = EquipType.Cloth;
						break;
					case "Weapon":
						inventory.EquipTYPE = EquipType.Weapon;
						break;
					case "Shoes":
						inventory.EquipTYPE = EquipType.Shoes;
						break;
					case "Necklace":
						inventory.EquipTYPE = EquipType.Necklace;
						break;
					case "Bracelet":
						inventory.EquipTYPE = EquipType.Bracelet;
						break;
					case "Ring":
						inventory.EquipTYPE = EquipType.Ring;
						break;
					case "Wing":
						inventory.EquipTYPE = EquipType.Wing;
						break;
				}
			}
			if(proArray[5] != "")
			{
				inventory.Price = int.Parse(proArray[5]);
			}
			else
			{
				inventory.Price = 0;
			}

			if(inventory.InventoryTYPE == InventoryType.Equip)
			{
				inventory.StarLevel = int.Parse(proArray[6]);
				inventory.Quality = int.Parse(proArray[7]);
				inventory.Damage = int.Parse(proArray[8]);
				inventory.Hp = int.Parse(proArray[9]);
				inventory.Power = int.Parse(proArray[10]);
			}

			if(inventory.InventoryTYPE == InventoryType.Drug)
			{
				switch(proArray[11])
				{
					case "Energy":
						inventory.InfoTYPE = InfoType.Energy;
						inventory.ApplyValue = int.Parse(proArray[12]);
						break;
					case "Exp":
						inventory.InfoTYPE = InfoType.Exp;
						inventory.ApplyValue = int.Parse(proArray[12]);
						break;
					case "Diamond":
						inventory.InfoTYPE = InfoType.Diamond;
						inventory.ApplyValue = int.Parse(proArray[12]);
						break;
					case "Coin":
						inventory.InfoTYPE = InfoType.Coin;
						inventory.ApplyValue = int.Parse(proArray[12]);
						break;
					case "Toughen":
						inventory.InfoTYPE = InfoType.Toughen;
						inventory.ApplyValue = int.Parse(proArray[12]);
						break;
				}
			}
			inventory.Des = proArray[13];
			inventoryDic.Add(inventory.ID, inventory);
		}
	}

	void ReadInventoryItemInfo()
	{
		//TODO   连接服务器获取拥有的物品信息

//		for(int i=0;i<20;i++)
//		{
//			int id = Random.Range(1001,1020);
//
//			Inventory inventory = null;
//			inventoryDic.TryGetValue(id, out inventory);
//			if(inventory.InventoryTYPE == InventoryType.Equip)
//			{
//				InventoryItem it = new InventoryItem();
//				it.Inventory = inventory;
//				it.Level = Random.Range(1,10);
//				it.Count = 1;
//				//inventoryItemDict.Add(id,it);
//				inventoryItemList.Add(it);
//			}
//			else
//			{
//				InventoryItem it = null;
//				//bool isExit = inventoryItemDict.TryGetValue(id, out it);
//				bool isExit = false;
//				foreach(InventoryItem temp in inventoryItemList)
//				{
//					if(temp.Inventory.ID == id)
//					{
//						isExit = true;
//						it = temp;
//						break;
//					}
//				}
//				if(isExit)
//				{
//					it.Count++;
//				}
//				else
//				{
//					it = new InventoryItem();
//					it.Inventory = inventory;
//					it.Level = 0;
//					it.Count = 1;
//				}
//				//inventoryItemDict.Add(id,it);
//				inventoryItemList.Add(it);
//			}
//		}
		inventoryItemDBController.GetInventoryItemDB();

	}

	void PickUp()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			int id = Random.Range(1001, 1020);
			Inventory i = null;
			inventoryDic.TryGetValue(id, out i);
			if(i.InventoryTYPE == InventoryType.Equip)
			{
//				InventoryItem it = new InventoryItem();
//				it.Inventory = i;
//				it.Level = Random.Range(1, 10);
//				it.Count = 1;
//				inventoryItemList.Add(it);
//				InventoryItemDB itemDB = it.CreateInventoryItemDB();

				InventoryItemDB itemDB = new InventoryItemDB();
				itemDB.InventoryID = id;
				itemDB.Level = Random.Range(1, 10);
				itemDB.Count = 1;
				itemDB.IsDressed = false;
				inventoryItemDBController.AddInventoryItemDB(itemDB);
			}
			else
			{
				InventoryItem it = null;
				//bool isExit = inventoryItemDict.TryGetValue(id, out it);
				bool isExit = false;
				foreach(InventoryItem temp in inventoryItemList)
				{
					if(temp.Inventory.ID == id)
					{
						isExit = true;
						it = temp;
						break;
					}
				}
				if(isExit)
				{
					it.Count++;
					inventoryItemDBController.UpdateInventoryItemDB(it.InventoryItemDB);
				}
				else
				{
					InventoryItemDB itemDB = new InventoryItemDB();
					itemDB.InventoryID = id;
					itemDB.Level = Random.Range(1, 10);
					itemDB.Count = 1;
					itemDB.IsDressed = false;
					inventoryItemDBController.AddInventoryItemDB(itemDB);
				}
			}
		}
	}

	public void OnAddInventoryItemDB(InventoryItemDB itemDB)
	{
		InventoryItem it = new InventoryItem(itemDB);
		inventoryItemList.Add(it);

		OnInventoryChange();
	}

	public void OnGetInventoryItemDBList(List<InventoryItemDB> list)
	{
		foreach(var itemDB in list)
		{
			InventoryItem it = new InventoryItem(itemDB);
			inventoryItemList.Add(it);
		}
		OnInventoryChange();
	}

	public void RemoveInventoryItem(InventoryItem it)
	{
		this.inventoryItemList.Remove(it);
	}
	void OnDestory()
	{
		if(inventoryItemDBController)
		{
			inventoryItemDBController.OnGetInventoryItemDBList -= this.OnGetInventoryItemDBList;
		}
	}
}


















