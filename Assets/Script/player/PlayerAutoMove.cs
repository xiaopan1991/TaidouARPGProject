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
		if(agent.enabled && (agent.remainingDistance < minDistance))
		{
			agent.Stop();
			agent.enabled = false;
		}

	}

	public void SetDestination(Vector3 targetPos)
	{
		agent.enabled = true;
		agent.SetDestination(targetPos);
	}
}
