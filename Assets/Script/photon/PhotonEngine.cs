using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using GameCommon;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener
{
	public ConnectionProtocol protocol = ConnectionProtocol.Tcp;
	public string serverAddress = "127.0.0.1:4530";
	public string applicationName = "GameServer";

	public delegate void OnConnectedToServerEvent();
	public event OnConnectedToServerEvent OnConnectedToServer;

	private PhotonPeer peer;
	private bool isConnected = false;
	private static PhotonEngine _instance;
	private Dictionary<byte, ControllerBase> controllers = new Dictionary<byte, ControllerBase>();

	public static PhotonEngine Instance
	{
		get{return _instance;}
	}

	void Awake () {
		peer = new PhotonPeer(this, protocol);
		peer.Connect(serverAddress, applicationName);
		peer.Service();
	}

	void Update () {
		if(isConnected)
		{
			peer.Service();
		}
	}

	public void RegisterController(OperationCode opCode, ControllerBase controller)
	{
		controllers.Add((byte)opCode, controller);
	}
	public void UnRegisterController(OperationCode opCode)
	{
		controllers.Remove((byte)opCode);
	}

	public void SendRequest(OperationCode opCode, Dictionary<byte, object> parameters)
	{
		peer.OpCustom((byte)opCode, parameters, true);
	}

	public void DebugReturn (DebugLevel level, string message)
	{
		print("level: " + level + "|||" + "message: " + message);
	}
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
			OnConnectedToServer();
			break;
		default:
			isConnected = false;
			break;
		}
	}
	public void OnEvent (EventData eventData)
	{
		throw new System.NotImplementedException ();
	}
}
