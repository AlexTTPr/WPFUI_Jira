using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Repository;
using WPFUI_Jira.Models.Repository.Interfaces;
using Moq;

namespace WPFUI_Jira_Test.Mocks;

internal class MockProjectRepository
{
	public static Mock<IRepository<Project>> GetMock()
	{
		var mock = new Mock<IRepository<Project>>();
		var list = new List<Project>()
		{
			new()
			{
				Id = 1,
				Title = "Test",
				Owner = new(){ Id = 1, Email = "TestUser", Login = "TestLogin", Name = "TestUser"}
			}
		};

		mock.Setup(m => m.GetCollection(It.IsAny<int>())).Returns(() => list);
		mock.Setup(m => m.GetItem(It.IsAny<int>()))
			.Returns((int id) => list.First());
		mock.Setup(m => m.Create(It.IsAny<Project>()))
				.Callback(() => { return; });
		mock.Setup(m => m.Update(It.IsAny<Project>()))
			   .Callback(() => { return; });
		mock.Setup(m => m.Delete(It.IsAny<int>()))
		   .Callback(() => { return; });
		return mock;
	}
}
