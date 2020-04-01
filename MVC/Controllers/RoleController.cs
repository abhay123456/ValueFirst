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
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
       
        public ActionResult Index()
        {
            RoleMasterClient roleMasterClient = new RoleMasterClient();
            IEnumerable<RoleMaster> roleMasters= roleMasterClient.GetRoles();
            return View(roleMasters.ToList());
        }

        // GET: Role/Details/5
        

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(RoleMaster collection)
        {
            try
            {
                RoleMasterClient roleMasterClient = new RoleMasterClient();
                roleMasterClient.Create(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            RoleMasterClient roleMasterClient = new RoleMasterClient();
            RoleMaster roleMaster= roleMasterClient.find(id);
            return View(roleMaster);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RoleMaster collection)
        {
            try
            {
                 RoleMasterClient roleMasterClient = new RoleMasterClient();
                 roleMasterClient.Edit(id, collection);
                 return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            RoleMasterClient roleMasterClient = new RoleMasterClient();
            RoleMaster roleMaster = roleMasterClient.find(id);
            return View(roleMaster);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                RoleMasterClient roleMasterClient = new RoleMasterClient();
                roleMasterClient.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
