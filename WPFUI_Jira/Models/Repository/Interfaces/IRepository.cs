using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Repository.Interfaces;

	public interface IRepository<T> where T : class
{
    IEnumerable<T> GetCollection(int ownerId);

    T GetItem(int Id);

    void Create(T item);

    void Update(T item);

    void Delete(int Id);
}
