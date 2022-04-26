using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentTDD.RestAPI.Controllers
{
    public class AppointmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
