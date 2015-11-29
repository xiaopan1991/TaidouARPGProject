using UnityEngine;
using System.Collections;

public class MessageManager : MonoBehaviour {

	public static MessageManager _instance;
	private UILabel messageLabel;
	private TweenAlpha tween;
	private bool isSetActive = true;
	
	void Awake()
	{
		_instance = this;
		messageLabel = this.transform.Find("Label").GetComponent<UILabel>();
		tween = this.GetComponent<TweenAlpha>();

		EventDelegate ed = new EventDelegate(this, "OnTweenFinished");
		tween.onFinished.Add(ed);

		gameObject.SetActive(false);
	}

	public void ShowMessage(string message, float time = 1)
	{
		gameObject.SetActive(true);
		StartCoroutine(Show(message,time));
	}

	IEnumerator Show(string message, float time)
	{
		tween.PlayForward();
		messageLabel.text = message;
		isSetActive = true;
		yield return new WaitForSeconds(time);

		tween.PlayReverse();
		isSetActive = false;
	}

	public void OnTweenFinished()
	{
		if(!isSetActive)
		{
			gameObject.SetActive(false);
		}
	}
}
