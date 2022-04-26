using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentTDD.RestAPI.Controllers
{
    public class DoctorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
