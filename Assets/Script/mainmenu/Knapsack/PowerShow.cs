using UnityEngine;
using System.Collections;

public class PowerShow : MonoBehaviour 
{
	private float starValue = 0;
	private int endValue = 0;
	private bool isStart = false;
	private UILabel numLabel;
	private bool isUp = true;
	private TweenAlpha tween;

	private int speed = 1000;

	void Awake()
	{
		numLabel = transform.Find("Label").GetComponent<UILabel>();
	
		EventDelegate ed = new EventDelegate(this, "OnTweenFinished");
		tween = this.GetComponent<TweenAlpha>();
		tween.onFinished.Add(ed);

		gameObject.SetActive(false);
	}

	void Update()
	{
		if(isStart)
		{
			if(isUp)
			{
				starValue += speed * Time.deltaTime;
				if(starValue > endValue)
				{
					isStart = false;
					starValue = endValue;
					tween.PlayReverse();
				}
			}
			else
			{
				starValue -= speed * Time.deltaTime;
				if(starValue < endValue)
				{
					isStart = false;
					starValue = endValue;
					tween.PlayReverse();
				}
			}
			numLabel.text = (int)starValue + "";
		}
	}

	public void OnTweenFinished()
	{
		if(isStart == false)
		{
			gameObject.SetActive(false);
		}
	}

	public void ShowPowerChange(int starValue, int endValue)
	{
		gameObject.SetActive(true);
		tween.PlayForward();

		this.starValue = starValue;
		this.endValue = endValue;
		if(endValue > starValue)
		{
			isUp = true;
		}
		else
		{
			isUp = false;
		}
		isStart = true;
	}

}
