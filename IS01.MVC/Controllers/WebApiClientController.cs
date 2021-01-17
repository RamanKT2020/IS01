using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace IS01.MVC.Controllers
{
    public class WebApiClientController : Controller
    {
        public IActionResult Index()
        {

            //get access token

            var serverClient = new HttpClient();
            serverClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));


            /* Long approach 
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("client_id", "client1");
            param.Add("client_secret", "client1_secret_code");
            param.Add("grant_type", "password");
            param.Add("username", "user1");
            param.Add("password", "password1");
            param.Add("scope", "employeesWebApi roles");

            var content = new FormUrlEncodedContent(param);

            var serverResponse = serverClient.PostAsync("http://localhost:5000/connect/token", content).Result;
            string jsonData = serverResponse.Content.ReadAsStringAsync().Result;
            var accessToken = JsonSerializer.Deserialize<Token>(jsonData);

            */


            var tokenResponse = serverClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                ClientId = "client1",
                ClientSecret = "client1_secret_code",
                GrantType = "password",
                UserName = "user1",
                Password = "password1",
                Scope = "employeesWebApi roles"
                 
            }).Result;



            // Call WebAPI

            var apiClient = new HttpClient();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //apiClient.SetBearerToken(accessToken.access_token); //for the long approach
            apiClient.SetBearerToken(tokenResponse.AccessToken);


            var apiResponse = apiClient.GetAsync("http://localhost:5020/Employees").Result;
            string jsonApiData = apiResponse.Content.ReadAsStringAsync().Result;


            List<string> apiData = JsonSerializer.Deserialize<List<string>>(jsonApiData);



            return View(apiData);
        }
    }
}
