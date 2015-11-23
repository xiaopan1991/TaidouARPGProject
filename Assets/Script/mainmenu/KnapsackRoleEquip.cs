﻿using UnityEngine;
using System.Collections;

public class KnapsackRoleEquip : MonoBehaviour
{
	private UISprite _sprite;
	private UISprite Sprite{
		get {
			if(_sprite == null)
			{
				_sprite = this.GetComponent<UISprite>();
			}
			return _sprite;
		}
	}

	public void SetId(int id)
	{
		Inventory inventory = null;
		bool isExit = InventoryManager._instance.inventoryDic.TryGetValue(id, out inventory);
		if(isExit)
		{
			Sprite.spriteName = inventory.Icon;
		}
	}
}
