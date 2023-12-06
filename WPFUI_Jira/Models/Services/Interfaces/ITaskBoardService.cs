using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Services.Interfaces;

internal interface ITaskBoardService
{
    IEnumerable<TaskBoard> GetTaskBoards(int onwerId);

    TaskBoard GetTaskBoard(int id);

    void CreateTaskBoard(TaskBoard taskBoard);

    void UpdateTaskBoard(TaskBoard taskBoard);

    void DeleteTaskBoard(int id);
}
