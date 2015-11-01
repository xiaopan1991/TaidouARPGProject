using UnityEngine;
using System.Collections;
using GameCommon;
using ExitGames.Client.Photon;

public abstract class ControllerBase : MonoBehaviour {

	public OperationCode opCode;

	void Start()
	{
		PhotonEngine.Instance.RegisterController(opCode, this);
	}

	public abstract void OnOperationResponse(OperationResponse response);

}
