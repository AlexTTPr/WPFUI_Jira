using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.Models.Ninject;

internal class NinjectRegistration : NinjectModule
{
    public override void Load()
    {
        Bind<IDbRepos>().To<DbRepos>().InSingletonScope();
		Bind<IUserService>().To<UserService>();
		Bind<IProjectService>().To<ProjectService>();
		Bind<ITaskBoardService>().To<TaskBoardService>();
        Bind<ITaskListService>().To<TaskListService>();
        Bind<ITaskCardService>().To<TaskCardService>();
        Bind<INavigationStore>().To<NavigationStore>().InSingletonScope();
        Bind<INavigationService>().To<NavigationService>();
        Bind<IAccountStore>().To<AccountStore>().InSingletonScope();
        Bind<IAuthenticationService>().To<AuthenticationService>().InSingletonScope();
	}
}
