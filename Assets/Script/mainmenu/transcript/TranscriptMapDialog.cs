using UnityEngine;
using System.Collections;

public class TranscriptMapDialog : MonoBehaviour {

	private TweenScale tween;
	private UILabel desLabel;
	private UILabel energyTagLabel;
	private UILabel energyLabel;
	private UIButton enterButton;
	private UIButton closeButton;

	void Awake()
	{
		tween = this.transform.GetComponent<TweenScale>();
		
		desLabel = this.transform.Find("Sprite/DesLabel").GetComponent<UILabel>();
		energyTagLabel = this.transform.Find("Sprite/EnergyTagLabel").GetComponent<UILabel>();
		energyLabel = this.transform.Find("Sprite/EnergyLabel").GetComponent<UILabel>();
		enterButton = this.transform.Find("BtnEnter").GetComponent<UIButton>();
		closeButton = this.transform.Find("BtnClose").GetComponent<UIButton>();

		EventDelegate ed1 = new EventDelegate(this, "OnEnter");
		enterButton.onClick.Add(ed1);
		EventDelegate ed2 = new EventDelegate(this, "OnClose");
		closeButton.onClick.Add(ed2);
	}

	public void ShowWarn()
	{
		energyLabel.enabled = false;
		energyTagLabel.enabled = false;
		enterButton.enabled = false;

		desLabel.text = "当前等级无法进入改地下城";
		Show();
	}

	public void ShowDialog(BtnTranscript transcript)
	{
		energyLabel.enabled = true;
		energyTagLabel.enabled = true;
		enterButton.enabled = true;

		desLabel.text = transcript.des;
		energyLabel.text = "3";
		Show();
	}

	public void Show()
	{
		tween.PlayForward();
	}
	public void Hide()
	{
		tween.PlayReverse();
	}

	void OnEnter()
	{
		transform.parent.SendMessage("OnEnter");
	}
	void OnClose()
	{
		Hide();
	}

}
