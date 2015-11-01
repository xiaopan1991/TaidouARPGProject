using UnityEngine;
using System.Collections;

public class StartmenuController : MonoBehaviour {

	public static StartmenuController _instance;

	public TweenScale startpanelTween;
	public TweenScale loginpanelTween;
	public TweenScale registerpanelTween;
	public TweenScale serverpanelTween;
	public TweenPosition startpanelTweenPos;
	public TweenPosition characterselectTweenPos;
	public TweenPosition charactershowTweenPos;

	public UIInput usernameInputLogin;
	public UIInput passwordInputLogin;
	public UIInput usernameInputRegister;
	public UIInput passwordInputRegister;
	public UIInput repasswordInputRegister;
	public UIInput characternameInput;

	public static string username;
	public static string password;
	public static serverProperty sp;

	public UILabel usernameLabelStart;
	public UILabel servernameLabelStart;

	private bool haveInitServerlist = false;

	public UIGrid serverlistGrid;
	public GameObject serveritemRed;
	public GameObject serveritemGreen;

	public GameObject serverSelectedGo;

	public GameObject[] characterArray;
	public GameObject[] characterSelectedArray;

	private GameObject characterSelected;
	public Transform characterSelectedParent; 

	public UILabel nameLabelCharacterselect;
	public UILabel levelLabelCharacterselect;

	void Start()
	{
		InitServerList();
	}

	void Awake()
	{
		_instance = this;
	}

	public void OnUsernameClick()
	{
		startpanelTween.PlayForward();
		StartCoroutine(HidePanel(startpanelTween.gameObject));

		loginpanelTween.gameObject.SetActive(true);
		loginpanelTween.PlayForward();
	}

	public void OnServerClick()
	{
		startpanelTween.PlayForward();
		StartCoroutine(HidePanel(startpanelTween.gameObject));

		serverpanelTween.gameObject.SetActive(true);
		serverpanelTween.PlayForward();
	}

	public void OnEnterGameClick()
	{
		//1.连接服务器，验证用户名和密码
		//TODO
		
		//2.进入角色选择界面
		//TODO
		startpanelTweenPos.PlayForward();
		HidePanel(startpanelTweenPos.gameObject);
		characterselectTweenPos.gameObject.SetActive(true);
		characterselectTweenPos.PlayForward();
	}

	IEnumerator HidePanel(GameObject go)
	{
		yield return new WaitForSeconds(0.4f);
		go.SetActive(false);
	}

	public void OnLoginClick()
	{
		username = usernameInputLogin.value;
		password = passwordInputLogin.value;

		//返回开始界面
		loginpanelTween.PlayReverse();
		StartCoroutine(HidePanel(loginpanelTween.gameObject));
		startpanelTween.gameObject.SetActive(true);
		startpanelTween.PlayReverse();

		usernameLabelStart.text = username;
	}

	public void OnRegisterShowClick()
	{
		Debug.Log("OnRegisterShowClick");
		loginpanelTween.PlayReverse();
		StartCoroutine(HidePanel(loginpanelTween.gameObject));
		registerpanelTween.gameObject.SetActive(true);
		registerpanelTween.PlayForward();
	}

	public void OnLoginCloseClick()
	{
		//返回开始界面
		loginpanelTween.PlayReverse();
		StartCoroutine(HidePanel(loginpanelTween.gameObject));
		startpanelTween.gameObject.SetActive(true);
		startpanelTween.PlayReverse();
	}

	public void OnCancelClick()
	{
		registerpanelTween.PlayReverse();
		StartCoroutine(HidePanel(registerpanelTween.gameObject));
		loginpanelTween.gameObject.SetActive(true);
		loginpanelTween.PlayForward();
	}

	public void OnRegisterCloseClick()
	{
		OnCancelClick();
	}

	public void OnRegisterAndLoginClick()
	{
		username = usernameInputRegister.value;
		password = passwordInputRegister.value;

		registerpanelTween.PlayReverse();
		StartCoroutine(HidePanel(registerpanelTween.gameObject));
		startpanelTween.gameObject.SetActive(true);
		startpanelTween.PlayReverse();

		usernameLabelStart.text = username;
	}

