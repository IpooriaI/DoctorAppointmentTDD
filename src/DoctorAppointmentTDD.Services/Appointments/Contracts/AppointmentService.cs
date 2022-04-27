using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.Services.Appointments.Contracts
{
    public interface AppointmentService : Service
    {
        void Add(AddAppointmentDto dto);
        List<GetAppointmentWithDoctorAndPatientDto> GetAll();
        GetAppointmentWithDoctorAndPatientDto Get(int id);
        void Update(int id, UpdateAppointmentDto dto);
    }
}
