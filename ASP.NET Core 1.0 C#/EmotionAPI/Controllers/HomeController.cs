﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net.Http.Headers;
using EmotionAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace EmotionAPI.Controllers
{
    public class HomeController : Controller
    {
        //_apiKey: Replace this with your own Project Oxford Emotion API key, please do not use my key. I inlcude it here so you can get up and running quickly but you can get your own key for free at https://www.projectoxford.ai/emotion 
        public const string _apiKey = "1dd1f4e23a5743139399788aa30a7153";

        //_apiUrl: The base URL for the API. Find out what this is for other APIs via the API documentation
        public const string _baseApiUrl = "https://api.projectoxford.ai/";

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> URLExample()
        {
            using (var httpClient = new HttpClient())
            {
                //setup HttpClient
                httpClient.BaseAddress = new Uri(_baseApiUrl);
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //setup data
                var dataO = new URLData()
                {
                    url = "https://oxfordportal.blob.core.windows.net/emotion/recognition1.jpg"
                };
                var serialisedData = new StringContent(dataO.ToString());
                serialisedData.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                //make request
                var response = await httpClient.PostAsync(_baseApiUrl, serialisedData);

                //read response
                var responseContent = await response.Content.ReadAsStringAsync();

                //parse response and write to view
                dynamic d = JObject.Parse(responseContent);
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}