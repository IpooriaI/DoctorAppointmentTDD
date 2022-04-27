using DoctorAppointmentTDD.Services.Appointments.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentTDD.RestAPI.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly AppointmentService _service;

        public AppointmentsController(AppointmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddAppointmentDto dto)
        {
            _service.Add(dto);
        }
    }
}
