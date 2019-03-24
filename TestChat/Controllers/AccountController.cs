using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestChat.DAL;
using TestChat.Models;

namespace TestChat.Controllers
{
    public class AccountController : Controller
    {
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				// поиск пользователя в бд
				User user = null;
				using (UserContext db = new UserContext())
				{
					user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
				}
				if (user != null)
				{
					FormsAuthentication.SetAuthCookie(model.Name, true);
					return RedirectToAction("Index", "Chat");
				}
				else
				{
					ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
				}
			}

			return View(model);
		}

		public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				User user = null;
				using (UserContext db = new UserContext())
				{
					user = db.Users.FirstOrDefault(u => u.Name == model.Name);
				}
				if (user == null)
				{
					
					using (UserContext db = new UserContext())
					{
						var maxId = db.Users.Any() ? db.Users.Select(u => u.UserId).Max() : 1;
						db.Users.Add(new User { Email = model.Email, Password = model.Password, Name = model.Name, UserId = maxId + 1});
						db.SaveChanges();

						user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
					}
					
					if (user != null)
					{
						FormsAuthentication.SetAuthCookie(model.Name, true);
						return RedirectToAction("Login", "Account");
					}
				}
				else
				{
					ModelState.AddModelError("", "Пользователь с таким логином уже существует");
				}
			}

			return View(model);
		}
		public ActionResult Logoff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}
	}
}
