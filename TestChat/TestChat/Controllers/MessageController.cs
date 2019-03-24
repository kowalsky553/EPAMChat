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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SendMessage(User user, String text, Chat chat)
		{
			var message = new Message() { MessageDateTime = DateTime.Now, Text = text };
			db.Messages.Add(message);
			chat.Messages.Add(message);
			db.SaveChanges();
			return RedirectToAction("Chat", "Chat");
		}

		public void Create()
		{
			//return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public void Create([Bind(Include = "MessageId,MessageDateTime,Text")] Message message)
		{
			//if (ModelState.IsValid)
			//{
			//    db.Messages.Add(message);
			//    db.SaveChanges();
			//    return RedirectToAction("Index");
			//}
		}

		// GET: Message/Edit/5
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

		// POST: Message/Edit/5
		// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
		// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
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

		// GET: Message/Delete/5
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

		// POST: Message/Delete/5
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
