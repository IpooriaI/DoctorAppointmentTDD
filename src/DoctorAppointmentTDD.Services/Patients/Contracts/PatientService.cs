using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.Services.Patients.Contracts
{
    public interface PatientService : Service
    {
        void Add(AddPatientDto dto);
        List<GetPatientDto> GetAll();
        GetPatientDto Get(int id);
        void Update(int id, UpdatePatientDto dto);
        Patient GetById(int id);
        void Delete(int id);
    }
}
