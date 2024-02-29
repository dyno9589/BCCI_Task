using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using BCCIApiConsume.Models;
using Newtonsoft.Json;
using System.Text;

namespace BCCIApiConsume.Controllers
{
    public class BCCIApiController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:44372/api");
        HttpClient client;

        public BCCIApiController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }


        // GET: BCCIApi
        public ActionResult Index()
        {
            List<BcciSery> modelList = new List<BcciSery>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/bcciapi").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<BcciSery>>(data);
            }

            return View(modelList);
        }


        public ActionResult Create()
        {


            return View();
        }



        [HttpPost]
        public ActionResult Create(BcciSery bcciSery)
        {
            string data = JsonConvert.SerializeObject(bcciSery);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/bcciapi", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }



        public ActionResult Edit(int id)
        {
            BcciSery model = new BcciSery();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/bcciapi?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<BcciSery>(data);
            }
            return View("Create", model);
        }



        [HttpPost]
        public ActionResult Edit(BcciSery bcciSery)
        {
            string data = JsonConvert.SerializeObject(bcciSery);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/bcciapi/" + bcciSery.MatchId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }





        public ActionResult Delete(int id)
        {

            BcciSery model = new BcciSery();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/bcciapi?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<BcciSery>(data);
            }

            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult ConfirmDeleteDelete(int id)
        {

            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/bcciapi/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }






    }
}