using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Valuefirst.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class RagistrationController : ApiController
    {
        private UserManagementEntities db = new UserManagementEntities();
        [HttpPost]
        public IHttpActionResult RagisterUser(User user)
        {

            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, user);
            }
            if (db.Users.Any(a => a.Email == user.Email))
            {
                return Content(HttpStatusCode.BadRequest, "Duplicate");
            }
            if(!db.Rolemasters.Any(a=>a.Role=="Agent"))
            {
                db.Rolemasters.Add(new Rolemaster() {Role= "Agent" });
                db.SaveChanges();
            }
            if (!db.Rolemasters.Any(a => a.Role == "Admin"))
            {
                db.Rolemasters.Add(new Rolemaster() { Role = "Admin" });
                db.SaveChanges();
            }
            if (!db.Rolemasters.Any(a => a.Role == "Supervisor"))
            {
                db.Rolemasters.Add(new Rolemaster() { Role = "Supervisor" });
                db.SaveChanges();
            }
            if(db.Users.Count()==0)
                user.UserRoles = "Admin";
            else
                user.UserRoles = "Agent";
            user.Status = "Active";
            
            db.Users.Add(user);
            db.SaveChanges();
            MailSetup objmailsetup = new MailSetup();
            objmailsetup.Sendmail(db.Users.Where(a => a.Email == user.Email).Select(m => m.UserID).FirstOrDefault(), db.Users.Where(a => a.Email == user.Email).Select(m => m.Name).FirstOrDefault(), db.Users.Where(a => a.Email == user.Email).Select(m => m.Email).FirstOrDefault());
            return Content(HttpStatusCode.OK, "Success");
        }
        [HttpPost]
        public IHttpActionResult Login(UserLogin user)
        {
            if (db.Users.Any(a => a.UserID == user.UserName && a.Status=="Active"))
            {
                return Content(HttpStatusCode.OK, db.Users.Where(a=>a.UserID == user.UserName && a.Password == user.Password));
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "User Not Exist");
            }
        }

        [HttpPost]
        public IHttpActionResult GetCurrentUserRole(string UserID)
        {
            string userrole = db.Users.Where(m => m.UserID == UserID).Select(a => a.UserRoles).FirstOrDefault();
            if (string.IsNullOrEmpty(userrole))
                return Content(HttpStatusCode.OK, "User not Exist");
            return Content(HttpStatusCode.OK, userrole);

        }


    }
}
