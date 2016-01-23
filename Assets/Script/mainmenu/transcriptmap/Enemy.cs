using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject damageEffectPrefab;
	public int hp = 200;
	public float speed = 2;
	public int attackRate = 2;
	public float attackDistance = 2;
	public float downSpeed = 2.0f;
	public float damage = 20;
	private float attackTimer = 0;
	private float distance = 0;
	private float downDistance = 0;
	private int hpTotal = 0;

	private Animation enemyAnimation;
	private Transform bloodPoint;
	private CharacterController cc;
	private GameObject hpBarGameObject;
	private UISlider hpBarSlider;
	private GameObject hudTextGameObject;
	private HUDText hudText;

	// Use this for initialization
	void Awake () {
		enemyAnimation = this.GetComponent<Animation>();
		bloodPoint = transform.Find("BloodPoint");
		cc = this.GetComponent<CharacterController>();

		hpTotal = hp;
	}

	void Start()
	{
		InvokeRepeating("CalcDistance", 0, 0.1f);
		Transform hpBarPoint = transform.Find("HpBarPoint");
		hpBarGameObject = HpBarManager._instance.GetHpBar(hpBarPoint.gameObject);
		hpBarSlider = hpBarGameObject.transform.Find("Bg").GetComponent<UISlider>();
		hudTextGameObject = HpBarManager._instance.GetHudText(hpBarPoint.gameObject);
		hudText = hudTextGameObject.GetComponent<HUDText>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hp <=0 ) 
		{
			downDistance += downSpeed * Time.deltaTime;
			transform.Translate(-transform.up * downSpeed * Time.deltaTime);
			if(downDistance > 4)
			{
				Destroy(this.gameObject);
			}
			return;
		}
		if(distance < attackDistance)
		{
			attackTimer += Time.deltaTime;
			if(attackTimer > attackRate)
			{
				Transform player = TranscriptManager._instance.Player.transform;
				Vector3 targetPos = player.position;
				targetPos.y = transform.position.y;
				transform.LookAt(targetPos);
				enemyAnimation.Play("attack01");
				attackTimer = 0;
			}
			if(!enemyAnimation.IsPlaying("attack01"))
			{
				enemyAnimation.CrossFade("idle");
			}
		}
		else
		{
			enemyAnimation.Play("walk");
			Move();
		}
	}

	void CalcDistance()
	{
		Transform player = TranscriptManager._instance.Player.transform;
		distance = Vector3.Distance(player.position, transform.position);
	}

	void Attack()
	{
		Transform player = TranscriptManager._instance.Player.transform;
		distance = Vector3.Distance(player.position, transform.position);
		if(distance < attackDistance)
		{
			player.SendMessage("TakeDamage",damage,SendMessageOptions.DontRequireReceiver);
		}
	}

	void Move()
	{
		Transform player = TranscriptManager._instance.Player.transform;
		Vector3 targetPos = player.position;
		targetPos.y = transform.position.y;
		transform.LookAt(targetPos);
		cc.SimpleMove(transform.forward * speed);
	}

	void Dead()
	{
		TranscriptManager._instance.enemyList.Remove(this.gameObject);
		this.GetComponent<CharacterController>().enabled = false;
		Destroy(hpBarGameObject);
		Destroy(hudTextGameObject);
		int random = Random.Range(0,10);
		if(random <= 7)
		{
			//第一种死亡方式是播放死亡动画
			enemyAnimation.Play("die");
		}
		else
		{
			//第二种方式是使用破碎效果
			this.GetComponentInChildren<MeshExploder>().Explode();
			this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		}
	}

	//受到攻击
	//0，受到多少伤害  1.后退距离 2.浮空高度
	void TakeDamage(string arg)
	{
		if(hp <= 0) return;
		Combo._instance.ComboPlus();
		string[] proArray = arg.Split(',');

		int damage = int.Parse(proArray[0]);
		hp -= damage;
		hpBarSlider.value = (float)hp/hpTotal;
		hudText.Add("-"+damage, Color.red, 0.3f);
		if(hp <= 0)
		{
			Dead();
		}

		enemyAnimation.Play("takedamage");
		float backDistance = float.Parse(proArray[1]);
		float jumpHeight = float.Parse(proArray[2]);
		iTween.MoveBy(this.gameObject, transform.InverseTransformDirection(TranscriptManager._instance.Player.transform.forward)*backDistance + Vector3.up*jumpHeight,0.3f);
		GameObject.Instantiate(damageEffectPrefab, bloodPoint.position, Quaternion.identity);

		if(hp <=0 ) 
		{
			Dead();
		}
	}
}
