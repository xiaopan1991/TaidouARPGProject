using UnityEngine;
using System.Collections;

public class KnapsackRole : MonoBehaviour {

	private UISprite helmSprite;
	private UISprite clothSprite;
	private UISprite weaponSprite;
	private UISprite shoesSprite;
	private UISprite necklaceSprite;
	private UISprite braceletSprite;
	private UISprite ringSprite;
	private UISprite wingSprite;

	private UILabel hpLabel;
	private UILabel damageLabel;
	private UILabel expLabel;
	private UISlider expSlider;

	void Awake()
	{
		helmSprite = this.transform.Find("HelmSprite").GetComponent<UISprite>();
		clothSprite = this.transform.Find("ClothSprite").GetComponent<UISprite>();
		weaponSprite = this.transform.Find("WeaponSprite").GetComponent<UISprite>();
		shoesSprite = this.transform.Find("ShoesSprite").GetComponent<UISprite>();
		necklaceSprite = this.transform.Find("NecklaceSprite").GetComponent<UISprite>();
		braceletSprite = this.transform.Find("BraceletSprite").GetComponent<UISprite>();
		ringSprite = this.transform.Find("RingSprite").GetComponent<UISprite>();
		wingSprite = this.transform.Find("WingSprite").GetComponent<UISprite>();

		hpLabel = this.transform.Find("HpBg/Label").GetComponent<UILabel>();
		damageLabel = this.transform.Find("DamageBg/Label").GetComponent<UILabel>();
		expLabel = this.transform.Find("ExpSlider/Label").GetComponent<UILabel>();
		expSlider = this.transform.Find("ExpSlider").GetComponent<UISlider>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
