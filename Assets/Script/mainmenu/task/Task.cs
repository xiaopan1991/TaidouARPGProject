using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
using System;

public enum TaskType
{
	Main = 0,
	Reward = 1,
	Daily= 2
}

public enum TaskProgress
{
	NoStart = 0,
	Accept = 1,
	Complete = 2,
	Reward = 3
}

public class Task 
{
	public delegate void OnTaskChangeEvent();
	public event OnTaskChangeEvent OnTaskChange;

	public TaskDB TaskDB{get; set;}

	//用来同步服务器传来的信息
	public void SyncTask(TaskDB taskDb)
	{
		TaskDB = taskDb;
		taskProgress = (TaskProgress)taskDb.State;
	}

	public void UpdateTask(TaskManager manager)
	{
		if(TaskDB == null)
		{
			TaskDB = new TaskDB();
			TaskDB.State = (int) taskProgress;
			TaskDB.TaskID = id;
			TaskDB.LastUpDateTime = new DateTime();
			TaskDB.Type = (int) taskType;
			manager.taskDBController.AddTaskDB(TaskDB);
		}
		else
		{
			this.TaskDB.State = (int) taskProgress;
			manager.taskDBController.UpdateTaskDB(this.TaskDB);
		}
	}

	private int id;
	public int Id
	{
		get{return id;}
		set{id = value;}
	}

	private string name;
	public string Name
	{
		get{return name;}
		set{name = value;}
	}

	private TaskType taskType;
	public TaskType TaskType
	{
		get{return taskType;}
		set{taskType = value;}
	}
	private string icon;
	public string Icon
	{
		get{return icon;}
		set{icon = value;}
	}
	private string des;
	public string Des
	{
		get{return des;}
		set{des = value;}
	}
	private int coin;
	public int Coin
	{
		get{return coin;}
		set{coin=value;}
	}
	private int diamond;
	public int Diamond
	{
		get{return diamond;}
		set{diamond = value;}
	}
	private string talkNpc;
	public string TalkNpc
	{
		get{return talkNpc;}
		set{talkNpc = value;}
	}
	private int idNpc;
	public int IdNpc
	{
		get{return idNpc;}
		set{idNpc = value;}
	}
	private int idTranscript;
	public int IdTranscript
	{
		get{return idTranscript;}
		set{idTranscript = value;}
	}
	private TaskProgress taskProgress = TaskProgress.NoStart;
	public TaskProgress TaskProgress
	{
		get{return taskProgress;}
		set
		{
			if(taskProgress != value)
			{
				taskProgress = value;
				OnTaskChange();
			}
		}
	}
}
