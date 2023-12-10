using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Services.Interfaces;
public interface ITaskCardService
{
    IEnumerable<TaskCard> GetTaskCards(int onwerId);

    TaskCard GetTaskCard(int id);

    void CreateTaskCard(TaskCard item);

    void UpdateTaskCard(TaskCard item);

    void DeleteTaskCard(int id);
}
