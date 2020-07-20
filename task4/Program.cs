using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace task4
{
	class Program
	{
		static void Main(string[] args)
		{
			string connectionString = "Data Source=APINP-ELPT64426\\SQLEXPRESS;user id=sa;password=Ankitm%$23;database=C#tasks";
			SqlConnection con = new SqlConnection();
			con.ConnectionString = connectionString;


			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select * from persons";
			cmd.Connection = con;

			
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();

			UserRepository userRepository = new UserRepository();

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					//Adding data to users list by extracting it from table by using the method AddUser().
					User user = new User();
					user.id = reader.GetInt32(0);
					user.name = reader.GetString(1);
					user.email = reader.GetString(2);
					user.location = reader.GetString(3);
					user.address = reader.GetString(4);
					user.isActive = reader.GetInt32(5);
					userRepository.AddUser(user);
				}
			}
			else 
			{
				Console.WriteLine("No data found in the table.");
			}

		
			con.Close();


			//displaying the list users by using method Users().
			foreach (User item in userRepository.Users())
			{
				Console.WriteLine(item.id + "\t" + item.name + "\t" + item.email + "\t" + item.location + "\t" + item.address + "\t" + item.isActive);
			}


			//Since all the data is added to the list , now all other operations are same as task3.
			Console.WriteLine("Enter any key");
			Console.ReadLine();


			//deleting the user based on id using DeleteUser() method.
			Console.WriteLine("Enter te id of user that needs to be deleted");
			int userId = Convert.ToInt32(Console.ReadLine());
			//List<User> resultList = userRepository.DeleteUser(userId);
			//if (resultList != null)
			//{ 

			//}

			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds,"persons");
			int count = ds.Tables[0].Rows.Count;
			if (count > 0)
			{
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					if (Convert.ToInt32(row["id"]) == userId)
					{
						row.Delete();
					}
				}
				ds.Tables[0].AcceptChanges();
				SqlCommandBuilder obj = new SqlCommandBuilder(da);



				//making changes to the db
				con.Open();
				cmd.CommandText = "delete from persons where id=" + userId;
				cmd.ExecuteNonQuery();
				con.Close();




				da.Update(ds, "persons");

				foreach (DataRow row in ds.Tables[0].Rows)
				{
					Console.WriteLine(row["id"] + "\t" + row["name"]);
				}
			}
			else {
				Console.WriteLine("table is empty.");
			}


			//retriving data about active users.
			Console.WriteLine("Enter any key");
			Console.ReadLine();

			if (count > 0)
			{

				Console.WriteLine("Active Users are");
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					if (Convert.ToInt32(row["isActive"]) == 1)
					{ 
						Console.WriteLine(row["id"] + "\t" + row["name"]+"\t"+row["isActive"]);
					}
				}
			}

			Console.WriteLine("Enter any key");
			Console.ReadLine();

			//geting user by the id.
			Console.WriteLine("Enter the id to retrive the details");
			int getUserId = Convert.ToInt32(Console.ReadLine());

			if (count>0)
			{
				foreach(DataRow row in ds.Tables[0].Rows)
				{
					if ((Convert.ToInt32(row["id"])) == getUserId)
					{
						Console.WriteLine(row["id"] + "\t" + row["name"] + "\t" + row["email"] + "\t" + row["isActive"]);
					}
				}
			}
			else
			{
				Console.WriteLine("No data found in the table.");
			}





		}
	}
}
