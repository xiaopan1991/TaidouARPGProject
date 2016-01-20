using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float velocity = 5;
	private Rigidbody m_rigidbody;
	private Animator anim;

	void Awake()
	{
		this.m_rigidbody = GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 nowVel = m_rigidbody.velocity;
		if(Mathf.Abs(h)>0.05f || Mathf.Abs(v)>0.05f)
		{
			m_rigidbody.velocity = new Vector3(velocity*h,nowVel.y,velocity*v);
			if(anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State"))
			{
				anim.SetBool("Move", true);
				this.transform.LookAt(new Vector3(h,0,v) + transform.position);
//				transform.rotation = Quaternion.LookRotation(new Vector3(h,0,v));
			}
			else
			{
				m_rigidbody.velocity = new Vector3(0,nowVel.y,0);
				anim.SetBool("Move", false);
			}
		}
		else
		{
			m_rigidbody.velocity = new Vector3(0,nowVel.y,0);
			anim.SetBool("Move", false);
		}
	}
}
