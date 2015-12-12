using UnityEngine;
using System.Collections;

public enum TaskType
{
	Main,
	Reward,
	Daily
}

public enum TaskProgress
{
	NoStart,
	Accept,
	Complete,
	Reward
}

public class Task 
{
	public delegate void OnTaskChangeEvent();
	public event OnTaskChangeEvent OnTaskChange;

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
