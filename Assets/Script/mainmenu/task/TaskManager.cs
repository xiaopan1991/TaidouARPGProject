﻿using UnityEngine;
using System.Collections;

public class TaskManager : MonoBehaviour {

	public TextAsset taskinfoText;

	private ArrayList taskList = new ArrayList();

	/// <summary>
	/// 初始化任务信息
	/// </summary>
	public void InitTask()
	{
		string[] taskinfoArray = taskinfoText.ToString().Split("\n");
		foreach(string str in taskinfoArray)
		{
			string[] proArray = str.Split("|");
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
			task.Coin = proArray[5];
			task.Diamond = proArray[6];
			task.TalkNpc = proArray[7];
			task.IdNpc = proArray[8];
			task.IdTranscript = proArray[9];
			taskList.Add(task);
		}
	}
}
