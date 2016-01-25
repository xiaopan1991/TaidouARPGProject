using UnityEngine;
using System.Collections;

public class BloodScene : MonoBehaviour {

	private static BloodScene _instance;
	private UISprite sprite;
	private TweenAlpha tweenAlpha;

	public static BloodScene Instance
	{
		get{return _instance;}
	}

	void Awake()
	{
		_instance = this;
		sprite = this.GetComponent<UISprite>();
		tweenAlpha = this.GetComponent<TweenAlpha>();
	}

	public void Show()
	{
		sprite.alpha = 1;
		tweenAlpha.ResetToBeginning();
		tweenAlpha.PlayForward();
	}
}
