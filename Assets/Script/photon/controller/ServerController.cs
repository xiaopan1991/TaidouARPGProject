using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using GameCommon;

public class ServerController : ControllerBase 
{

	public override void Start()
	{
		base.Start();
		PhotonEngine.Instance.OnConnectedToServer += GetServerList;
	}

	public void GetServerList()
	{
		PhotonEngine.Instance.SendRequest(OperationCode.GetServer,new Dictionary<byte, object>());
	}

	public override void OnOperationResponse(OperationResponse response)
	{

	}

	public override void OnDestory()
	{
		base.OnDestory();
		PhotonEngine.Instance.OnConnectedToServer -= GetServerList;
	}

}
