using UnityEngine;
using System.Collections;

//NPCDialogUI
public class NPCDialogUI : MonoBehaviour {

	private TweenPosition tween;
	private UILabel npcTalkLabel;
	private UIButton acceptButton;

	public static NPCDialogUI _instance;

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		tween = this.GetComponent<TweenPosition>();
		npcTalkLabel = transform.Find("Label").GetComponent<UILabel>();
		acceptButton = transform.Find("AcceptButton").GetComponent<UIButton>();

		EventDelegate ed1 = new EventDelegate(this, "OnAccept");
		acceptButton.onClick.Add(ed1);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show(string npcTalk)
	{
		npcTalkLabel.text = npcTalk;
		tween.PlayForward();
	}
	public void Hide()
	{
		tween.PlayReverse();
	}
	void OnAccept()
	{
		TaskManager._instance.OnAcceptTask();
		Hide();
	}
}
