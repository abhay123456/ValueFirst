using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Script.Serialization;

namespace Valuefirst.Models
{
    public class Account
    {
        private string Base_URL = "http://localhost:8080/api/Ragistration/";

        public string RagisterUser(User user)
        {
            string message = string.Empty;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("RagisterUser", user).Result;
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {

                        var result = content.ReadAsStringAsync();
                        //message = (new JavaScriptSerializer()).Deserialize<List<string>>(response.Content.ReadAsStringAsync().Result);
                        message = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return message;
        }

        public User Login(UserLogin user)
        {
            List<User> Logeduser = new List<User>();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync("Login", user).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        var result = content.ReadAsStringAsync();
                        Logeduser = (new JavaScriptSerializer()).Deserialize<List<User>>(response.Content.ReadAsStringAsync().Result);
                      
                    }
                }
             
            }
            catch (Exception ex)
            {

            }
            if(Logeduser.Count()>0)
            return Logeduser[0];

            return new User();
        }

        


    }
}