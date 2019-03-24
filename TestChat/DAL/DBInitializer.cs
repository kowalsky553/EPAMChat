using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestChat.Models;

namespace TestChat.DAL
{
	public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseAlways<UserContext>
	{
		protected override void Seed(UserContext context)
		{
			//var users = new List<User>
			//{
			//	new User(){Name = "Admin", Password = "123", AuthorId = 1, Email = "qwe@mail.ru"}
			//};
			//users.First().Chats.Add(new Chat(){ChatId = 1, Name = "First Chat", Messages = new List<Message>(), Users = new List<User>()});
			//users.First().Chats.First().Users.Add(users.First());

			//users.ForEach(s => context.Users.Add(s));
			//context.SaveChanges();
			//var chats = new List<Chat>
			//{
			//	users.First().Chats.First()
			//};
			//chats.ForEach(s => context.Chats.Add(s));
			//context.SaveChanges();
			//var messages = new List<Message>
			//{
			//	new Message(){Author = users.First(), MessageDateTime = DateTime.Now, MessageId = 1}
			//};
			//chats.First().Messages.Add(messages.First());
			//messages.ForEach(s => context.Messages.Add(s));
			//context.SaveChanges();
		}
	}
}