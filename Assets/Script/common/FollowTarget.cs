using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Vector3 Offset;
	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.position + Offset;
	}
}
