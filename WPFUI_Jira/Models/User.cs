using System.ComponentModel.DataAnnotations;

namespace WPFUI_Jira.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Email { get; set; }

    public string Login { get; set; }
    public string _name;
    public string Name
    {
        get => _name;
        set => _name = value;
    }

	public ICollection<Project> OwnedProjects { get; set; }

    public ICollection<Project> WorkProjects { get; set; }

	public override string ToString()
	{
        return Name;
	}
}