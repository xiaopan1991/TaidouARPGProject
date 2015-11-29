using UnityEngine;
using System.Collections;

public enum InfoType
{
	Name,
	HeadPortrait,
	Level,
	Power,
	Exp,
	Diamond,
	Coin,
	Energy,
	Toughen,
	HP,
	Damage,
	Equip,
	All
}

public class PlayerInfo : MonoBehaviour {

	public static PlayerInfo _instance = null;

	#region unity event
	void Awake()
	{
		_instance = this;
	}
	void Start()
	{
		Init();
	}
	void Update()
	{
		if(this.Energy < 100)
		{
			energyTime += Time.deltaTime;
			if(energyTime > 60)
			{
				Energy += 1;
				this.energyTime -= 60;
				OnPlayerInfoChanged(InfoType.Energy);
			}
		}
		else
		{
			this.energyTime = 0f;
		}

		if(this.Toughen < 50)
		{
			this.toughenTime += Time.deltaTime;
			if(toughenTime > 60)
			{
				Toughen += 1;
				this.toughenTime -= 60;
				OnPlayerInfoChanged(InfoType.Toughen);
			}
		}
		else
		{
			this.toughenTime = 0f;
		}
	}
	#endregion

	#region property
	private string _name;
	private string _headPortrait;
	private int _level =1;
	private int _power = 1;
	private int _exp = 0;
	private int _diamond = 0;
	private int _coin = 0;

	private int _energy = 0;
	private int _toughen = 0;

	private int _hp;
	private int _damage;

	/*private int _helmID = 0;
	private int _clothID = 0;
	private int _weaponID = 0;
	private int _shoesID = 0;
	private int _necklaceID = 0;
	private int _braceleID = 0;
	private int _ringID = 0;
	private int _wingID = 0;*/

	public InventoryItem helmInventoryItem;
	public InventoryItem clothInventoryItem;
	public InventoryItem weaponInventoryItem;
	public InventoryItem shoesInventoryItem;
	public InventoryItem necklaceInventoryItem;
	public InventoryItem braceleInventoryItem;
	public InventoryItem ringInventoryItem;
	public InventoryItem wingInventoryItem;
	
	
	#endregion

	private float energyTime = 0f;
	private float toughenTime = 0f;

	public delegate void OnPlayerInfoChangeEvent(InfoType type);
	public event OnPlayerInfoChangeEvent OnPlayerInfoChanged;


	#region get set function

	public float EnergyTime
	{
		get{return energyTime;}
		set{energyTime = value;}
	}
	public float ToughenTime
	{
		get{return toughenTime;}
		set{toughenTime = value;}
	}

	public string Name{
		get{return _name;}
		set{_name = value;}
	}

	public string HeadPortrait{
		get{return _headPortrait;}
		set{_headPortrait = value;}
	}

	public int Level
	{
		get{return _level;}
		set{_level = value;}
	}
	public int Power
	{
		get{return _power;}
		set{_power = value;}
	}
	public int Exp
	{
		get{return _exp;}
		set{_exp = value;}
	}
	public int Diamond
	{
		get{return _diamond;}
		set{_diamond = value;}
	}
	public int Coin
	{
		get{return _coin;}
		set{_coin = value;}
	}
	public int Energy
	{
		get{return _energy;}
		set{_energy = value;}
	}
	public int Toughen
	{
		get{return _toughen;}
		set{_toughen = value;}
	}
	public int Hp
	{
		get{return _hp;}
		set{_hp = value;}
	}
	public int Damage
	{
		get{return _damage;}
		set{_damage = value;}
	}
	/*public int HelmID
	{
		get{return _helmID;}
		set{_helmID = value;}
	}
	public int CloseID
	{
		get{return _clothID;}
		set{_clothID = value;}
	}
	public int WeaponID
	{
		get{return _weaponID;}
		set{_weaponID = value;}
	}
	public int ShoesID
	{
		get{return _shoesID;}
		set{_shoesID = value;}
	}
	public int NecklaceID
	{
		get{return _necklaceID;}
		set{_necklaceID = value;}
	}
	public int BraceleId
	{
		get{return _braceleID;}
		set{_braceleID = value;}
	}
	public int RingID
	{
		get{return _ringID;}
		set{_ringID = value;}
	}
	public int WingID
	{
		get{return _wingID;}
		set{_wingID = value;}
	}*/

