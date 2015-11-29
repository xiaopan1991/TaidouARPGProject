using UnityEngine;
using System.Collections;

public class InventoryPopup : MonoBehaviour
{
	private UILabel nameLabel;
	private UISprite InventorySprite;
	private UILabel desLabel;
	private UILabel btnLabel;

	private InventoryItem it;
	private InventoryItemUI itUI;

	private UIButton closeButton;
	private UIButton useButton;
	private UIButton useBatchingButton;

	void Awake()
	{
		nameLabel = this.transform.Find("Bg/NameLabel").GetComponent<UILabel>();
		InventorySprite = this.transform.Find("Bg/Sprite/Sprite").GetComponent<UISprite>();
		desLabel = this.transform.Find("Bg/Label").GetComponent<UILabel>();
		btnLabel = this.transform.Find("Bg/ButtonUseBatching/Label").GetComponent<UILabel>();

		closeButton = this.transform.Find("CloseButton").GetComponent<UIButton>();
		useButton = this.transform.Find("Bg/ButtonUse").GetComponent<UIButton>();
		useBatchingButton = this.transform.Find("Bg/ButtonUseBatching").GetComponent<UIButton>();
	
		EventDelegate ed1 = new EventDelegate(this, "OnClose");
		closeButton.onClick.Add(ed1);

		EventDelegate ed2 = new EventDelegate(this, "OnUse");
		useButton.onClick.Add(ed2);

		EventDelegate ed3 = new EventDelegate(this, "OnUseBatching");
		useBatchingButton.onClick.Add(ed3);
	}

	public void Show(InventoryItem it, InventoryItemUI itUI)
	{
		this.gameObject.SetActive(true);
		this.it = it;
		this.itUI = itUI;
		nameLabel.text = it.Inventory.Name;
		InventorySprite.spriteName = it.Inventory.Icon;
		desLabel.text = it.Inventory.Des;
		btnLabel.text = "批量使用(" + it.Count +")";
	}

	public void OnClose()
	{
		Clear();
		gameObject.SetActive(false);
	}

	public void OnUse()
	{
		itUI.ChangeCount(1);
		PlayerInfo._instance.InventoryUse(it, 1);
		OnClose();
	}

	public void OnUseBatching()
	{
		itUI.ChangeCount(it.Count);
		PlayerInfo._instance.InventoryUse(it, it.Count);
		OnClose();
	}

	void Clear()
	{
		it = null;
		itUI = null;
	}
}

























