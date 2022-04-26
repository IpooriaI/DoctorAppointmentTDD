using DoctorAppointmentTDD.Services.Doctors.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentTDD.RestAPI.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : Controller
    {
        private readonly DoctorService _service;

        public DoctorsController(DoctorService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddDoctorDto dto)
        {
            _service.Add(dto);
        }

    }
}
