using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon;
using TaidouCommon.Model;
using LitJson;
using ExitGames.Client.Photon;

public class LoginController : ControllerBase {

	private RoleController roleController;

	public override void Start ()
	{
		base.Start ();
		roleController = this.GetComponent<RoleController>();
	}

	public void Login(string username, string password)
	{
		User user = new User(){Username = username, Password = password};
		string json = JsonMapper.ToJson(user);
		Dictionary<byte, object> parameters = new Dictionary<byte, object>();
		parameters.Add((byte)ParameterCode.User, json);

		PhotonEngine.Instance.SendRequest(OperationCode.Login, parameters);
	}

	#region implemented abstract members of ControllerBase

	public override void OnOperationResponse (OperationResponse response)
	{
		switch(response.ReturnCode)
		{
		case (short)ReturnCode.Success:
			StartmenuController._instance.HideStartPanel();
			roleController.GetRole();
			break;
		case (short)ReturnCode.Fail:
			MessageManager._instance.ShowMessage(response.DebugMessage, 2);
			break;
		}
	}

	public override TaidouCommon.OperationCode OpCode {
		get {
			return OperationCode.Login;
		}
	}

	#endregion
}
