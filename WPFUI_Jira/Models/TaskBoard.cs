using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models;

public  class TaskBoard
{
    public int Id { get; set; }

    public User Owner { get; set; }

    public ObservableCollection<TaskList> TaskLists { get; set; }

    public string? Title { get; set; }

    public TaskBoard(string title)
    {
        TaskLists = new ObservableCollection<TaskList>();
        Title = title;
    }
}