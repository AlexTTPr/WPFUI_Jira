using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFUI_Jira.Models.MongoDB;

class MongoContext
{
	string connectionString = ConfigurationManager.ConnectionStrings["TestMongoDBConnection"].ConnectionString;
	MongoClient client;
	IMongoDatabase database;

	public IMongoCollection<User> UserCollection
	{
		get { return database.GetCollection<User>("Users"); }
	}

	public IMongoCollection<Project> ProjectCollection
	{
		get { return database.GetCollection<Project>("Projects"); }
	}

	public IMongoCollection<TaskBoard> TaskBoardCollection
	{
		get { return database.GetCollection<TaskBoard>("TaskBoards"); }
	}

	public IMongoCollection<TaskList> TaskListCollection
	{
		get { return database.GetCollection<TaskList>("TaskLists"); }
	}

	public IMongoCollection<TaskCard> TaskCardCollection
	{
		get { return database.GetCollection<TaskCard>("TaskCards"); }
	}

	public MongoContext()
	{
		var connection = new MongoUrlBuilder(connectionString);
		MongoClient client = new MongoClient(connectionString);
		database = client.GetDatabase(connection.DatabaseName);

		//var ts = new TaskBoard("KPO") { Id = 1, Owner = new User() { Id = 1, Email = "asdf", Login = "asdf", Name = "ASDF" } };
		//var v = new List<TaskList>()
		//{
		//	new(ListType.InWork){ Id = 1, TaskBoard = ts, Title = "В работе"},
		//	new(ListType.Query){Id = 2, TaskBoard = ts, Title = "Очередь"},
		//	new(ListType.Done){Id = 3, TaskBoard = ts, Title = "Готово"}
		//};

		//TaskListCollection.InsertMany(v);
		//TaskBoardCollection.InsertOne(new("KPO") { Id = 1, Owner = new User() { Id = 1, Email = "asdf", Login = "asdf", Name = "ASDF" } });


		//var v = new List<TaskCard>()
		//{
		//	new TaskCard("йцукенгшщ54", "", new User() { Id = 1, Email = "asdf", Login = "asdf", Name = "ASDF"}, null, 1) {Id = 1},
		//	new TaskCard("DDDDDDD", "sdvsdvc", null, null, 2) {Id = 2},
		//	new TaskCard("dscvfdcvfd", "", new User() { Id = 1, Email = "asdf", Login = "asdf", Name = "ASDF"}, null, 3) {Id = 3},
		//	new TaskCard("1325епавсма", "12345", null, null, 3) {Id = 4}
		//};

		//TaskCardCollection.InsertMany(v);
	}
}
