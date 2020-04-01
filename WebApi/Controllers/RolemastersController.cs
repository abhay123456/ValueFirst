using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class RolemastersController : ApiController
    {
        private UserManagementEntities db = new UserManagementEntities();

        // GET: api/Rolemasters
        public IQueryable<Rolemaster> GetRolemasters()
        {
            return db.Rolemasters;
        }

        // GET: api/Rolemasters/5
        [ResponseType(typeof(Rolemaster))]
        public IHttpActionResult GetRolemaster(int id)
        {
            Rolemaster rolemaster = db.Rolemasters.Find(id);
            if (rolemaster == null)
            {
                return NotFound();
            }

            return Ok(rolemaster);
        }

        // PUT: api/Rolemasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRolemaster(int id, Rolemaster rolemaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rolemaster.Id)
            {
                return BadRequest();
            }

            db.Entry(rolemaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolemasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rolemasters
        [ResponseType(typeof(Rolemaster))]
        public HttpResponseMessage PostRolemaster(Rolemaster rolemaster)
        {
            HttpResponseMessage response = null; ;
            if (!ModelState.IsValid && db.Rolemasters.Any(a => a.Role == rolemaster.Role))
            {
                return response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                
            }
            db.Rolemasters.Add(rolemaster);
            db.SaveChanges();

           return response = new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/Rolemasters/5
        [ResponseType(typeof(Rolemaster))]
        public IHttpActionResult DeleteRolemaster(int id)
        {
            Rolemaster rolemaster = db.Rolemasters.Find(id);
            if (rolemaster == null)
            {
                return NotFound();
            }

            db.Rolemasters.Remove(rolemaster);
            db.SaveChanges();

            return Ok(rolemaster);
        }
        [HttpGet]
        public IHttpActionResult GetCurrentUserRole(string UserID)
        {
            string userrole = db.Users.Where(m => m.UserID == UserID).Select(a => a.UserRoles).FirstOrDefault();
            if(string.IsNullOrEmpty(userrole))
               return Content(HttpStatusCode.OK, "User not Exist");
            return Content(HttpStatusCode.OK, userrole);
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RolemasterExists(int id)
        {
            return db.Rolemasters.Count(e => e.Id == id) > 0;
        }
    }
}