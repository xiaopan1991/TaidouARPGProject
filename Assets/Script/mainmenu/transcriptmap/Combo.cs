using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour {

	public static Combo _instance;
	public float comboTime = 2;
	private int comboCount = 0;
	private float timer = 0;
	private UILabel numberLabel;

	void Awake()
	{
		_instance = this;
		this.gameObject.SetActive(false);
		numberLabel = transform.Find("NumberLabel").GetComponent<UILabel>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			this.gameObject.SetActive(false);
			comboCount = 0;
		}
	}

	public void ComboPlus()
	{
		this.gameObject.SetActive(true);
		timer = comboTime;
		comboCount++;
		numberLabel.text = comboCount.ToString();
		transform.localScale = Vector3.one;
		iTween.ScaleTo(this.gameObject, new Vector3(1.5f,1.5f,1.5f), 0.1f);
		iTween.ShakePosition(this.gameObject, new Vector3(0.15f,0.15f,0.15f), 0.15f);
	}
}
