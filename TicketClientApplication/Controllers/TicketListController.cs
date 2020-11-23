using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketClientApplication.Models;

namespace TicketClientApplication.Controllers
{
    public class TicketListController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TicketListController));

        public IActionResult Index2()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login");

            }
            else
            {
                _log4net.Info("Ticketlist getting Displayed");

                List<Ticket> ItemList = new List<Ticket>();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44331/api/Ticket"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ItemList = JsonConvert.DeserializeObject<List<Ticket>>(apiResponse);
                    }
                }
                return View(ItemList);

            }
        }

        public async Task<IActionResult> Book(int id)
        {
            _log4net.Info("Booking in progess");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                Ticket Item = new Ticket();
                Booking b = new Booking();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44331/api/Ticket/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<Ticket>(apiResponse);
                    }
                    b.Ticket_Id = Item.Ticket_Id;
                    b.Cost = Item.Cost;
                    b.Destination = Item.Destination;
                    b.User_Id = Convert.ToInt32(HttpContext.Session.GetInt32("User_Id"));
                    
                }
                return View(b);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(Booking b)
        {
            _log4net.Info("Booking completed");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                Ticket p = new Ticket();

                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44331/api/Ticket/" + b.Ticket_Id))
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        p = JsonConvert.DeserializeObject<Ticket>(apiResponse);
                    }

                    b.User_Id = Convert.ToInt32(HttpContext.Session.GetInt32("User_Id"));




                    StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("https://localhost:44330/api/Booking/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        b = JsonConvert.DeserializeObject<Booking>(apiResponse);
                    }
                }
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> GetBookingItems(int id)
        {
            _log4net.Info("Getting Booking details");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {
                List<Booking> item = new List<Booking>();
                ViewBag.Username = Convert.ToString(HttpContext.Session.GetString("Username"));
                var name = Convert.ToString(HttpContext.Session.GetString("Username"));


                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    if (HttpContext.Session.GetInt32("User_Id") != null)
                    {
                        id = Convert.ToInt32(HttpContext.Session.GetInt32("User_Id"));
                    }

                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44330/api/Booking/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<List<Booking>>(apiResponse);
                    }
                }
                return View(item);
            }


        }
    }
}