	public void InitServerList()
	{
		if(haveInitServerlist)
		{	
			return;
		}
		else
		{ 
			//1.from server to get serverlist info
			//TODO
			//2.uer serverlist infp to init serverlist

			for(int i=0;i<20;i++)
			{
				string ip = "127.0.0.1:9080";
				string name = string.Format("{0}区 马达加斯加", (i+1));
				int count = Random.Range(0,100);

				GameObject go = null;
				if(count > 70)
				{
					//red
					go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemRed);
				}
				else
				{
					//green
					go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemGreen);
				}
				serverProperty sp = go.GetComponent<serverProperty>();
				sp.m_name = name;
				sp.m_ip = ip;
				sp.m_count = count;
				serverlistGrid.AddChild(go.transform);
			}

			haveInitServerlist = true;
		}
	}

	public void OnServerSelect(GameObject serverGo)
	{
		sp = serverGo.GetComponent<serverProperty>();
		serverSelectedGo.GetComponent<UISprite>().spriteName = serverGo.GetComponent<UISprite>().spriteName;

		UILabel temp = serverSelectedGo.transform.Find("Label").GetComponent<UILabel>();
		temp.text = sp.m_name;
		temp.color = sp.m_color;
		//Debug.Log("Color: " + sp.m_color);
	}

	public void OnServerSelectedClick()
	{
		serverpanelTween.PlayReverse();
		StartCoroutine(HidePanel(serverpanelTween.gameObject));

		startpanelTween.gameObject.SetActive(true);
		startpanelTween.PlayReverse();

		servernameLabelStart.text = sp.m_name;
	}

	public void OnCharacterClick(GameObject go)
	{
		if(go == characterSelected)
		{
			return;
		}
		iTween.ScaleTo(go, new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
		if(characterSelected != null)
		{
			iTween.ScaleTo(characterSelected, new Vector3(1f, 1f, 1f), 0.5f);
		}
		characterSelected = go;

		/*if(characterSelected != null)
		{
			if(characterSelected == go)
			{
				iTween.ScaleTo(characterSelected, new Vector3(1f, 1f, 1f), 0.5f);
				characterSelected = null;
			}
			else
			{
				iTween.ScaleTo(go, new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
				iTween.ScaleTo(characterSelected, new Vector3(1f, 1f, 1f), 0.5f);
				characterSelected = go;
			}
		}
		else
		{
			iTween.ScaleTo(go, new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
			characterSelected = go;
		}*/

	}

	public void OnButtonChangecharacterClick()
	{
		characterselectTweenPos.PlayReverse();
		StartCoroutine(HidePanel(characterselectTweenPos.gameObject));

		charactershowTweenPos.gameObject.SetActive(true);
		charactershowTweenPos.PlayForward();
	}

	public void OnCharactershowButtonSureClick()
	{
		//1.check name is right;
		//TODO
		//2.check select character or not
		//TODO

		int index = -1;
		for(int i=0;i<characterArray.Length;i++)
		{
			if(characterSelected == characterArray[i])
			{
				index = i;
				break;
			}
		}
		if(index == -1)
		{
			return;
		}
//		Animation[] temp = characterSelectedParent.GetComponentsInChildren<Animation>();
//		Debug.Log (characterSelectedParent.FindChild("girl_select"));

//		GameObject.Destroy(characterSelectedParent.GetComponentInChildren<Animation>().gameObject);
		GameObject go = GameObject.Instantiate(characterSelectedArray[index], Vector3.zero, Quaternion.identity) as GameObject;
		go.transform.parent = characterSelectedParent;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = new Vector3(1f,1f,1f);

		nameLabelCharacterselect.text = characternameInput.value;
		levelLabelCharacterselect.text = "Lv.1";

		OnCharactershowButtonBackClick();
		Debug.Log("AAAAA:" + characterSelectedParent.childCount);
	}

	public void OnCharactershowButtonBackClick()
	{
		charactershowTweenPos.PlayReverse();
		StartCoroutine(HidePanel(charactershowTweenPos.gameObject));

		characterselectTweenPos.gameObject.SetActive(true);
		characterselectTweenPos.PlayForward();
	}
}















