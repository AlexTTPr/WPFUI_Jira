using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.Repository;
internal class ActionRecordRepository : IRepository<ActionRecord>
{
	private ApplicationContext _context;

	public ActionRecordRepository(ApplicationContext context)
	{
		_context = context;
	}

	public void Create(ActionRecord item)
	{
		throw new NotImplementedException();
	}

	public void Delete(int Id)
	{
		throw new NotImplementedException();
	}

	public ICollection<ActionRecord> GetCollection(int ownerId)
	{
		return _context.ActionRecord.Where(i=>i.ActorId==ownerId).ToList();
	}

	public ActionRecord GetItem(int Id)
	{
		throw new NotImplementedException();
	}

	public void Update(ActionRecord item)
	{
		throw new NotImplementedException();
	}
}
