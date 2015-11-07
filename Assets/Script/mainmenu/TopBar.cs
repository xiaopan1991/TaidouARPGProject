using UnityEngine;
using System.Collections;

public class TopBar : MonoBehaviour {

	private UILabel coinLabel;
	private UILabel diamondLabel;
	private UIButton coinPlusButton;
	private UIButton diamondPlusButton;

	void Awake()
	{
		coinLabel = this.transform.Find("CoinBg/Label").GetComponent<UILabel>();
		coinPlusButton = this.transform.Find("CoinBg/PlusButton").GetComponent<UIButton>();
		diamondLabel = this.transform.Find("DiamondBg/Label").GetComponent<UILabel>();
		diamondPlusButton = this.transform.Find("DiamondBg/PlusButton").GetComponent<UIButton>();

		PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
	}

	void OnDistory()
	{
		PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
	}

	void OnPlayerInfoChanged(InfoType type)
	{
		if(type == InfoType.Coin || type == InfoType.Diamond || type== InfoType.All)
		{
			UpdateShow();
		}
	}
	void UpdateShow()
	{
		PlayerInfo info = PlayerInfo._instance;
		coinLabel.text = info.Coin.ToString();
		diamondLabel.text = info.Diamond.ToString();
	}

}
