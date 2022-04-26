using DoctorAppointmentTDD.Infrastructure.Application;

namespace DoctorAppointmentTDD.Services.Doctors.Contracts
{
    public interface DoctorService : Service
    {
        void Add(AddDoctorDto dto);
    }
}
