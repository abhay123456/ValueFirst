using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Valuefirst.Models;

namespace Valuefirst
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get { throw new Exception(); } set { throw new Exception(); } }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            RoleMasterClient userClient = new RoleMasterClient();
            UserLogin login = new UserLogin();
            login.UserName = username;
            string roles=userClient.GetCurrentUserRole(username);
            //using (var context = new UserManagementEntities())
            //{
            //    //var result = (from user in context.User
            //    //              join role in context.UserRole on user.Id equals role.UserId
            //    //              where user.UserName == username
            //    //              select role.Role).ToArray();
            //    //return result;
            string[] result = new string[] { roles };
                return result;
            //}
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}