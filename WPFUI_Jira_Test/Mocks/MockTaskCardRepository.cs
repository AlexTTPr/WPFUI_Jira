using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models;

namespace WPFUI_Jira_Test.Mocks;
public class MockTaskCardRepository
{
	public static Mock<IRepository<TaskCard>> GetMock()
	{
		var mock = new Mock<IRepository<TaskCard>>();
		var list = new List<TaskCard>()
		{
			new("1", "первая", null, null, 1),
			new("2", "вторая", null, DateTime.Now, 2),
			new("3", "тертья", null, null, 3)
		};

		mock.Setup(m => m.GetCollection(It.IsAny<int>())).Returns(() => list);
		mock.Setup(m => m.GetItem(It.IsAny<int>()))
			.Returns((int id) => list.First());
		mock.Setup(m => m.Create(It.IsAny<TaskCard>()))
				.Callback(() => { return; });
		mock.Setup(m => m.Update(It.IsAny<TaskCard>()))
			   .Callback(() => { return; });
		mock.Setup(m => m.Delete(It.IsAny<int>()))
		   .Callback(() => { return; });
		return mock;
	}
}
