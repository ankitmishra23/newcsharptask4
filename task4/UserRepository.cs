using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace task4
{
	public class UserRepository : IUserRepository
	{
		private List<User> users = new List<User>();
		public List<User> ActiveUsers()
		{
			List<User> actieList = users.Where(a => a.isActive == 1).ToList();
			if (actieList.Any())
			{
				return actieList;
			}
			return null;
		}

		public List<User> AddUser(User user)
		{
			users.Add(user);
			return users;
		}

		public List<User> DeleteUser(int id)
		{
			List<User> temp = users.Where(a => a.id == id).ToList();
			if (temp.Any())
			{
				foreach (User item in users)
				{
					users.Remove(item);
				}
			}
			else {
				return null;
			}
			return users;
		}

		public User GetUser(int id)
		{
			var result = users.Where(a => a.id == id).FirstOrDefault();
			return result;
		}

		public List<User> Users()
		{
			return users;
		}
	}
}
