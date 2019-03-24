using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestChat.Models
{
	public class Message
	{
		public int MessageId { get; set; }
		public User Author { get; set; }
		public DateTime MessageDateTime { get; set; }
		[AllowHtml]
		public string Text { get; set; }
	}
}