using UnityEngine;
using System.Collections;

public class TaskUI : MonoBehaviour {

	private UIGrid taskListGrid;
	private TweenPosition tween;
	private UIButton closeButton;

	public GameObject taskItemPrefab;
	public static TaskUI _instance;

	void Awake()
	{
		_instance = this;
		taskListGrid = transform.Find("Scroll View/Grid").GetComponent<UIGrid>();
		tween = this.GetComponent<TweenPosition>();
		closeButton = transform.Find("CloseButton").GetComponent<UIButton>();

		EventDelegate ed = new EventDelegate(this, "OnClose");
		closeButton.onClick.Add(ed);
	}

	void Start()
	{
		InitTaskList();
	}

	/// <summary>
	/// 初始化任务列表信息
	/// </summary>
	void InitTaskList () {
		ArrayList taskList = TaskManager._instance.GetTaskList();

		foreach(Task task in taskList)
		{
			GameObject go = NGUITools.AddChild(taskListGrid.gameObject, taskItemPrefab);
			TaskItemUI ti = go.GetComponent<TaskItemUI>();
			ti.SetTask(task);
			taskListGrid.AddChild(go.transform);
		}
	}
	
	public void Show()
	{
		tween.PlayForward();
	}
	public void Hide()
	{
		tween.PlayReverse();
	}

	void OnClose()
	{
		Hide();
	}
}
