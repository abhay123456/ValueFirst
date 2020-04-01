using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Script.Serialization;

namespace Valuefirst.Models
{
    public class RoleMasterClient
    {
        private string Base_URL = "http://localhost:8080/api/rolemasters/";

        public IEnumerable<RoleMaster> GetRoles()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("GetRolemasters").Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<RoleMaster>>().Result;
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public string GetCurrentUserRole(string userid)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("GetCurrentUserRole?UserID="+ userid).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<string>().Result;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public RoleMaster find(int? id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("GetRolemaster/" + id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<RoleMaster>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool Create(RoleMaster roleMaster)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("PostRolemaster", roleMaster).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(int ?id,RoleMaster rolemaster)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("PutRolemaster/" + rolemaster.Id, rolemaster).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("DeleteRolemaster/" + id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    
       
    }
}