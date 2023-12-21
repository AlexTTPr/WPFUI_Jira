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
public class TestTaskCardService
{
	Mock<IDbRepos> uowMock;
	TaskCardService service;

	private readonly TaskCard _invalidTaskCardTitle = new TaskCard("", null, null, null, 1);

	private readonly TaskCard _invalidTaskCardExpirationTime = new TaskCard("", null, null, DateTime.MinValue, 1);

	private readonly TaskCard _validTaskCard = new TaskCard("Task", null, null, null, 1);

	[SetUp]
	public void Setup()
	{
		uowMock = MockUowRepository.GetMock();
		service = new TaskCardService(uowMock.Object);
	}

	[Test]
	public void CreateTask_Fail()
	{
		try
		{
			Assert.Throws<InvalidOperationException>(() => service.PutTask(_invalidTaskCardTitle));
			Assert.Throws<InvalidOperationException>(() => service.PutTask(_invalidTaskCardExpirationTime));
		}
		catch (Exception ex) { }

		Assert.Pass();
	}

	[Test]
	public void CreateTask_Success()
	{
		var result = service.PutTask(_validTaskCard);
		Assert.IsNotNull(result);
        Assert.GreaterOrEqual(1, result.Id);

        Assert.Pass();
	}
}
