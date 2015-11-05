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
	}
}
