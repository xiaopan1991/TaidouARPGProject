using UnityEngine;
using System.Collections;

public class SkillButton : MonoBehaviour {

	public PosType posType = PosType.Basic;
	public float coldTime = 4;
	private float coldTimer = 0;
	private UISprite maskSprite;
	private UIButton btn;
	private BoxCollider collider;

	private PlayerAnimation playerAnimation;

	void Start()
	{
		playerAnimation = TranscriptManager._instance.Player.GetComponent<PlayerAnimation>();
		if(transform.Find("Mask"))
		{
			maskSprite = transform.Find("Mask").GetComponent<UISprite>();
		}
		btn = this.GetComponent<UIButton>();
		this.collider = this.GetComponent<BoxCollider>();
	}

	void Update()
	{
		if(maskSprite == null)
			return;
		if(coldTimer > 0)
		{
			coldTimer -= Time.deltaTime;
			maskSprite.fillAmount = coldTimer/coldTime;
			if(coldTimer <=0)
			{
				Enable();
			}
		}
		else
		{
			maskSprite.fillAmount = 0;
		}
	}

	void OnPress(bool isPress)
	{
		playerAnimation.OnAttackButtonClick(isPress, posType);
		if(isPress && posType!=PosType.Basic)
		{
			coldTimer = coldTime;
			Disable();
		}
	}
	void Disable()
	{
		this.collider.enabled = false;
		this.btn.SetState(UIButtonColor.State.Normal,true);
	}
	void Enable()
	{
		this.collider.enabled = true;
	}
}
