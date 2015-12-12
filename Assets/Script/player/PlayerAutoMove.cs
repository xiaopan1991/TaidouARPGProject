using UnityEngine;
using System.Collections;

public class PlayerAutoMove : MonoBehaviour {

	private NavMeshAgent agent;
	private float minDistance = 3.5f;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.enabled && (agent.remainingDistance < minDistance) && agent.remainingDistance!=0)
		{
			agent.Stop();
			agent.enabled = false;
			TaskManager._instance.OnArriveDestination();
		}

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		if(Mathf.Abs(h)>0.005f || Mathf.Abs(v)>0.005f)
		{
			StopAuto();
		}
	}

	public void SetDestination(Vector3 targetPos)
	{
		agent.enabled = true;
		agent.SetDestination(targetPos);
	}

	public void StopAuto()
	{
		if(agent.enabled)
		{
			agent.Stop();
			agent.enabled = false;
		}
	}
}
