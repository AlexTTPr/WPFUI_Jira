using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models;

public class TaskCard
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get;  set; }

	public int TaskListId { get; set; }

	public User? Executor { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? ExpirationTime { get; set; }

    public List<ActionRecord> Actions { get; set; }

    private TaskCard()
    {
        CreationTime = DateTime.Now;
		Actions = new();
	}

    public TaskCard(string title, string? taskDescription, User? executer, DateTime? expirationDate, int taskListId) : this()
    {
        Title = title;
        Description = taskDescription;
        Executor = executer;
        ExpirationTime = expirationDate;
        TaskListId = taskListId;
	}
}