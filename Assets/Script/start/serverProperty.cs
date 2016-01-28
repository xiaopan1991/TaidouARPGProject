 using UnityEngine;
using System.Collections;

public class serverProperty : MonoBehaviour {

	public string m_ip = "127.0.0.1:9080";

	private string _name;
	public string m_name
	{
		set{
			transform.Find("Label").GetComponent<UILabel>().text = value;
			_name = value;
		}
		get
		{
			return _name;
		}
	}
	public int m_count = 100;

	public Color m_color{
		get{
			return transform.Find("Label").GetComponent<UILabel>().color;
		}
		set{
			transform.Find("Label").GetComponent<UILabel>().color = value;
		}
	}

	public void OnPress(bool isPress)
	{
		if(isPress)
		{
			//select the current server
			transform.root.SendMessage("OnServerSelect", this.gameObject);
		}
	}

}
