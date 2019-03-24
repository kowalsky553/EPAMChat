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
	public class ChatController : Controller
	{
		private UserContext db = new UserContext();

		[Authorize]
		public ActionResult Index()
		{
			return View(db.Chats.ToList());
		}

		[Authorize]
		public ActionResult Chat(int? id)
		{
			if (id == null)			
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			
			Chat chat = db.Chats.Find(id);
			if (chat == null)			
				return HttpNotFound();
			
			chat.Messages = db.Messages.Where(m => m.ChatId == chat.ChatId).ToList();
			foreach (var message in chat.Messages)			
				message.Author = db.Users.First(u => u.UserId == message.AuthorId);
			
			return View(chat);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ChatId,Name")] Chat chat)
		{
			if (ModelState.IsValid)
			{
				db.Chats.Add(chat);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(chat);
		}


		[Authorize]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Chat chat = db.Chats.Find(id);
			if (chat == null)
			{
				return HttpNotFound();
			}
			return View(chat);
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ChatId,Name")] Chat chat)
		{
			if (ModelState.IsValid)
			{
				db.Entry(chat).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(chat);
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Chat chat = db.Chats.Find(id);
			if (chat == null)
			{
				return HttpNotFound();
			}
			return View(chat);
		}

		[HttpPost, ActionName("Delete")]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Chat chat = db.Chats.Find(id);
			db.Chats.Remove(chat);
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
