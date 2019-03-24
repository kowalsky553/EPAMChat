using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestChat.Models
{
	public class Chat
	{


		public Chat()
		{
			Users = new List<User>();
			Messages = new List<Message>();
		}

		public int ChatId { get; set; }
		public string Name { get; set; }
		public ICollection<User> Users;
		public ICollection<Message> Messages;
	}
}