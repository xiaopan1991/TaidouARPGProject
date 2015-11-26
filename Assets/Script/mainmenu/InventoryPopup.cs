using UnityEngine;
using System.Collections;

public class InventoryPopup : MonoBehaviour
{
	private UILabel nameLabel;
	private UISprite InventorySprite;
	private UILabel desLabel;
	private UILabel btnLabel;

	private InventoryItem it;

	void Awake()
	{
		nameLabel = this.transform.Find("Bg/NameLabel").GetComponent<UILabel>();
		InventorySprite = this.transform.Find("Bg/Sprite/Sprite").GetComponent<UISprite>();
		desLabel = this.transform.Find("Bg/Label").GetComponent<UILabel>();
		btnLabel = this.transform.Find("Bg/ButtonUseBatching/Label").GetComponent<UILabel>();
	}

	public void Show(InventoryItem it)
	{
		this.gameObject.SetActive(true);
		this.it = it;
		nameLabel.text = it.Inventory.Name;
		InventorySprite.spriteName = it.Inventory.Icon;
		desLabel.text = it.Inventory.Des;
		btnLabel.text = "批量使用(" + it.Count +")";
	}

}

























