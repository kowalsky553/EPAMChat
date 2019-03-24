using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestChat.DAL;
using TestChat.Models;

namespace TestChat.Controllers
{
	public class MessageController : Controller
	{
		private UserContext db = new UserContext();

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "MessageId,Text,ChatId")] Message message, int chatId, string userGuid)
		{
			if (ModelState.IsValid)
			{
				var chat = db.Chats.First(c => c.ChatId == chatId);
				var author = db.Users.First(u => u.Email == userGuid);
				message.MessageDateTime = DateTime.Now;
				message.ChatId = chatId;
				message.Author = author;
				message.AuthorId = author.UserId;
				chat.Messages.Add(message);
				db.Messages.Add(message);
				db.SaveChanges();
				return RedirectToAction(string.Format("Chat/{0}", chatId),"Chat");
			}
			return View();
		}
		
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Message message = db.Messages.Find(id);
			if (message == null)
			{
				return HttpNotFound();
			}
			return View(message);
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "MessageId,MessageDateTime,Text")] Message message)
		{
			if (ModelState.IsValid)
			{
				db.Entry(message).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(message);
		}
		
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Message message = db.Messages.Find(id);
			if (message == null)
			{
				return HttpNotFound();
			}
			return View(message);
		}
		
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Message message = db.Messages.Find(id);
			db.Messages.Remove(message);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
