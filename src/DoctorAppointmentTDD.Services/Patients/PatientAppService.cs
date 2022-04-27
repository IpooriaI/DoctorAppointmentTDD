using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Services.Patients.Contracts;
using DoctorAppointmentTDD.Services.Patients.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace DoctorAppointmentTDD.Services.Patients
{
    public class PatientAppService : PatientService
    {
        private readonly PatientRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public PatientAppService(PatientRepository repository
            , UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddPatientDto dto)
        {
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                NationalCode = dto.NationalCode,
            };
            var doesPatientExist = _repository
                .DoesNationalCodeExist(dto.NationalCode, 0);


            if (doesPatientExist)
            {
                throw new PatientAlreadyExistsException();
            }
            if (dto.NationalCode.Length != 10
                || patient.NationalCode
                .Any(char.IsLetter) == true)
            {
                throw new BadPatientNationalCodeFormat();
            }
            if (patient.FirstName.Any(char.IsDigit) == true
                || string.IsNullOrEmpty(patient.FirstName)
                || patient.LastName.Any(char.IsDigit) == true
                || string.IsNullOrEmpty(patient.LastName))
            {
                throw new BadPatientNameFormatException();
            }

            _repository.Add(patient);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var patient = _repository.GetById(id);

            if(patient == null)
            {
                throw new PatientDosntExistException();
            }

            _repository.Delete(patient);
            _unitOfWork.Commit();
        }

        public GetPatientDto Get(int id)
        {
            return _repository.Get(id);
        }

        public List<GetPatientDto> GetAll()
        {
            return _repository.GetAll();
        }

        public Patient GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(int id, UpdatePatientDto dto)
        {
            var patient = _repository.GetById(id);

            var isPatientExist = _repository
                .DoesNationalCodeExist(dto.NationalCode, id);
            
            if(patient == null)
            {
                throw new PatientDosntExistException();
            }

            if (dto.NationalCode.Length != 10
                || dto.NationalCode
                .Any(char.IsLetter) == true)
            {
                throw new BadPatientNationalCodeFormat();
            }

            if (dto.FirstName.Any(char.IsDigit) == true
                || string.IsNullOrEmpty(dto.FirstName)
                || dto.LastName.Any(char.IsDigit) == true
                || string.IsNullOrEmpty(dto.LastName))
            {
                throw new BadPatientNameFormatException();
            }

            if (isPatientExist)
            {
                throw new PatientAlreadyExistsException();
            }


            patient.FirstName = dto.FirstName;
            patient.LastName = dto.LastName;
            patient.NationalCode = dto.NationalCode;



            _unitOfWork.Commit();
        }
    }
}