	#endregion

	void Init()
	{
		this.Coin = 10000;
		this.Diamond = 10000;
		this.Energy = 50;
		this.HeadPortrait = "头像底板女性";
		this.Level = 1;
		this.Exp = 20;
		this.Name = "柔小美";
		this.Power = 1745;
		this.Toughen = 34;

		/*this.BraceleId = 1001;
		this.WingID = 1002;
		this.RingID = 1003;
		this.CloseID = 1004;
		this.HelmID = 1005;
		this.WeaponID = 1006;
		this.NecklaceID = 1007;
		this.ShoesID = 1008;*/

		InitHpDamagePower();

		OnPlayerInfoChanged(InfoType.All);
	}

	void InitHpDamagePower()
	{
		this.Hp = this.Level * 100;
		this.Damage = this.Level * 50;
		this.Power = this.Hp + this.Damage;

		/*PutonEquip(this.BraceleId);
		PutonEquip(this.WingID);
		PutonEquip(this.RingID);
		PutonEquip(this.CloseID);
		PutonEquip(this.HelmID);
		PutonEquip(this.WeaponID);
		PutonEquip(this.NecklaceID);
		PutonEquip(this.ShoesID);*/

	}

	public int GetOverallPower()
	{
		float power = (float)this.Power;
		if(helmInventoryItem != null)
		{
			power += helmInventoryItem.Inventory.Power*(1 + (helmInventoryItem.Level - 1)/10f);
		}
		if(clothInventoryItem != null)
		{
			power += clothInventoryItem.Inventory.Power*(1 + (clothInventoryItem.Level - 1)/10f);
		}
		if(weaponInventoryItem != null)
		{
			power += weaponInventoryItem.Inventory.Power*(1 + (weaponInventoryItem.Level - 1)/10f);
		}
		if(shoesInventoryItem != null)
		{
			power += shoesInventoryItem.Inventory.Power*(1 + (shoesInventoryItem.Level - 1)/10f);
		}
		if(necklaceInventoryItem != null)
		{
			power += necklaceInventoryItem.Inventory.Power*(1 + (necklaceInventoryItem.Level - 1)/10f);
		}
		if(braceleInventoryItem != null)
		{
			power += braceleInventoryItem.Inventory.Power*(1 + (braceleInventoryItem.Level - 1)/10f);
		}
		if(ringInventoryItem != null)
		{
			power += ringInventoryItem.Inventory.Power*(1 + (ringInventoryItem.Level - 1)/10f);
		}
		if(wingInventoryItem != null)
		{
			power += wingInventoryItem.Inventory.Power*(1 + (wingInventoryItem.Level - 1)/10f);
		}
		return (int)power;
	}

