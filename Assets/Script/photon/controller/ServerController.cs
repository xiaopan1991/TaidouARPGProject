using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaidouCommon;
using LitJson;

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
		Dictionary<byte, object> parameters = response.Parameters;
		object jsonObject = null;
		parameters.TryGetValue((byte)ParameterCode.ServerList, out jsonObject);
		List<TaidouCommon.Model.ServerProperty> serverList = JsonMapper.ToObject<List<TaidouCommon.Model.ServerProperty>>(jsonObject.ToString());

		StartmenuController._instance.InitServerListFromServer(serverList);

	}

	public override void OnDestory()
	{
		base.OnDestory();
		PhotonEngine.Instance.OnConnectedToServer -= GetServerList;
	}
	public override OperationCode OpCode
	{
		get {return OperationCode.GetServer;}
	}
}
