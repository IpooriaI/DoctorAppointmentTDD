using DoctorAppointmentTDD.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentTDD.Persistence.EF.Doctors
{
    public class EFDoctorRepository : DoctorRepository
    {
        private readonly EFDataContext _dataContext;
        public EFDoctorRepository(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
