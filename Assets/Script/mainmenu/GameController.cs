using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static int getRequireExpByLevel(int level)
	{
		return (int)((100f + (100f + 10*(level-2f)))/2 * (level-1));
	}
}
