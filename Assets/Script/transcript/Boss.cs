using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public float viewAngle = 50;
	public float rotateSpeed = 1;
	public float attackDistance = 3;
	public float moveSpeed = 2;
	public float timeInterval = 1;
	public float[] attackArray = {100,200,300};
	public GameObject bossBulletPrefab;

	private float timer = 0;
	private bool isAttacking = false;
	private Transform player;
	private Animation boss_anim;
	private Rigidbody boss_rigidbody;
	private GameObject attack01GameObject;
	private GameObject attack02GameObject;
	private Transform attack03Pos;

	// Use this for initialization
	void Start () {
		player =  TranscriptManager._instance.Player.transform;
		boss_anim = this.GetComponent<Animation>();
		boss_rigidbody = this.GetComponent<Rigidbody>();
		attack01GameObject = transform.Find("attack01").gameObject;
		attack02GameObject = transform.Find("attack02").gameObject;
		attack03Pos = transform.Find("attack03Pos");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = player.position;
		playerPos.y = transform.position.y;
		float angle = Vector3.Angle(playerPos - transform.position, transform.forward);
		if(isAttacking)
			return;
		if(angle < viewAngle/2)
		{
			float distance = Vector3.Distance(player.position, transform.position);
			if(distance < attackDistance)
			{
				if(!isAttacking)
				{
					boss_anim.CrossFade("stand");
					timer += Time.deltaTime;
					if(timer > timeInterval)
					{
						timer = 0;
						Attack();
					}
				}
			}
			else
			{
				boss_anim.CrossFade("walk");
				boss_rigidbody.MovePosition(transform.position + transform.forward*moveSpeed*Time.deltaTime);
			}
		}
		else
		{
			Quaternion targetRotation = Quaternion.LookRotation(playerPos - transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);
			boss_anim.CrossFade("walk");
		}
	}

	private int attackIndex = 0;
	void Attack()
	{
		isAttacking = true;
		attackIndex++;
		if(attackIndex == 4) attackIndex = 1;
		boss_anim.CrossFade("attack0" + attackIndex);
	}
	void BackToStand()
	{
		isAttacking = false;
	}

	void PlayAttack01Effect()
	{
		attack01GameObject.SetActive(true);
		float distance = Vector3.Distance(player.position, transform.position);
		if(distance < attackDistance)
		{
			player.SendMessage("TakeDamage", attackArray[0], SendMessageOptions.DontRequireReceiver);
		}
	}

	void PlayAttack02Effect()
	{
		attack02GameObject.SetActive(true);
		float distance = Vector3.Distance(player.position, transform.position);
		if(distance < attackDistance)
		{
			player.SendMessage("TakeDamage", attackArray[1], SendMessageOptions.DontRequireReceiver);
		}
	}

	void PlayAttack03Effect()
	{
		GameObject go = GameObject.Instantiate(bossBulletPrefab, attack03Pos.position, attack03Pos.rotation) as GameObject;
		go.GetComponent<BossBullet>().Damage = attackArray[2];
	}
}
