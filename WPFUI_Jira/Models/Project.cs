using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFUI_Jira.Models;

public class Project
{
	[Key]
	public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public int OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	public User Owner { get; set; }
    
    public ICollection<User>? Workers { get; set; }

    public int TaskBoardId { get; set; }

    [ForeignKey(nameof(TaskBoardId))]
    public TaskBoard TaskBoard { get; set; }
}