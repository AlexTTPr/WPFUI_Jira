using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Services.Interfaces;
internal interface IProjectService
{
    IEnumerable<Project> GetProjects(int ownerId);

    Project GetProject(int id);

    void CreateProject(Project project);

    void UpdateProject(Project project);

    void DeleteProject(int id);
}
