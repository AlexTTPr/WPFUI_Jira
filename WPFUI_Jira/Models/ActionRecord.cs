﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WPFUI_Jira.Models;

public class ActionRecord
{
    [Key]
    public int Id { get; set; }

    public TimeSpan TimeSpent { get; set; }

    public DateTime CrearionTime { get; set; }

    public int ActorId { get; set; }

    protected ActionRecord()
    {
		CrearionTime = DateTime.Now;
	}

    public ActionRecord(TimeSpan timeSpent, int actorId) : this() 
    {
		TimeSpent = timeSpent;
        ActorId = actorId;
    }
}