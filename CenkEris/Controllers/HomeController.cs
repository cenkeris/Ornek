using CenkEris.Models;
using CenkEris.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CenkEris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        SaveRecords sv = new SaveRecords();

        public IActionResult Index(int page = 1)
        {

            List<Root> personList = new List<Root>();
            var client = new RestClient("https://reqres.in/api/users?page=" + page + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=df64bcd34a9e4b9808de3f31dece79ddb1615073990");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            personList= new List<Root> { JsonConvert.DeserializeObject<Root>(response.Content) };
            ViewBag.Page = page;
            sv.Save("List","",response.Content);
            return View(personList);
        }
        public IActionResult View(int id)
        {
            List<ViewRoot> personList = new List<ViewRoot>();
            var client = new RestClient("https://reqres.in/api/users/" + id + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=df64bcd34a9e4b9808de3f31dece79ddb1615073990");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.Content == "{}")
            {
                return RedirectToAction("Index");
            }
            personList = new List<ViewRoot> { JsonConvert.DeserializeObject<ViewRoot>(response.Content) };
            sv.Save("Detail", "", response.Content);
            return View(personList);
            
            
           
        }
        public IActionResult Delete(int id)
        {
            var client = new RestClient("https://reqres.in/api/users/" + id + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Cookie", "__cfduid=df64bcd34a9e4b9808de3f31dece79ddb1615073990");
            IRestResponse response = client.Execute(request);
            sv.Save("Delete", "", response.Content);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            List<ViewRoot> personList = new List<ViewRoot>();
            var client = new RestClient("https://reqres.in/api/users/" + id + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__cfduid=df64bcd34a9e4b9808de3f31dece79ddb1615073990");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.Content == "{}")
            {
                return RedirectToAction("Index");
            }
            personList = new List<ViewRoot> { JsonConvert.DeserializeObject<ViewRoot>(response.Content) };
            var person = personList.FirstOrDefault();
            EditViewModel form = new EditViewModel();
            form.id = person.data.id;
            form.email = person.data.email;
            form.avatar = person.data.avatar;
            form.first_name = person.data.first_name;
            form.last_name = person.data.last_name;
            return View(form);
        }
        [HttpPost]
        public IActionResult Edit(int id,EditViewModel form)
        {
            var client = new RestClient("https://reqres.in/api/users/"+id+"");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            string requestval = "{\r\n         \r\n        \"email\": \"" + form.email + "\",\r\n        \"first_name\": \"" + form.first_name + "\",\r\n        \"last_name\": \"" + form.last_name + "\",\r\n        \"avatar\": \""+form.avatar+"\"\r\n    \r\n}\r\n";
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "__cfduid=df64bcd34a9e4b9808de3f31dece79ddb1615073990");
            request.AddParameter("application/json",requestval , ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            sv.Save("Update", requestval, response.Content);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AddViewModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var client = new RestClient("https://reqres.in/api/users/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            string requestval = "{\r\n         \r\n        \"email\": \"" + form.email + "\",\r\n        \"first_name\": \"" + form.first_name + "\",\r\n        \"last_name\": \"" + form.last_name + "\",\r\n        \"avatar\": \"" + form.avatar + "\"\r\n    \r\n}\r\n";
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "__cfduid=df64bcd34a9e4b9808de3f31dece79ddb1615073990");
            request.AddParameter("application/json", requestval, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            sv.Save("Create", requestval, response.Content);
            return RedirectToAction("Index");
        }



    }
}
