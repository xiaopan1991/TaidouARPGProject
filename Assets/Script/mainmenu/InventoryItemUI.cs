using UnityEngine;
using System.Collections;

public class InventoryItemUI : MonoBehaviour 
{
	private UISprite sprite;
	private UILabel label;
	private InventoryItem it;

	private UISprite Sprite
	{
		get{
			if(sprite == null)
			{
				sprite = transform.Find("Sprite").GetComponent<UISprite>();
			}
			return sprite;
		}
	}

	private UILabel Label
	{
		get
		{
			if(label == null)
			{
				label = transform.Find("Label").GetComponent<UILabel>();
			}
			return label;
		}
	}

	public void SetInventoryItem(InventoryItem it)
	{
		this.it = it;
		Sprite.spriteName = it.Inventory.Icon;
		if(it.Count <= 1)
		{
			Label.text = "";
		}
		else
		{
			Label.text = it.Count.ToString();
		}
	}

	public void Clear()
	{
		Label.text = "";
		Sprite.spriteName = "bg_道具";
	}
}
