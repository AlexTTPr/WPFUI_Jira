using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models;

namespace WPFUI_Jira_Test.Mocks;
public class MockUserRepository
{
	public static Mock<IRepository<User>> GetMock()
	{
		var mock = new Mock<IRepository<User>>();
		var list = new List<User>()
		{
			new(){ Id = 1, Email = "TestUser1@gmail.com", Login = "TestLogin1", Name = "TestUser1"},
			new(){ Id = 2, Email = "TestUser2@gmail.com", Login = "TestLogin2", Name = "TestUser2"},
			new(){ Id = 3, Email = "TestUser3@gmail.com", Login = "TestLogin3", Name = "TestUser3"}
		};

		mock.Setup(m => m.GetCollection(It.IsAny<int>())).Returns(() => list);
		mock.Setup(m => m.GetItem(It.IsAny<int>()))
			.Returns((int id) => list.First());
		mock.Setup(m => m.Create(It.IsAny<User>()))
				.Callback(() => { return; });
		mock.Setup(m => m.Update(It.IsAny<User>()))
			   .Callback(() => { return; });
		mock.Setup(m => m.Delete(It.IsAny<int>()))
		   .Callback(() => { return; });
		return mock;
	}
}
