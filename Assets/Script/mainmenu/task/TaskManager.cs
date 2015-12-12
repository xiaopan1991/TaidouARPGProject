using UnityEngine;
using System.Collections;

public class TaskManager : MonoBehaviour {

	public static TaskManager _instance;
	public TextAsset taskinfoText;
	private Task currentTask;
	private ArrayList taskList = new ArrayList();
	private PlayerAutoMove playerAutoMove;
	public PlayerAutoMove PlayerAutoMove
	{
		get{
			if(playerAutoMove == null)
			{
				playerAutoMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAutoMove>();
			}
			return playerAutoMove;
		}
	}

	void Awake()
	{
		_instance = this;
		InitTask();
	}



	/// <summary>
	/// 初始化任务信息
	/// </summary>
	public void InitTask()
	{
		string[] taskinfoArray = taskinfoText.ToString().Split('\n');
		foreach(string str in taskinfoArray)
		{
			string[] proArray = str.Split('|');
			Task task = new Task();
			task.Id = int.Parse(proArray[0]);
			switch(proArray[1])
			{
			case "Main":
				task.TaskType = TaskType.Main;
				break;
			case "Reward":
				task.TaskType = TaskType.Reward;
				break;
			case "Daily":
				task.TaskType = TaskType.Daily;
				break;
			}
			task.Name = proArray[2];
			task.Icon = proArray[3];
			task.Des = proArray[4];
			task.Coin = int.Parse(proArray[5]);
			task.Diamond = int.Parse(proArray[6]);
			task.TalkNpc = proArray[7];
			task.IdNpc = int.Parse(proArray[8]);
			task.IdTranscript = int.Parse(proArray[9]);
			taskList.Add(task);
		}
	}

	public ArrayList GetTaskList()
	{
		return taskList;
	}
	public void OnExcuteTask(Task task)
	{
		this.currentTask = task;
		if(currentTask.TaskProgress == TaskProgress.NoStart)
		{
			PlayerAutoMove.SetDestination(NPCManager._instance.GetNpcById(currentTask.IdNpc).transform.position);
		}
		else if(currentTask.TaskProgress == TaskProgress.Accept)
		{
			PlayerAutoMove.SetDestination(NPCManager._instance.TranscriptGo.transform.position);
		}
	}
	public void OnAcceptTask()
	{
		this.currentTask.TaskProgress = TaskProgress.Accept;
		//TODO寻路到副本
		PlayerAutoMove.SetDestination(NPCManager._instance.TranscriptGo.transform.position);
	}
	public void OnArriveDestination()
	{
		if(this.currentTask.TaskProgress == TaskProgress.NoStart)
		{
			NPCDialogUI._instance.Show(this.currentTask.TalkNpc);
		}

		//达到副本//TODO
	}
}
