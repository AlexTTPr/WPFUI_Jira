using System;

namespace WPFUI_Jira.Models;

public class ActionRecord
{
    public string Id { get; set; }

    public TimeSpan SpendTime { get; set; }

    public DateTime CrearionTime { get; set; }

    public User Actor { get; set; }
}