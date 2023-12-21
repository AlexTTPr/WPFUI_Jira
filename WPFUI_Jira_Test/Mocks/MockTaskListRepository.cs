using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models;

namespace WPFUI_Jira_Test.Mocks;
internal class MockTaskListRepository
{
	public static Mock<IRepository<TaskList>> GetMock()
	{
		var mock = new Mock<IRepository<TaskList>>();
		var taskBoard = new TaskBoard("TaskBoard")
		{
			Id = 1,
			Owner = new() { Id = 1, Email = "TestUser", Login = "TestLogin", Name = "TestUser" }
		};
		var list = new List<TaskList>()
		{
			new(ListType.Query) {Id = 1, Title = "Очередь", TaskBoard = taskBoard},
			new(ListType.InWork) {Id = 2, Title = "В работе", TaskBoard = taskBoard},
			new(ListType.Done) {Id = 3, Title = "Готово", TaskBoard = taskBoard},
		};

		mock.Setup(m => m.GetCollection(It.IsAny<int>())).Returns(() => list);
		mock.Setup(m => m.GetItem(It.IsAny<int>()))
			.Returns((int id) => list.First());
		mock.Setup(m => m.Create(It.IsAny<TaskList>()))
				.Callback(() => { return; });
		mock.Setup(m => m.Update(It.IsAny<TaskList>()))
			   .Callback(() => { return; });
		mock.Setup(m => m.Delete(It.IsAny<int>()))
		   .Callback(() => { return; });
		return mock;
	}
}
