using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.Services.Patients.Contracts
{
    public interface PatientRepository : Repository
    {
        void Add(Patient patient);
        bool DoesNationalCodeExist(string nationalCode, int id);
        List<GetPatientDto> GetAll();
        GetPatientDto Get(int id);
        Patient GetById(int id);
        void Delete(Patient patient);
    }
}
