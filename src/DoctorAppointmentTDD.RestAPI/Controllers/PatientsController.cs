using DoctorAppointmentTDD.Services.Patients.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.RestAPI.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : Controller
    {
        private readonly PatientService _service;
        public PatientsController(PatientService patientService)
        {
            _service = patientService;
        }

        [HttpPost]
        public void Add(AddPatientDto dto)
        {
            _service.Add(dto);
        }

        [HttpGet]
        public List<GetPatientDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public GetPatientDto Get(int id)
        {
            return _service.Get(id);
        }

        [HttpPut("{id}")]
        public void Update(int id, UpdatePatientDto dto)
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
