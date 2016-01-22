using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject damageEffectPrefab;
	public int hp = 200;

	private Animation eneryAnimation;
	private Transform bloodPoint;

	// Use this for initialization
	void Awake () {
		eneryAnimation = this.GetComponent<Animation>();
		bloodPoint = transform.Find("BloodPoint");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//受到攻击
	//0，受到多少伤害  1.后退距离 2.浮空高度
	void TakeDamage(string arg)
	{
		if(hp <= 0) return;
		
		string[] proArray = arg.Split(',');

		int damage = int.Parse(proArray[0]);
		hp -= damage;
		if(hp <= 0)
		{
			Dead();
		}

		eneryAnimation.Play("takedamage");
		float backDistance = float.Parse(proArray[1]);
		float jumpHeight = float.Parse(proArray[2]);
		iTween.MoveBy(this.gameObject, transform.InverseTransformDirection(TranscriptManager._instance.Player.transform.forward)*backDistance + Vector3.up*jumpHeight,0.3f);
		GameObject.Instantiate(damageEffectPrefab, bloodPoint.position, Quaternion.identity);
	}

	void Dead()
	{
		
	}
}
