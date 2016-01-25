using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Vector3 Offset;
	private Transform playerBip;
	public float smoothing = 1;

	// Use this for initialization
	void Start () {
		playerBip = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
	}
	
	// Update is called once per frame
	void Update () {
//		transform.position = playerBip.position + Offset;
		Vector3 targetPos = playerBip.position + Offset;
		transform.position = Vector3.Lerp(transform.position, targetPos, smoothing*Time.deltaTime);
	}
}
