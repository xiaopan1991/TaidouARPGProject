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
	private int _helmID = 0;
	private int _clothID = 0;
	private int _weaponID = 0;
	private int _shoesID = 0;
	private int _necklaceID = 0;
	private int _braceleID = 0;
	private int _ringID = 0;
	private int _wingID = 0;

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
	public int HelmID
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
	}

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

		this.BraceleId = 1001;
		this.WingID = 1002;
		this.RingID = 1003;
		this.CloseID = 1004;
		this.HelmID = 1005;
		this.WeaponID  =1006;
		this.NecklaceID = 1007;
		this.ShoesID = 1008;

		InitHpDamagePower();

		OnPlayerInfoChanged(InfoType.All);
	}

	void InitHpDamagePower()
	{
		this.Hp = this.Level * 100;
		this.Damage = this.Level * 50;
		this.Power = this.Hp + this.Damage;

		PutonEquip(this.BraceleId);
		PutonEquip(this.WingID);
		PutonEquip(this.RingID);
		PutonEquip(this.CloseID);
		PutonEquip(this.HelmID);
		PutonEquip(this.WeaponID);
		PutonEquip(this.NecklaceID);
		PutonEquip(this.ShoesID);

	}

	public void ChangeName(string newName)
	{
		this.Name = newName;
		OnPlayerInfoChanged(InfoType.All);
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

}










