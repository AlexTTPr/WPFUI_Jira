using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models;

namespace WPFUI_Jira_Test.Mocks;
internal class MockTaskBoardRepository
{
	public static Mock<IRepository<TaskBoard>> GetMock()
	{
		var mock = new Mock<IRepository<TaskBoard>>();
		var list = new List<TaskBoard>()
		{
			new("TaskBoard")
			{
				Id = 1,
				Owner = new(){ Id = 1, Email = "TestUser", Login = "TestLogin", Name = "TestUser"}
			}
		};

		mock.Setup(m => m.GetCollection(It.IsAny<int>())).Returns(() => list);
		mock.Setup(m => m.GetItem(It.IsAny<int>()))
			.Returns((int id) => list.First());
		mock.Setup(m => m.Create(It.IsAny<TaskBoard>()))
				.Callback(() => { return; });
		mock.Setup(m => m.Update(It.IsAny<TaskBoard>()))
			   .Callback(() => { return; });
		mock.Setup(m => m.Delete(It.IsAny<int>()))
		   .Callback(() => { return; });
		return mock;
	}
}
