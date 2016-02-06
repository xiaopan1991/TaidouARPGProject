using UnityEngine;
using System.Collections;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class InventoryItemDBController : ControllerBase {

	public void GetInventoryItemDB()
	{
		PhotonEngine.Instance.SendRequest(OpCode, SubCode.GetInventoryItemDB, new Dictionary<byte, object>());
	}
	public void AddInventoryItemDB(InventoryItemDB itemDB)
	{
		itemDB.Role = null;
		Dictionary<byte, object> parameters = new Dictionary<byte, object>();
		ParameterTool.AddParameter(parameters, ParameterCode.InventoryItemDB, itemDB);
		PhotonEngine.Instance.SendRequest(OpCode, SubCode.AddInventoryItemDB, parameters);
	}

	public void UpdateInventoryItemDB(InventoryItemDB itemDB)
	{
		itemDB.Role = null;
		Dictionary<byte, object> parameters = new Dictionary<byte, object>();
		ParameterTool.AddParameter(parameters, ParameterCode.InventoryItemDB, itemDB);
		PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateInventoryItemDB, parameters);
	}

	#region implemented abstract members of ControllerBase

	public override void OnOperationResponse (OperationResponse response)
	{
		SubCode subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode, false);
		switch(subCode)
		{
		case SubCode.GetInventoryItemDB:
			List<InventoryItemDB> list = ParameterTool.GetParameter<List<InventoryItemDB>>(response.Parameters, ParameterCode.InventoryItemDBList);
			if(OnGetInventoryItemDBList != null)
			{
				OnGetInventoryItemDBList(list);
			}
			break;
		case SubCode.AddInventoryItemDB:
			if(OnAddInventoryItemDB != null)
			{
				InventoryItemDB itemDB = ParameterTool.GetParameter<InventoryItemDB>(response.Parameters, ParameterCode.InventoryItemDB);
				OnAddInventoryItemDB(itemDB);
			}
			break;

		case SubCode.UpdateInventoryItemDB:
			if(OnUpdateInventoryItemDB != null)
			{
				OnUpdateInventoryItemDB();
			}
			break;
		}
	}

	public override OperationCode OpCode {
		get {
			return OperationCode.InventoryItemDB;
		}
	}

	#endregion

	public event OnGetInventoryItemDBListEvent OnGetInventoryItemDBList;
	public event OnAddInventoryItemDBEvent OnAddInventoryItemDB;
	public event OnUpdateInventoryItemDBEvent OnUpdateInventoryItemDB;

}
