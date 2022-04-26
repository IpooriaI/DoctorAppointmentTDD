using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentTDD.RestAPI.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
