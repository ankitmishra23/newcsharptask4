using System;
using task4usingentityframework.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace task4usingentityframework
{
	class Program
	{
		static void Main(string[] args)
		{
			
			AplicationContext app = new AplicationContext();

			//inserting data to db.
			for (int i = 0; i < 10; i++)
			{

				Person person = new Person();
				person.name = "Ankit" + i;
				person.email = "Ankit" + i + "@gmail.com";
				person.isActive = (i % 5 == 0 || i % 3 == 0) ? 1 : 0;
				app.persons.Add(person);
				app.SaveChanges();
			}

			//extracting persion detail by id

			Console.WriteLine("Enter the id to search");
			int userId = Convert.ToInt32(Console.ReadLine());
			var getPerson = app.persons.Where(a => a.id == userId).FirstOrDefault();

			if (getPerson != null)
			{
				Console.WriteLine(getPerson.id + "\t" + getPerson.name + "\t" + getPerson.email + "\t" + getPerson.isActive);

			}
			else {
				Console.WriteLine("Not found");
			}


			//get active users
			Console.WriteLine("Enter any key to see active users");
			Console.ReadLine();
			var activeUsers = app.persons.Where(a => a.isActive == 1).ToList();

			if (activeUsers.Any())
			{
				foreach (Person item in activeUsers)
				{
					Console.WriteLine(item.id + "\t" + item.name + "\t" + item.isActive);
				}
			}
			else {
				Console.WriteLine("No active users.");
			}


			//delete based on id
			Console.WriteLine("Enter the id to delete");
			int newId = Convert.ToInt32(Console.ReadLine());
			var resultantList = app.persons.Where(a => a.id == newId).FirstOrDefault();
			if (resultantList != null)
			{

				app.persons.Remove(resultantList);
				app.SaveChanges();

			}
			else {
				Console.WriteLine("no such id is found");
			}
		}
	}
}
