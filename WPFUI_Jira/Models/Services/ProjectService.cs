using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.Models.Services;

internal class ProjectService : IProjectService
{
    private IDbRepos _db;

    public ProjectService(IDbRepos dbRepos)
    {
        _db = dbRepos;
    }

    public void CreateProject(Project project)
    {
        _db.Projects.Create(project);
        Save();
    }

    public void DeleteProject(int id)
    {
        _db.Projects.Delete(id);
        Save();
    }

    public Project GetProject(int id)
    {
        return _db.Projects.GetItem(id);
    }

    public IEnumerable<Project> GetProjects(int ownerId)
    {
        return _db.Projects.GetCollection(ownerId);
    }

    public void UpdateProject(Project project)
    {
        _db.Projects.Update(project);
        Save();
    }

    public void Save()
    {
        _db.Save();
    }
}
