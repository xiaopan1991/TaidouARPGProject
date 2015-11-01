using UnityEngine;
using System.Collections;

public class CharacterShow : MonoBehaviour {
	
	public void OnPress(bool isPress)
	{
		if(!isPress)
		{
			StartmenuController._instance.OnCharacterClick(this.transform.parent.gameObject);
		}
	}
}
