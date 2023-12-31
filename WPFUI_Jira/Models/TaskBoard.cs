﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models;

public  class TaskBoard
{
	[Key]
	public int Id { get; set; }

    public int OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public User Owner { get; set; }

    public ObservableCollection<TaskList> TaskLists { get; set; }

    public string? Title { get; set; }

    public TaskBoard(string title)
    {
        TaskLists = new ObservableCollection<TaskList>();
        Title = title;
    }
}