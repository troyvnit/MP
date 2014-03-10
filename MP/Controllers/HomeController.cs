using System.Web.Mvc;
using MP.Data.Repository;
using MP.Data.Service;
using MP.Model.Models;

namespace MP.Controllers
{
    public class HomeController : Controller
    {
        private ITripService tripService { get; set; }
        public HomeController(ITripService tripService)
        {
            this.tripService = tripService;
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
    }
}