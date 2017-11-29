using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace CardApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CardDraw()
        {

            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            WR.UserAgent = "Grand Circus Detroit .NET Framework Test Client";

            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();

            StreamReader reader = new StreamReader(Response.GetResponseStream());

            string CardData = reader.ReadToEnd();

            JObject JsonData = JObject.Parse(CardData);

            string deck = JsonData["deck_id"].ToString();


            HttpWebRequest WR2 = WebRequest.CreateHttp($"https://deckofcardsapi.com/api/deck/{deck}/draw/?count=5");
            WR2.UserAgent = "Grand Circus Detroit .NET Framework Test Client";

            HttpWebResponse Response2 = (HttpWebResponse)WR2.GetResponse();
            HttpStatusCode status = Response2.StatusCode;

            StreamReader reader2 = new StreamReader(Response2.GetResponseStream());

            string CardData2 = reader2.ReadToEnd();
            ViewBag.um = CardData2;
            ViewBag.Status = status;
            JObject JsonData2 = JObject.Parse(CardData2);

            ViewBag.Cards = JsonData2["cards"];
         
            
            List<string> images = new List<string>();
            string image = "";

            for (int i = 0; i <= ViewBag.Cards.Count - 1;i++)
            {

                image = JsonData2["cards"][i]["images"]["png"].ToString();
                images.Add(image);

            }

            ViewBag.Image = images;
            return View();
        }
    }
}