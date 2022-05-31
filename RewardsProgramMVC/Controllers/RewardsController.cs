using Newtonsoft.Json;
using RewardsProgramMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace RewardsProgramMVC.Controllers
{
    public class RewardsController : Controller
    {
        // GET: Rewards
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllRetailerDetails()
        {
            var retailersList = new List<RetailersModel>();
            retailersList = GetRetailersList();
          
            return View(retailersList);
        }
        public ActionResult Details(int id, int totalDollarsSpent)
        {
            var rewardsModel = new RewardsModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/v2/rewardsprogram");
                var content = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
                //HTTP Post
                var responseTask = client.PostAsync("https://localhost:44378/api/v2/rewardsprogram?TotalDollarsSpent=" + totalDollarsSpent, content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    rewardsModel = JsonConvert.DeserializeObject<RewardsModel>(readTask.Result);
                }
                else //web api sent error response 
                {
                    //log response status here..



                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            var list = GetRetailersList();
            var retailerModel = list.Where(x => x.Id == id).FirstOrDefault();
            retailerModel = retailerModel == null ? new RetailersModel() : retailerModel;
            retailerModel.TotalSpent = totalDollarsSpent;
            retailerModel.Id = id;
            retailerModel.TotalRewardsEarned = rewardsModel.RewardsEarned;
            return View(retailerModel);
        }

        public JsonResult GetRewardsEarned(int id, int totalDollarsSpent)
        {
            var rewardsModel = new RewardsModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/v2/rewardsprogram");
                var content = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
                //HTTP Post
                var responseTask = client.PostAsync("https://localhost:44378/api/v2/rewardsprogram?TotalDollarsSpent=" + totalDollarsSpent, content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    rewardsModel = JsonConvert.DeserializeObject<RewardsModel>(readTask.Result);
                }
                else //web api sent error response 
                {
                    //log response status here..



                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            var json = new { rewardsModel.RewardsEarned };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        private List<RetailersModel> GetRetailersList()
        {
            var retailersList = new List<RetailersModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/v2.0/rewardsprogram");
                //HTTP GET
                var responseTask = client.GetAsync("rewardsprogram");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    retailersList = JsonConvert.DeserializeObject<List<RetailersModel>>(readTask.Result);
                }
                else //web api sent error response 
                {
                    //log response status here..



                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return retailersList;
        }
    }
}
