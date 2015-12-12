using UnityEngine;
using System.Collections;

public class PlayerVillageAnimation : MonoBehaviour {

	private Animator anim;
	private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
		m_rigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_rigidbody.velocity.magnitude > 0.5f)
		{
			anim.SetBool("Move", true);
		}
		else
		{
			anim.SetBool("Move", false);
		}
	}
}
