using UnityEngine;
using System.Collections;

public class PlayerVillageMove : MonoBehaviour {

	public float velocity = 5;
	private Rigidbody m_rigidbody;


	void Awake()
	{
		this.m_rigidbody = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 vel = m_rigidbody.velocity;
		this.m_rigidbody.velocity = new Vector3(-h*velocity, vel.y, -v*velocity);

		if(Mathf.Abs(h)>0.005f || Mathf.Abs(v)>0.005f)
		{
			transform.rotation = Quaternion.LookRotation(new Vector3(-h,0,-v));
		}

	}
}
