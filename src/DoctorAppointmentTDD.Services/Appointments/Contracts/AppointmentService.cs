using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;

namespace DoctorAppointmentTDD.Services.Appointments.Contracts
{
    public interface AppointmentService : Service
    {
        void Add(AddAppointmentDto dto);
        GetAppointmentWithDoctorAndPatientDto Get(int id);
    }
}
