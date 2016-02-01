using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using TaidouCommon;
using TaidouCommon.Model;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener
{
	public ConnectionProtocol protocol = ConnectionProtocol.Tcp;
	private string serverAddress = "192.168.1.107:4530";
	private string applicationName = "TaidouServer";

	public delegate void OnConnectedToServerEvent();
	public event OnConnectedToServerEvent OnConnectedToServer;

	private PhotonPeer peer;
	private bool isConnected = false;
	private static PhotonEngine _instance;
	private Dictionary<byte, ControllerBase> controllers = new Dictionary<byte, ControllerBase>();

	public Role role;


	public static PhotonEngine Instance
	{
		get{return _instance;}
	}

	void Awake () {
		_instance = this;
		peer = new PhotonPeer(this, protocol);
		peer.Connect(serverAddress, applicationName);
		DontDestroyOnLoad(this.gameObject);
	}

	void Update () {
		if(peer != null)
		{
			peer.Service();
		}
	}

	public void RegisterController(OperationCode opCode, ControllerBase controller)
	{
		if(opCode == OperationCode.Role)
		{
//			Debug.Log("0000000000000");
		}
		if(controllers.ContainsKey((byte)opCode))
		{
//			Debug.Log("opCode: " + opCode);
//			Debug.Log("22222222222");
		}
		else
		{
			controllers.Add((byte)opCode, controller);
		}
	}
	public void UnRegisterController(OperationCode opCode)
	{
		controllers.Remove((byte)opCode);
	}

	//向服务器发送请求
	public void SendRequest(OperationCode opCode, Dictionary<byte, object> parameters)
	{
		peer.OpCustom((byte)opCode, parameters, true);
	}
	public void SendRequest(OperationCode opCode, SubCode subCode, Dictionary<byte, object> parameters)
	{
		parameters.Add((byte)ParameterCode.SubCode, subCode);
		peer.OpCustom((byte)opCode, parameters, true);
	}

	public void DebugReturn (DebugLevel level, string message)
	{
		print("level: " + level + "|||" + "message: " + message);
	}

	//服务器相应请求
	public void OnOperationResponse (OperationResponse operationResponse)
	{
		ControllerBase controller;
		controllers.TryGetValue(operationResponse.OperationCode, out controller);
		if(controller != null)
		{
			controller.OnOperationResponse(operationResponse);
		}
		else
		{
			print("Received a unknow response. OperationCode: " + operationResponse.OperationCode);
		}
	}
	public void OnStatusChanged (StatusCode statusCode)
	{
		Debug.Log("OnStatusChange: " + statusCode);
		switch(statusCode)
		{
		case StatusCode.Connect:
			isConnected = true;
			OnConnectedToServer();//网络连接成功后初始化调用，并请求服务器数据
			break;
		default:
			isConnected = false;
			break;
		}
	}
	public void OnEvent (EventData eventData)
	{
		//throw new System.NotImplementedException ();
	}
}
