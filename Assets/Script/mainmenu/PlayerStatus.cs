using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	private UISprite headSprite;
	private UILabel levelLabel;
	private UILabel nameLabel;
	private UILabel powerLabel;
	private UISlider expSlider;
	private UILabel expLabel;
	private UILabel diamondLabel;
	private UILabel coinLabel;
	private UILabel energyLabel;
	private UILabel energyRestorePartLabel;
	private UILabel energyRestoreAllLabel;
	private UILabel toughenLabel;
	private UILabel toughenRestorePartLabel;
	private UILabel toughenRestoreAllLabel;

	void Awake()
	{
		headSprite = this.transform.Find("HeadSprite").GetComponent<UISprite>();
		levelLabel = this.transform.Find("LevelLabel").GetComponent<UILabel>();
		nameLabel = this.transform.Find("NameLabel").GetComponent<UILabel>();
		powerLabel = this.transform.Find("PowerLabel").GetComponent<UILabel>();
		expSlider = this.transform.Find("ExpProgressBar").GetComponent<UISlider>();
		expLabel = this.transform.Find("ExpProgressBar/Label").GetComponent<UILabel>();
		diamondLabel = this.transform.Find("DiamondLabel").GetComponent<UILabel>();
		coinLabel = this.transform.Find("CoinLabel").GetComponent<UILabel>();
		energyLabel = this.transform.Find("EnergyLabel/NumLabel").GetComponent<UILabel>();
		energyRestorePartLabel = this.transform.Find("EnergyLabel/RestorePartTime").GetComponent<UILabel>();
		energyRestoreAllLabel = this.transform.Find("EnergyLabel/RestoreAllTime").GetComponent<UILabel>();
		toughenLabel = this.transform.Find("ToughenLabel/NumLabel").GetComponent<UILabel>();
		toughenRestorePartLabel = this.transform.Find("ToughenLabel/RestorePartTime").GetComponent<UILabel>();
		toughenRestoreAllLabel = this.transform.Find("ToughenLabel/RestoreAllTime").GetComponent<UILabel>();
	}
}















































