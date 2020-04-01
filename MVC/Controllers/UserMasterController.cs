using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valuefirst.Filters;
using Valuefirst.Models;

namespace Valuefirst.Controllers
{
    [AuthFilter]
    public class UserMasterController : Controller
    {
        // GET: UserMaster

        public ActionResult Index()
        {

            UserMasterClient userMasterClient = new UserMasterClient();
            IEnumerable<User> users = userMasterClient.GetAllUsers();
            if (Session != null && Session["Roles"] != null && Session["Roles"].ToString().ToLower() == "admin")
                return View(users);
            else if (Session != null && Session["Roles"] != null && Session["Roles"].ToString().ToLower() == "supervisor")
                return View(users.Where(a => a.UserRoles.ToLower() != "admin"));
            else
                return View(users.Where(a => a.UserRoles.ToLower() == "agent"));
        }

        // GET: UserMaster/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: UserMaster/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: UserMaster/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserMaster/Edit/5
        [Authorize(Roles = "Supervisor,Admin")]
        public ActionResult Edit(int id)
        {
            UserMasterClient userMasterClient = new UserMasterClient();
            User user = userMasterClient.find(id);
            if (user.UserID != null)
            {
                RoleMasterClient roleMasterClient = new RoleMasterClient();
                List<RoleMaster> rolemasters = roleMasterClient.GetRoles().ToList();
                ViewBag.Roles = new SelectList(rolemasters, "Role", "Role", user.UserRoles);
                List<UserStatus> userStatuses = new List<UserStatus>();
                userStatuses.Add(new UserStatus() { Status = "Active" });
                userStatuses.Add(new UserStatus() { Status = "Deactive" });
                ViewBag.Status = new SelectList(userStatuses, "Status", "Status", user.Status);

            }

            return View(user);
        }

        // POST: UserMaster/Edit/5
        [Authorize(Roles = "Supervisor,Admin")]
        [HttpPost]
        public ActionResult Edit(int id, User collection)
        {
            try
            {
                // TODO: Add update logic here
                UserMasterClient userMasterClient = new UserMasterClient();
                userMasterClient.Edit(id, collection);
                return RedirectToAction("Index");


            }
            catch
            {
                return View();
            }
        }

        // GET: UserMaster/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            UserMasterClient userMasterClient = new UserMasterClient();
            User user = userMasterClient.find(id);
            return View(user);
        }

        // POST: UserMaster/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                UserMasterClient userMasterClient = new UserMasterClient();
                userMasterClient.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

    public class UserStatus
    {
        public string Status { get; set; }
    }
}
