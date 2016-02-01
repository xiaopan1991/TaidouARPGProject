
using System.Collections.Generic;
using TaidouCommon.Model;

public delegate void OnSyncTaskCompleteEvent();

public delegate void OnGetRoleEvent(List<Role> roleList);

public delegate void OnAddRoleEvent(Role role);

public delegate void OnSelectRoleEvent();

public delegate void OnGetTaskDBListEvent(List<TaskDB> list);

public delegate void OnAddTaskDBEvent(TaskDB taskDB);

public delegate void OnUpdateTaskDBEvent();