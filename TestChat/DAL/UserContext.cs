using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TestChat.Models;

namespace TestChat.DAL
{
	public class UserContext : DbContext
	{
		public UserContext() : base("UserContext")
		{
		}

		public DbSet<Message> Messages { get; set; }
		public DbSet<Chat> Chats { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}