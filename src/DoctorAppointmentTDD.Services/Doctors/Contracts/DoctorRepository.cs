using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.Services.Doctors.Contracts
{
    public interface DoctorRepository : Repository
    {
        void Add(Doctor doctor);
        bool DoesNationalCodeExist(string nationalCode, int id);
        List<GetDoctorDto> GetAll();
        GetDoctorDto Get(int id);
        Doctor GetById(int id);
    }
}