	public void ChangeName(string newName)
	{
		this.Name = newName;
		OnPlayerInfoChanged(InfoType.All);
	}
	public void DressOn(InventoryItem it)
	{
		it.IsDressed = true;
		bool isDressed = false;
		InventoryItem inventoryItemDressed = null;
		switch(it.Inventory.EquipTYPE)
		{
			case EquipType.Bracelet:
				if(braceleInventoryItem != null)
				{
					isDressed = true;
					inventoryItemDressed = braceleInventoryItem;
				}
				braceleInventoryItem = it;
				break;
			case EquipType.Cloth:
				if(clothInventoryItem != null)
				{
					isDressed = true;
					inventoryItemDressed = clothInventoryItem;
				}
				clothInventoryItem = it;
				break;
			case EquipType.Helm:
				if(helmInventoryItem != null)
				{
					isDressed = true;
					inventoryItemDressed = helmInventoryItem;
				}
				helmInventoryItem = it;
				break;
			case EquipType.Necklace:
				if(necklaceInventoryItem != null)
				{
					isDressed = true;
					inventoryItemDressed = necklaceInventoryItem;
				}
				necklaceInventoryItem = it;
				break;
			case EquipType.Ring:
				if(ringInventoryItem != null)
				{
					isDressed = true;
					inventoryItemDressed = ringInventoryItem;
				}
				ringInventoryItem = it;
				break;
			case EquipType.Shoes:
				if(shoesInventoryItem != null)
				{
					isDressed = true;
					inventoryItemDressed = shoesInventoryItem;
				}
					shoesInventoryItem = it;
				break;
			case EquipType.Weapon:
				if(weaponInventoryItem != null)
				{
					isDressed = true;
					inventoryItemDressed = weaponInventoryItem;
				}
				weaponInventoryItem = it;
				break;
			case EquipType.Wing:
				if(wingInventoryItem != null)
				{
					isDressed = true;
					inventoryItemDressed = wingInventoryItem;
				}
				wingInventoryItem = it;
				break;
		}
		if(isDressed)
		{
			inventoryItemDressed.IsDressed = false;
			InventoryUI._instance.AddInventoryItem(inventoryItemDressed);
		}
		OnPlayerInfoChanged(InfoType.Equip);
	}

	public void DressOff(InventoryItem it)
	{
		switch(it.Inventory.EquipTYPE)
		{
		case EquipType.Bracelet:
			if(braceleInventoryItem != null)
			{
				braceleInventoryItem = null;
			}
			break;
		case EquipType.Cloth:
			if(clothInventoryItem != null)
			{
				clothInventoryItem = null;
			}
			break;
		case EquipType.Helm:
			if(helmInventoryItem != null)
			{
				helmInventoryItem = null;
			}
			break;
		case EquipType.Necklace:
			if(necklaceInventoryItem != null)
			{
				necklaceInventoryItem  = null;
			}
			break;
		case EquipType.Ring:
			if(ringInventoryItem != null)
			{
				ringInventoryItem = null;
			}
			break;
		case EquipType.Shoes:
			if(shoesInventoryItem != null)
			{
				shoesInventoryItem = null;
			}
			break;
		case EquipType.Weapon:
			if(weaponInventoryItem != null)
			{
				weaponInventoryItem = null;
			}
			break;
		case EquipType.Wing:
			if(wingInventoryItem != null)
			{
				wingInventoryItem = null;
			}
			break;
		}
		it.IsDressed = false;
		InventoryUI._instance.AddInventoryItem(it);
		OnPlayerInfoChanged(InfoType.Equip);
	}

	public void InventoryUse(InventoryItem it, int count)
	{
		it.Count -= count;
		if(it.Count <= 0)
		{
			InventoryManager._instance.inventoryItemList.Remove(it);
		}
	}

	void PutonEquip(int id)
	{
		if(id == 0)
			return;
		Inventory inventory = null;
		InventoryManager._instance.inventoryDic.TryGetValue(id, out inventory);
		this.Hp += inventory.Hp;
		this.Damage += inventory.Damage;
	}
	void PutoffEquip(int id)
	{
		if(id == 0)
			return;
		Inventory inventory = null;
		InventoryManager._instance.inventoryDic.TryGetValue(id,out inventory);
		this.Hp -= inventory.Hp;
		this.Damage -= inventory.Damage;
	}
	public bool GetCoin(int count)
	{
		if(Coin >= count)
		{
			Coin -= count;
			OnPlayerInfoChanged(InfoType.Coin);
			return true;
		}
		else
		{
			return false;
		}
	}
}










