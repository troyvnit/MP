using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MP.Data.Repository;
using MP.Data.Service;
using MP.Model.Models;
using MP.Model.SearchModels;
using MP.Models;
using Newtonsoft.Json;

namespace MP.Controllers
{
    public class HomeController : Controller
    {
        private ITripService tripService { get; set; }
        private IPassengerService passengerService { get; set; }
        private IItemService itemService { get; set; }
        public HomeController(ITripService tripService, IPassengerService passengerService, IItemService itemService)
        {
            this.tripService = tripService;
            this.passengerService = passengerService;
            this.itemService = itemService;
        }
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

        public ActionResult TripContent(TripName tripName)
        {
            var trip = new Trip { TripName = tripName };
            return View(trip);
        }

        public ActionResult GetPassenger(TripModel tripModel)
        {
            var passengers = passengerService.GetPassengers(Mapper.Map<TripModel, Trip>(tripModel)).Select(Mapper.Map<Passenger, PassengerModel>);
            return Json(passengers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdatePassenger(PassengerModel passengerModel, TripModel tripModel)
        {
            var passenger = Mapper.Map<PassengerModel, Passenger>(passengerModel);
            var trip = tripService.AddOrUpdateTripFollowDepartureInfo(Mapper.Map<TripModel, Trip>(tripModel));
            passenger.Trip = trip;
            passenger.TripId = trip.Id;
            passengerService.AddOrUpdatePassenger(passenger);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItem(ItemSearchModel parameter)
        {
            var items = itemService.GetItems(parameter).Select(Mapper.Map<Item, ItemModel>);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdateItem(string models, TripModel tripModel)
        {
            var items = JsonConvert.DeserializeObject<List<ItemModel>>(models);
            foreach (var itemModel in items)
            {
                var item = Mapper.Map<ItemModel, Item>(itemModel);
                var trip = tripService.AddOrUpdateTripFollowDepartureInfo(Mapper.Map<TripModel, Trip>(tripModel));
                item.Trip = trip;
                item.TripId = trip.Id;
                itemService.AddOrUpdateItem(item);
                itemModel.Id = item.Id;
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}