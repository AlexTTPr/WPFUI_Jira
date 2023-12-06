using System.ComponentModel.DataAnnotations.Schema;

namespace WPFUI_Jira.Models;

public class Project
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public User Owner { get; set; }
    
    public ICollection<User>? Workers { get; set; }

    public TaskBoard? TaskBoard { get; set; }
}