using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Services.Interfaces;
public interface IActionRecordService
{
	IEnumerable<ActionRecord> GetActionRecords(int ownerId);

	ActionRecord GetActionRecord(int id);

	void CreateActionRecord(ActionRecord item);

	void UpdateActionRecord(ActionRecord item);

	void DeleteActionRecord(int id);

	ICollection<ActionRecord> GetExecutorsActionRescordsInDate(int executorId, DateTime date);
}
