using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Services.Doctors.Contracts;

namespace DoctorAppointmentTDD.Persistence.EF.Doctors
{
    public class EFDoctorRepository : DoctorRepository
    {
        private readonly EFDataContext _dataContext;
        public EFDoctorRepository(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Doctor doctor)
        {
            _dataContext.Doctors.Add(doctor);
        }
    }
}
