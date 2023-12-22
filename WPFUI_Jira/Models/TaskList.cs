using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models;

public class TaskList
{
    public int Id { get; set; }

    public int TaskBoardId { get; set; }

    public ObservableCollection<TaskCard> TaskCards { get; set; }

    public string Title { get; set; }

    public ListType Type { get; set; }

    public TaskList(ListType type)
    {
        Type = type;
    }
}
