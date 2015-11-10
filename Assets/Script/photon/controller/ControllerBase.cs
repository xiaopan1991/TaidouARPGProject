using UnityEngine;
using System.Collections;
using GameCommon;
using ExitGames.Client.Photon;

public abstract class ControllerBase : MonoBehaviour {

	public OperationCode opCode;

	public virtual void Start()
	{
		PhotonEngine.Instance.RegisterController(opCode, this);
	}

	public virtual void OnDestory()
	{
		PhotonEngine.Instance.UnRegisterController(opCode);
	}

	public abstract void OnOperationResponse(OperationResponse response);

}
