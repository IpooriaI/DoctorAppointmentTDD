using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using System.Collections.Generic;
using System.Linq;

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

        public bool DoesNationalCodeExist(string nationalCode)
        {
            return _dataContext.Doctors.Any(_ => _.NationalCode == nationalCode);
        }

        public List<GetDoctorDto> GetAll()
        {
            return _dataContext.Doctors.Select(_ => new GetDoctorDto
            {
                FirstName = _.FirstName,
                LastName = _.LastName,
                Field = _.Field,
                NationalCode = _.NationalCode
            }).ToList();
        }
    }
}
