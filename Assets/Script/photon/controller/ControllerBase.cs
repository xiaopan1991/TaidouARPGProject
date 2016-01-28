using UnityEngine;
using System.Collections;
using TaidouCommon;
using ExitGames.Client.Photon;

public abstract class ControllerBase : MonoBehaviour {

//	public OperationCode opCode;
	public abstract OperationCode OpCode {get;}

	public virtual void Start()
	{
		PhotonEngine.Instance.RegisterController(OpCode, this);
	}

	public virtual void OnDestory()
	{
		PhotonEngine.Instance.UnRegisterController(OpCode);
	}

	public abstract void OnOperationResponse(OperationResponse response);

}
