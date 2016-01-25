using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public float viewAngle = 50;
	public float rotateSpeed = 1;
	public float attackDistance = 3;
	public float moveSpeed = 2;

	private Transform player;
	private Animation boss_anim;
	private Rigidbody boss_rigidbody;

	// Use this for initialization
	void Start () {
		player =  TranscriptManager._instance.Player.transform;
		boss_anim = this.GetComponent<Animation>();
		boss_rigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = player.position;
		playerPos.y = transform.position.y;
		float angle = Vector3.Angle(playerPos - transform.position, transform.forward);
		if(angle < viewAngle/2)
		{
			float distance = Vector3.Distance(player.position, transform.position);
			if(distance < distance)
			{
				
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
}
