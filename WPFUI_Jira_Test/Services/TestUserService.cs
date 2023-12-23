using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services;
using WPFUI_Jira_Test.Mocks;

namespace WPFUI_Jira_Test.Services;
internal class TestUserService
{
	//Mock<IDbRepos> uowMock;
	//UserService service;

	//private readonly User _invalidUserEmail = new User() { Id = 3, Email = "TestUser1", Login = "TestLogin1", Name = "TestUser1"};

	//private readonly User _invalidUserLogin = new User() { Id = 2, Email = "TestUser1@gmail.com", Login = "", Name = "TestUser2" };

	//private readonly User _validUser = new User() { Id = 1, Email = "TestUser1@gmail.com", Login = "TestLogin3", Name = "TestUser3" };

	//[SetUp]
	//public void Setup()
	//{
	//	uowMock = MockUowRepository.GetMock();
	//	service = new UserService(uowMock.Object);
	//}

	//[Test]
	//public void CreateUser_FailEmail()
	//{
	//	Assert.Throws<InvalidOperationException>(() => service.PutUser(_invalidUserEmail));
	//}

	//[Test]
	//public void CreateUser_FailLogin()
	//{
	//	Assert.Throws<InvalidOperationException>(() => service.PutUser(_invalidUserLogin));
	//}

	//[Test]
	//public void CreateUser_Success()
	//{
	//	var result = service.PutUser(_validUser);
	//	Assert.IsNotNull(result);
	//	Assert.GreaterOrEqual(1, result.Id);
	//}
}
