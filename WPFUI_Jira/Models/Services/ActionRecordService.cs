using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.Models.Services;
public class ActionRecordService : IActionRecordService
{
	private IDbRepos _db;

	public ActionRecordService(IDbRepos dbRepos)
	{
		_db = dbRepos;
	}
	public void CreateActionRecord(ActionRecord item)
	{
		throw new NotImplementedException();
	}

	public void DeleteActionRecord(int id)
	{
		throw new NotImplementedException();
	}

	public ActionRecord GetActionRecord(int id)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<ActionRecord> GetActionRecords(int ownerId)
	{
		throw new NotImplementedException();
	}

	//I'm sorry
	public ICollection<ActionRecord> GetExecutorsActionRescordsInDate(int executorId, DateTime date)
	{
		DateTime leftEdge = date.AddSeconds(date.TimeOfDay.Seconds);
		DateTime rightEdge = leftEdge.AddDays(1).AddSeconds(-1);

		return _db.ActionRecords.GetCollection(executorId).Where(i => i.CrearionTime >= leftEdge && i.CrearionTime <= rightEdge).ToList();
	}

	public void UpdateActionRecord(ActionRecord item)
	{
		throw new NotImplementedException();
	}
}
