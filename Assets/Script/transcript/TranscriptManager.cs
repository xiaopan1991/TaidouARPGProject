using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TranscriptManager : MonoBehaviour {

	public static TranscriptManager _instance;
	private GameObject player;

	public List<GameObject> enemyList = new List<GameObject>();


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
