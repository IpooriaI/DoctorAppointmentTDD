using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.Services.Doctors.Contracts
{
    public interface DoctorService : Service
    {
        void Add(AddDoctorDto dto);
        List<GetDoctorDto> GetAll();
        GetDoctorDto Get(int id);
        void Update(int id, UpdateDoctorDto dto);
        Doctor GetById(int id);
    }
}
