using DoctorAppointmentTDD.Services.Appointments.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet]
        public List<GetAppointmentWithDoctorAndPatientDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public GetAppointmentWithDoctorAndPatientDto Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPut("{id}")]
        public void Update(int id, UpdateAppointmentDto dto)
        {
            _service.Update(id, dto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
