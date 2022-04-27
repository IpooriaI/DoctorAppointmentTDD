using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Services.Patients.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace DoctorAppointmentTDD.Persistence.EF.Patients
{
    public class EFPatientRepository : PatientRepository
    {
        private readonly EFDataContext _dataContext;
        public EFPatientRepository(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Patient patient)
        {
            _dataContext.Patients.Add(patient);
        }

        public void Delete(Patient patient)
        {
            _dataContext.Patients.Remove(patient);
        }

        public bool DoesNationalCodeExist(string nationalCode, int id)
        {
            return _dataContext.Patients
                .Any(_ => _.NationalCode == nationalCode && _.Id != id);
        }

        public GetPatientDto Get(int id)
        {
            return _dataContext.Patients
                .Where(_ => _.Id == id)
                .Select(_ => new GetPatientDto
                {
                    FirstName = _.FirstName,
                    LastName = _.LastName,
                    NationalCode = _.NationalCode
                }).FirstOrDefault();
        }

        public List<GetPatientDto> GetAll()
        {
            return _dataContext.Patients.Select(_ => new GetPatientDto
            {
                FirstName = _.FirstName,
                LastName = _.LastName,
                NationalCode = _.NationalCode
            }).ToList();
        }

        public Patient GetById(int id)
        {
            return _dataContext.Patients
                .FirstOrDefault(_ => _.Id == id);
        }
    }
}
