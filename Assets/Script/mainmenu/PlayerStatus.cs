using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public static PlayerStatus _instance;

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
	private UIButton closeButton;

	private UIButton changeNameButton;
	private GameObject changeNameGo;
	private UIInput nameInput;
	private UIButton sureButton;
	private UIButton cancelButton;

	PlayerInfo info = null;

	private TweenPosition tween;

	void Awake()
	{
		_instance =this;

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
		closeButton = this.transform.Find("CloseButton").GetComponent<UIButton>();

		changeNameButton = this.transform.Find("ButtonChangeName").GetComponent<UIButton>();
		changeNameGo = this.transform.Find("ChangeNameBg").gameObject;
		changeNameGo.SetActive(false);
		nameInput = this.transform.Find("ChangeNameBg/NameInput").GetComponent<UIInput>();
		sureButton = this.transform.Find("ChangeNameBg/SureButton").GetComponent<UIButton>();
		cancelButton = this.transform.Find("ChangeNameBg/CancelButton").GetComponent<UIButton>();

		info = PlayerInfo._instance;
		PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;

		tween = this.GetComponent<TweenPosition>();

		EventDelegate ed_closeButton = new EventDelegate(this,"OnButtonCloseClick");
		closeButton.onClick.Add(ed_closeButton);
		EventDelegate ed_changeNameButton = new EventDelegate(this,"OnButtonChangeNameClick");
		changeNameButton.onClick.Add(ed_changeNameButton);
		EventDelegate ed_sureButton = new EventDelegate(this,"OnButtonSureClick");
		sureButton.onClick.Add(ed_sureButton);
		EventDelegate ed_cancelButton = new EventDelegate(this,"OnButtonCancelClick");
		cancelButton.onClick.Add(ed_cancelButton);
	}

	void Update()
	{
		if((info.Energy < 100) || (info.Toughen < 50))
		{
			UpdateEvergyAndToughenShow();
		}
	}

	void OnPlayerInfoChanged(InfoType type)
	{
		if(type == InfoType.All)
		{
			UpdateShow();
		}
	}

	void UpdateShow()
	{
		headSprite.spriteName = info.HeadPortrait.ToString();
		levelLabel.text = info.Level.ToString();
		nameLabel.text = info.Name.ToString();
		powerLabel.text = info.Power.ToString ();
		diamondLabel.text = info.Diamond.ToString();
		coinLabel.text = info.Coin.ToString();
		int requireExp = GameController.getRequireExpByLevel(info.Level + 1);
		expSlider.value = (float)info.Exp / requireExp;
		expLabel.text = info.Exp.ToString() + "/" + requireExp.ToString();

		UpdateEvergyAndToughenShow();
	}

	void UpdateEvergyAndToughenShow()
	{
		energyLabel.text = info.Energy.ToString() + "/100";
		if(info.Energy >= 100)
		{
			energyRestorePartLabel.text = "00:00:00";
			energyRestoreAllLabel.text = "00:00:00";
		}
		else
		{
			int remainTime = 60 - (int)info.energyTime;
			string str = (remainTime<10)?"0"+remainTime:remainTime.ToString();
			energyRestorePartLabel.text = "00:00:" + str;

			int minutes = 100-info.Energy-1;
			int hours = minutes / 60;
			minutes = minutes % 60;
			string hoursStr = (hours<10)?"0"+hours:hours.ToString();
			string minutesStr = (minutes<10)?"0"+minutes:minutes.ToString();
			energyRestoreAllLabel.text = hoursStr + ":" + minutesStr +  ":" + str;
		}

		toughenLabel.text = info.Toughen.ToString() + "/50";
		if(info.Toughen >= 100)
		{
			toughenRestorePartLabel.text = "00:00:00";
			toughenRestoreAllLabel.text = "00:00:00";
		}
		else
		{
			int remainTime = 60 - (int)info.toughenTime;
			string str = (remainTime<10)?"0"+remainTime:remainTime.ToString();
			toughenRestorePartLabel.text = "00:00:" + str;
			
			int minutes = 50-info.Toughen-1;
			int hours = minutes / 60;
			minutes = minutes % 60;
			string hoursStr = (hours<10)?"0"+hours:hours.ToString();
			string minutesStr = (minutes<10)?"0"+minutes:minutes.ToString();
			toughenRestoreAllLabel.text = hoursStr + ":" + minutesStr + ":" + str;
		}
	}

	public void Show()
	{
		tween.PlayForward();
	}
	public void OnButtonCloseClick()
	{
		tween.PlayReverse();
	}
	public void OnButtonChangeNameClick()
	{
		changeNameGo.SetActive(true);
	}
	public void OnButtonSureClick()
	{
		//1.联网校验名字是否重复
		//TODO
		info.ChangeName(nameInput.value);
		changeNameGo.SetActive(false);
	}
	public void OnButtonCancelClick()
	{
		changeNameGo.SetActive(false);
	}
}















































