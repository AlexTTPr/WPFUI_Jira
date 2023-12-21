using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira_Test.Mocks;
internal class MockUowRepository
{
	public static Mock<IDbRepos> GetMock()
	{
		var mock = new Mock<IDbRepos>();
		var mockProjectRepository = MockProjectRepository.GetMock();
		var mockTaskBoardRepository = MockTaskBoardRepository.GetMock();
		var mockTaskCardRepository = MockTaskCardRepository.GetMock();
		var mockTaskListRepository = MockTaskListRepository.GetMock();
		var mockUserRepository = MockUserRepository.GetMock();

		mock.Setup(m => m.Projects).Returns(() => mockProjectRepository.Object);
		mock.Setup(m => m.TaskBoards).Returns(() => mockTaskBoardRepository.Object);
		mock.Setup(m => m.TaskCards).Returns(() => mockTaskCardRepository.Object);
		mock.Setup(m => m.TaskLists).Returns(() => mockTaskListRepository.Object);
		mock.Setup(m => m.Users).Returns(() => mockUserRepository.Object);

		return mock;
	}
}
