using UnityEngine;
using System.Collections;

public class PlayerVillageMove : MonoBehaviour {

	public float velocity = 5;
	private Rigidbody m_rigidbody;
	private NavMeshAgent agent;


	void Awake()
	{
		this.m_rigidbody = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 vel = m_rigidbody.velocity;

		if(Mathf.Abs(h)>0.005f || Mathf.Abs(v)>0.005f)
		{
			this.m_rigidbody.velocity = new Vector3(-h*velocity, vel.y, -v*velocity);
			transform.rotation = Quaternion.LookRotation(new Vector3(-h,0,-v));
		}
		else
		{
			if(agent.enabled == false)
			{
				m_rigidbody.velocity = Vector3.zero;
			}
		}
		if(agent.enabled)
		{
			transform.rotation = Quaternion.LookRotation(agent.velocity);
		}
	}
}
