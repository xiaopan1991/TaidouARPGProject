using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon;
using ExitGames.Client.Photon;
using TaidouCommon.Model;
using TaidouCommon.Tools;

public class TaskDBController : ControllerBase 
{
	#region implemented abstract members of ControllerBase

	public override void OnOperationResponse (OperationResponse response)
	{
		SubCode subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode,false);
		switch(subCode)
		{
		case SubCode.GetTaskDB:
			List<TaskDB> list = ParameterTool.GetParameter< List<TaskDB> >(response.Parameters, ParameterCode.TaskDBList);
			if(OnGetTaskDBList != null)
			{
				OnGetTaskDBList(list);
			}
			break;
		case SubCode.AddTaskDB:
			TaskDB taskDB = ParameterTool.GetParameter<TaskDB>(response.Parameters, ParameterCode.TaskDB);
			if(OnAddTaskDB != null)
			{
				OnAddTaskDB(taskDB);
			}
			break;
		case SubCode.UpdateTaskDB:
			if(OnUpdateTaskDB != null)
			{
				OnUpdateTaskDB();
			}
			break;
		}
	}

	public override OperationCode OpCode {
		get {
			return OperationCode.TaskDB;
		}
	}

	#endregion

	public void GetTaskDBList()
	{
		PhotonEngine.Instance.SendRequest(OpCode, SubCode.GetTaskDB, new Dictionary<byte, object>());
	}

	public void AddTaskDB(TaskDB taskDB)
	{
		Dictionary<byte, object> parameters = new Dictionary<byte, object>();
		taskDB.Role = null;
		ParameterTool.AddParameter(parameters, ParameterCode.TaskDB, taskDB);
		PhotonEngine.Instance.SendRequest(OpCode, SubCode.AddTaskDB, parameters);
	}

	public void UpdateTaskDB(TaskDB taskDB)
	{
		Dictionary<byte, object> parameters = new Dictionary<byte, object>();
		taskDB.Role = null;
		ParameterTool.AddParameter(parameters, ParameterCode.TaskDB, taskDB);
		PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateTaskDB, parameters);
	}

	public event OnGetTaskDBListEvent OnGetTaskDBList;
	public event OnAddTaskDBEvent OnAddTaskDB;
	public event OnUpdateTaskDBEvent OnUpdateTaskDB;

	public override void OnDestory()
	{
		base.OnDestory();
	}
}
