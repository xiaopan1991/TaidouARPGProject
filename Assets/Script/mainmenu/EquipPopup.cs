using UnityEngine;
using System.Collections;

public class EquipPopup : MonoBehaviour {

	private InventoryItem it;
	private InventoryItemUI itUI;

	private UISprite equipSprite;
	private UILabel nameLabel;
	private UILabel qualityLabel;
	private UILabel damageLabel;
	private UILabel hpLabel;
	private UILabel powerLabel;
	private UILabel desLabel;
	private UILabel levelLabel;
	private UIButton closeButton;
	private UIButton equipButton;

	void Awake()
	{
		equipSprite = transform.Find("EquipBg/Sprite").GetComponent<UISprite>();
		nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
		qualityLabel = transform.Find("QualityLabel/Label").GetComponent<UILabel>();
		damageLabel = transform.Find("DamageLabel/Label").GetComponent<UILabel>();
		hpLabel = transform.Find("HpLabel/Label").GetComponent<UILabel>();
		powerLabel = transform.Find("PowerLabel/Label").GetComponent<UILabel>();
		desLabel = transform.Find("DesLabel").GetComponent<UILabel>();
		levelLabel = transform.Find("LevelLabel/Label").GetComponent<UILabel>();

		closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
		equipButton = transform.Find("EquipButton").GetComponent<UIButton>();

		EventDelegate ed1 = new EventDelegate(this, "OnClose");
		closeButton.onClick.Add(ed1);

		EventDelegate ed2 = new EventDelegate(this, "OnEquip");
		equipButton.onClick.Add(ed2);
	}

	public void Show(InventoryItem it, InventoryItemUI itUI, bool isLeft)
	{
		gameObject.SetActive(true);

		this.it = it;
		this.itUI = itUI;

		Vector3 pos = transform.localPosition;
		if(isLeft)
		{
			transform.localPosition = new Vector3(-Mathf.Abs(pos.x), pos.y, pos.z);
		}
		else
		{
			transform.localPosition = new Vector3(Mathf.Abs(pos.x), pos.y, pos.z);
		}
		equipSprite.spriteName = it.Inventory.Icon;
		nameLabel.text = it.Inventory.Name;
		qualityLabel.text = it.Inventory.Quality.ToString();
		damageLabel.text = it.Inventory.Damage.ToString();
		hpLabel.text = it.Inventory.Hp.ToString();
		powerLabel.text = it.Inventory.Power.ToString();
		desLabel.text = it.Inventory.Des;
		levelLabel.text = it.Level.ToString();
	}

	public void OnClose()
	{
		ClearObject();
		gameObject.SetActive(false);
	}
	public void OnEquip()
	{
		itUI.Clear();
		PlayerInfo._instance.DressOn(this.it);
		ClearObject();
		gameObject.SetActive(false);
	}
	void ClearObject()
	{
		it = null;
		itUI = null;
	}
}














