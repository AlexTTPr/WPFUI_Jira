using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Services.Interfaces;
public interface ITaskListService
{
    IEnumerable<TaskList> GetTaskLists(int ownerId);

    TaskList GetTaskList(int id);

    void CreateTaskList(TaskList taskCard);

    void UpdateTaskList(TaskList taskCard);

    void DeleteTaskList(int id);
}
