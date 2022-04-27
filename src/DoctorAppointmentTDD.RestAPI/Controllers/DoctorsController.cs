using DoctorAppointmentTDD.Services.Doctors.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet]
        public List<GetDoctorDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public GetDoctorDto Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPut("{id}")]
        public void Update(UpdateDoctorDto dto, int id)
        {
            _service.Update(id, dto);
        }

    }
}
