using UnityEngine;
using System.Collections;
using TaidouCommon;
using TaidouCommon.Model;
using LitJson;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class RegisterController : ControllerBase {

	private StartmenuController controller;
	private User user;

	public void Register(string username, string password, StartmenuController controller)
	{
		this.controller = controller;
		
		user = new User(){Username = username, Password = password};
		string json = JsonMapper.ToJson(user);
		Dictionary<byte, object> parameters = new Dictionary<byte, object>();
		parameters.Add((byte)ParameterCode.User, json);
		PhotonEngine.Instance.SendRequest(OperationCode.Register, parameters);
	}

	#region implemented abstract members of ControllerBase

	public override void OnOperationResponse (OperationResponse response)
	{
		switch(response.ReturnCode)
		{
		case (short)ReturnCode.Success:
			controller.HideRegisterPanel();
			controller.ShowStartPanel();
			controller.usernameLabelStart.text = user.Username;
			MessageManager._instance.ShowMessage("注册成功!", 2);
			break;
		case (short)ReturnCode.Fail:
			MessageManager._instance.ShowMessage(response.DebugMessage, 2);
			break;
		}
	}

	public override OperationCode OpCode {
		get {
			return OperationCode.Register;
		}
	}

	#endregion
}
