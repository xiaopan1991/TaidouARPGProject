using UnityEngine;
using System.Collections;

public class KnapsackRole : MonoBehaviour {

	private KnapsackRoleEquip helmEquip;
	private KnapsackRoleEquip clothEquip;
	private KnapsackRoleEquip weaponEquip;
	private KnapsackRoleEquip shoesEquip;
	private KnapsackRoleEquip necklaceEquip;
	private KnapsackRoleEquip braceletEquip;
	private KnapsackRoleEquip ringEquip;
	private KnapsackRoleEquip wingEquip;

	private UILabel hpLabel;
	private UILabel damageLabel;
	private UILabel expLabel;
	private UISlider expSlider;

	void Awake()
	{
		helmEquip = this.transform.Find("HelmSprite").GetComponent<KnapsackRoleEquip>();
		clothEquip = this.transform.Find("ClothSprite").GetComponent<KnapsackRoleEquip>();
		weaponEquip = this.transform.Find("WeaponSprite").GetComponent<KnapsackRoleEquip>();
		shoesEquip = this.transform.Find("ShoesSprite").GetComponent<KnapsackRoleEquip>();
		necklaceEquip = this.transform.Find("NecklaceSprite").GetComponent<KnapsackRoleEquip>();
		braceletEquip = this.transform.Find("BraceletSprite").GetComponent<KnapsackRoleEquip>();
		ringEquip = this.transform.Find("RingSprite").GetComponent<KnapsackRoleEquip>();
		wingEquip = this.transform.Find("WingSprite").GetComponent<KnapsackRoleEquip>();

		hpLabel = this.transform.Find("HpBg/Label").GetComponent<UILabel>();
		damageLabel = this.transform.Find("DamageBg/Label").GetComponent<UILabel>();
		expLabel = this.transform.Find("ExpSlider/Label").GetComponent<UILabel>();
		expSlider = this.transform.Find("ExpSlider").GetComponent<UISlider>();

		PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
	}

	void OnDestory()
	{
		PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
	}

	void OnPlayerInfoChanged(InfoType type)
	{
		if(type == InfoType.All || type == InfoType.Damage || 
		   type == InfoType.HP || type == InfoType.Equip)
		{
			UpdateShow();
		}
	}

	void UpdateShow()
	{
		PlayerInfo info = PlayerInfo._instance;

		/*helmEquip.SetId(info.HelmID);
		clothEquip.SetId(info.CloseID);
		weaponEquip.SetId(info.WeaponID);
		shoesEquip.SetId(info.ShoesID);

		necklaceEquip.SetId(info.NecklaceID);
		braceletEquip.SetId(info.BraceleId);
		ringEquip.SetId(info.RingID);
		wingEquip.SetId(info.WingID);*/

		hpLabel.text = info.Hp.ToString();
		damageLabel.text = info.Damage.ToString();
		expLabel.text = info.Exp + "/" + GameController.getRequireExpByLevel(info.Level + 1);
		expSlider.value = (float)info.Exp/GameController.getRequireExpByLevel(info.Level + 1);
	}
}
