using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;

namespace DoctorAppointmentTDD.Services.Doctors.Contracts
{
    public interface DoctorRepository : Repository
    {
        void Add(Doctor doctor);
    }
}
