using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	void Awake()
	{
		Transform posTransform = GameObject.Find("Player-pos").transform;
//		string playerPrefabName = "Player-girl";
		string playerPrefabName = "Player-boy";
		if(PhotonEngine.Instance.role.IsMan)
		{
			playerPrefabName = "Player-boy";
		}
		GameObject playerGo = GameObject.Instantiate(Resources.Load("Player/" + playerPrefabName)) as GameObject;
		playerGo.transform.position = posTransform.position;
	}

	public static int getRequireExpByLevel(int level)
	{
		return (int)((100f + (100f + 10*(level-2f)))/2 * (level-1));
	}
}
