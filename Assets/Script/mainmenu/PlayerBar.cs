using UnityEngine;
using System.Collections;

public class PlayerBar : MonoBehaviour {

	private UISprite headSprite;
	private UILabel nameLabel;
	private UILabel levelLabel;
	private UILabel energyLabel;
	private UILabel toughenLabel;
	private UISlider toughenSlider;
	private UISlider energySlider;
	private UIButton energyPlusButton;
	private UIButton toughenPlusButton;
	private UIButton headButton;

	void Awake()
	{
		headSprite = this.transform.Find("HeadSprite").GetComponent<UISprite>();
		nameLabel = this.transform.Find ("NameLabel").GetComponent<UILabel>();
		levelLabel = this.transform.Find("LevelLabel").GetComponent<UILabel>();
		toughenSlider = this.transform.Find("ToughenProgressBar").GetComponent<UISlider>();
		toughenLabel = this.transform.Find("ToughenProgressBar/Label").GetComponent<UILabel>();
		energySlider = this.transform.Find("EnergyProgressBar").GetComponent<UISlider>();
		energyLabel = this.transform.Find("EnergyProgressBar/Label").GetComponent<UILabel>();
		energyPlusButton = this.transform.Find("EnergyPlusButton").GetComponent<UIButton>();
		toughenPlusButton = this.transform.Find("ToughenPlusButton").GetComponent<UIButton>();
		headButton = this.transform.Find("HeadButton").GetComponent<UIButton>();

		PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;

		EventDelegate ed = new EventDelegate(this, "OnHeadButtonClick");
		headButton.onClick.Add(ed);
	}

	void OnDestory()
	{
		PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
	}

	void OnPlayerInfoChanged(InfoType type)
	{
		if(type == InfoType.Name || type == InfoType.HeadPortrait || type== InfoType.Level 
		   || type==InfoType.Energy || type == InfoType.Toughen || type == InfoType.Energy
		   || type== InfoType.All)
		{
			UpdateShow();
		}
	}

	void UpdateShow()
	{
		PlayerInfo info = PlayerInfo._instance;

		headSprite.spriteName = info.HeadPortrait.ToString();
		levelLabel.text = info.Level.ToString();
		nameLabel.text = info.Name.ToString();
		energySlider.value = info.Energy / 100f;
		energyLabel.text = info.Energy + "/100";
		toughenSlider.value = info.Toughen / 50f;
		toughenLabel.text = info.Toughen + "/50";
	}
	public void OnHeadButtonClick()
	{
		PlayerStatus._instance.Show();
	}
}
