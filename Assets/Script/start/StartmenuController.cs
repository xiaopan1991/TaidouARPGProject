using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;

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
	public static string rePassword;
	public static serverProperty sp;
	public static List<Role> roleList = null;

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
	private LoginController loginController;
	private RegisterController registerController;
	private RoleController roleController;

	void Start()
	{
		//InitServerList();
	}

	public void OnGetRole(List<Role> roleList)
	{
		StartmenuController.roleList = roleList;
		if(roleList != null && roleList.Count > 0)
		{
			//进入角色显示的界面
			Role role = roleList[0] as Role;
		}
		else
		{
			//进入角色创建界面
			ShowRoleAddPanel();
		}
	}

	public void ShowRole(Role role)
	{
		PhotonEngine.Instance.role = role;
		ShowCharacterselect();

		nameLabelCharacterselect.text = role.Name;
		levelLabelCharacterselect.text = "Lv." + role.Level;

		int index = -1;
		for(int i=0;i<characterArray.Length;i++)
		{
			if((characterArray[i].name.IndexOf("boy")>0 && role.IsMan) || (characterArray[i].name.IndexOf("girl")>0 && !role.IsMan))
			{
				index = i;
				break;
			}
		}
		if(index == -1)
		{
			return;
		}
		GameObject go = GameObject.Instantiate(characterSelectedArray[index], Vector3.zero, Quaternion.identity) as GameObject;
		go.transform.parent = characterSelectedParent;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = new Vector3(1f,1f,1f);
	}
	public void OnAddRole(Role role)
	{
		if(roleList == null)
		{
			roleList = new List<Role>();
		}
		roleList.Add(role);
		ShowRole(role);
	}

	void Awake()
	{
		_instance = this;
		loginController = this.GetComponent<LoginController>();
		registerController = this.GetComponent<RegisterController>();
		roleController = this.GetComponent<RoleController>();

		roleController.OnGetRole += OnGetRole;
		roleController.OnAddRole += OnAddRole;
	}

	void OnDestroy()
	{
		if(roleController != null)
		{
			roleController.OnGetRole -= OnGetRole;
			roleController.OnAddRole -= OnAddRole;
		}
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
		loginController.Login(username, password);
		 
		//2.进入角色选择界面
		//TODO
	}

	public void HideStartPanel()
	{
		startpanelTweenPos.PlayForward();
		StartCoroutine(HidePanel(startpanelTweenPos.gameObject));
	}

	public void ShowCharacterselect()
	{
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
		rePassword = repasswordInputRegister.value;

		if(username == null || username.Length <= 3)
		{
			MessageManager._instance.ShowMessage("用户名不能少于三个字符");
			return;
		}
		if(password == null || password.Length <= 3)
		{
			MessageManager._instance.ShowMessage("密码不能少于三个字符");
			return;
		}
		if(password != rePassword)
		{
			MessageManager._instance.ShowMessage("密码输入不一致！",2);
			return;
		}

		registerController.Register(username, password, this);
	}

	public void HideRegisterPanel()
	{
		registerpanelTween.PlayReverse();
		StartCoroutine(HidePanel(registerpanelTween.gameObject));
	}

	public void ShowStartPanel()
	{
		startpanelTween.gameObject.SetActive(true);
		startpanelTween.PlayReverse();
	}

	public void InitServerListFromServer(List<TaidouCommon.Model.ServerProperty> list)
	{
		int index = 0;
		serverProperty spDefault = null;
		GameObject goDefault = null;
		
		foreach(TaidouCommon.Model.ServerProperty temp in list)
		{
			string ip = temp.IP;
			string name = temp.Name;
			int count = temp.Count;

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
			if(index == 0)
			{
				spDefault = sp;
				goDefault = go;
			}
			index++;

		}
		servernameLabelStart.text = spDefault.m_name;
		OnServerSelect(goDefault);
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

		foreach(var role in roleList)
		{
			if((role.IsMan && go.name.IndexOf("boy") > 0) || (!role.IsMan&&go.name.IndexOf("girl")>0))
			{
				characternameInput.value = role.Name;
			}
		}



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

	public void ShowRoleAddPanel()
	{
		charactershowTweenPos.gameObject.SetActive(true);
		charactershowTweenPos.PlayForward();
	}

	public void OnCharactershowButtonSureClick()
	{
		if(characternameInput.value.Length < 3)
		{
			MessageManager._instance.ShowMessage("角色的名字不能少于三个字符");
			return;
		}

		Role role = null;
		foreach(var roleTemp in roleList)
		{
			if((roleTemp.IsMan && characterSelected.name.IndexOf("boy") > 0) || (!roleTemp.IsMan&&characterSelected.name.IndexOf("girl")>0))
			{
				characternameInput.value = roleTemp.Name;
				role = roleTemp;
			}
		}

		if(role == null)
		{
			Role roleAdd = new Role();
			roleAdd.IsMan = characterSelected.name.IndexOf("boy")>0;
			roleAdd.Name = characternameInput.value;
			roleAdd.Level = 1;
			roleController.AddRole(roleAdd);
		}
		else
		{
			ShowRole(role);
		}

		OnCharactershowButtonBackClick();
	}

	public void OnCharactershowButtonBackClick()
	{
		charactershowTweenPos.PlayReverse();
		StartCoroutine(HidePanel(charactershowTweenPos.gameObject));

		characterselectTweenPos.gameObject.SetActive(true);
		characterselectTweenPos.PlayForward();
	}
}















