using UnityEngine;
using System.Collections;

public class TranscriptManager : MonoBehaviour {

	public static TranscriptManager _instance;
	private GameObject player;

	void Awake () {
		_instance = this;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public GameObject Player
	{
		get{
			return player;
		}
	}

	void Start () {
		
	}
}
