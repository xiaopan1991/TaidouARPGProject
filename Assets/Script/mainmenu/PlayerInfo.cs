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
	#endregion

	public float energyTime = 0f;
	public float toughenTime = 0f;

	public delegate void OnPlayerInfoChangeEvent(InfoType type);
	public event OnPlayerInfoChangeEvent OnPlayerInfoChanged;


	#region get set function

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

		OnPlayerInfoChanged(InfoType.All);
	}

	public void ChangeName(string newName)
	{
		this.Name = newName;
		OnPlayerInfoChanged(InfoType.All);
	}
}
