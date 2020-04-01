using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valuefirst.Models;
using System.Web.Security;


namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            ViewBag.message = TempData["Message"];
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
           
            FormsAuthentication.SetAuthCookie(user.UserName, false);
            Account userClient = new Account();
            User currentuser=userClient.Login(user);
            if (currentuser.UserID != null && currentuser.Email != null)
            {
                Session["UserID"] = currentuser.UserID;
                Session["Email"] = currentuser.Email;
                Session["Roles"] = currentuser.UserRoles;
                return RedirectToAction("index", "UserMaster");
            }
            else
                ModelState.AddModelError("", "Invalid username and password");

            return View();
           
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            Account userClient = new Account();
            string message=userClient.RagisterUser(user);
            if (message == "Success")
            {
                TempData["Message"] = "Ragister Successfully,Please check your mail.";
                //ModelState.AddModelError("", "Ragister Successfully,Please check your mail.");
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Email already exist");
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}