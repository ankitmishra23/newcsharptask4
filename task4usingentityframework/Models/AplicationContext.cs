using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace task4usingentityframework.Models
{
	class AplicationContext:DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer("Data Source=APINP-ELPT64426\\SQLEXPRESS;user id=sa;password=Ankitm%$23;database=mynewdb");
		}
		public DbSet<Person> persons { get; set; }
	}
}
