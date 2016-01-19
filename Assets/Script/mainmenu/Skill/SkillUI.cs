using UnityEngine;
using System.Collections;

public class SkillUI : MonoBehaviour {

	private UILabel skillNameLabel;
	private UILabel skillDesLabel;
	private UIButton closeButton;
	private UIButton upgradeButton;
	private UILabel upgradeButtonLabel;

	private Skill skill;
	private TweenPosition tween;

	public static SkillUI _instance;

	void Awake () {
		_instance = this;

		skillNameLabel = transform.Find("Bg/SkillNameLabel").GetComponent<UILabel>();
		skillDesLabel = transform.Find("Bg/DesLabel").GetComponent<UILabel>();
		closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
		upgradeButton = transform.Find("UpgradeButton").GetComponent<UIButton>();
		upgradeButtonLabel = transform.Find("UpgradeButton/Label").GetComponent<UILabel>();
		tween = this.GetComponent<TweenPosition>();

		skillNameLabel.text = "";
		skillDesLabel.text = "";
		SetUpgradeButtonState(false,"选择技能");

		EventDelegate ed = new EventDelegate(this, "OnUpgrade");
		this.upgradeButton.onClick.Add(ed);
		EventDelegate ed1 = new EventDelegate(this, "OnClose");
		this.closeButton.onClick.Add(ed1);
	}
	
	void SetUpgradeButtonState(bool able=false, string label="")
	{
		upgradeButton.enabled = able;
		UIButtonColor.State state = (able?UIButtonColor.State.Normal:UIButtonColor.State.Disabled);
		upgradeButton.SetState(state,true);
		if(label != "")
		{
			upgradeButtonLabel.text = label;
		}
	}

	void OnSkillClick(Skill skill)
	{
		PlayerInfo info = PlayerInfo._instance;
		
		this.skill = skill;
		this.skillNameLabel.text = this.skill.Name + " Lv." + this.skill.Level;
		this.skillDesLabel.text = "当前技能的攻击力为: " + (this.skill.Damage*this.skill.Level) + "\n下一级技能的攻击力为: " + (this.skill.Damage*(this.skill.Level+1)) 
			+ "\n升级所需要的金币数: " + (500*(skill.Level+1));
		if((500*(this.skill.Level+1)) <= info.Coin)
		{
			if(this.skill.Level < info.Level)
			{
				SetUpgradeButtonState(true, "升级");
			}
			else
			{
				SetUpgradeButtonState(false, "最大等级");
			}
		}
		else
		{
			SetUpgradeButtonState(false, "金币不足");
		}

	}

	void OnUpgrade()
	{
		PlayerInfo info = PlayerInfo._instance;

//		if(this.skill.Level < info.Level)
//		{
			int coinNeed = 500*(this.skill.Level+1);
			bool isSuccess = info.GetCoin(coinNeed);
			if(isSuccess)
			{
				skill.Upgrade();
				OnSkillClick(this.skill);
			}
//			else
//			{
//				SetUpgradeButtonState(false, "金币不足");
//			}
//		}
//		else
//		{
//			SetUpgradeButtonState(false, "最大等级");
//		}
	}

	void OnClose()
	{
		Hide();
	}

	public void Show()
	{
		tween.PlayForward();
	}
	public void Hide()
	{
		tween.PlayReverse();
	}
}
