using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

	private Dictionary<string, PlayerEffect>effectDict = new Dictionary<string, PlayerEffect>();
	public float distanceAttackForward = 2;
	public float distanceAttackAround = 2;
	public int[] damageArray = new int[]{20,30,30,30};

	public enum AttackRange{
		Forward,
		Around
	}

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
		string soundName = proArray[2];
		SoundManager._instance.Play(soundName);
		float moveForward = float.Parse(proArray[3]);
		if(moveForward > 0.1f)
		{
			iTween.MoveBy(this.gameObject, Vector3.forward*moveForward, 0.3f);
		}

		string posType = proArray[0];
		if(posType == "normal")
		{
			ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
			foreach(GameObject go in array)
			{
				go.SendMessage("TakeDamage", damageArray[0]+","+proArray[3]+","+proArray[4]);
			}
		}
	}
	void ShowPlayerEffect(string effectName)
	{
		PlayerEffect pe;
		if(effectDict.TryGetValue(effectName,out pe))
		{
			pe.Show();
		}
	}

	ArrayList GetEnemyInAttackRange( AttackRange attackRange )
	{
		ArrayList arrayList = new ArrayList();
		if(attackRange == AttackRange.Forward)
		{
			foreach(GameObject go in TranscriptManager._instance.enemyList)
			{
				Vector3 pos = transform.InverseTransformPoint(go.transform.position);

				if(pos.z > -0.5f)
				{
					float distance = Vector3.Distance(Vector3.zero, pos);
					if(distance < distanceAttackForward)
					{
						arrayList.Add(go);
					}
				}
			}
		}
		else if(attackRange == AttackRange.Around)
		{
			foreach(GameObject go in TranscriptManager._instance.enemyList)
			{
				float distance = Vector3.Distance(transform.position, go.transform.position);
				if(distance < distanceAttackAround)
				{
					arrayList.Add(go);
				}
			}
		}
		return arrayList;
	}

}
