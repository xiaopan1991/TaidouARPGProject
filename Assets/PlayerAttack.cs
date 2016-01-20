using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

	private Dictionary<string, PlayerEffect>effectDict = new Dictionary<string, PlayerEffect>();

	void Start(){
		PlayerEffect[] peArray = this.GetComponentsInChildren<PlayerEffect>();
		foreach(PlayerEffect pe in peArray)
		{
			effectDict.Add(pe.gameObject.name, pe);
		}
	}

	//0 normal skill1 skill2 skill3
	//1 effect name
	//2 sound name
	//3 move forward
	//4 jump height
	void Attack(string args)
	{
		string[] proArray = args.Split(',');
		string effectName = proArray[1];
		ShowPlayerEffect(effectName);

	}
	void ShowPlayerEffect(string effectName)
	{
		PlayerEffect pe;
		if(effectDict.TryGetValue(effectName,out pe))
		{
			pe.Show();
		}
	}
}
