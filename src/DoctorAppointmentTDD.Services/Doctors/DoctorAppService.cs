using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using DoctorAppointmentTDD.Services.Doctors.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace DoctorAppointmentTDD.Services.Doctors
{
    public class DoctorAppService : DoctorService
    {
        private readonly DoctorRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public DoctorAppService(DoctorRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public void Add(AddDoctorDto dto)
        {
            var doctor = new Doctor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Field = dto.Field,
                NationalCode = dto.NationalCode,
            };
            var doesDoctorExist = _repository.DoesNationalCodeExist(dto.NationalCode,0);
            
            
            if(doesDoctorExist)
            {
                throw new DoctorAlreadyExistsException();
            }
            if(dto.NationalCode.Length != 10
                ||doctor.NationalCode
                .Any(char.IsLetter) == true)
            {
                throw new BadDoctorNationalCodeFormat();
            }
            if (doctor.FirstName.Any(char.IsDigit) == true
                || string.IsNullOrEmpty(doctor.FirstName)
                || doctor.LastName.Any(char.IsDigit) == true
                || string.IsNullOrEmpty(doctor.LastName))
            {
                throw new BadDoctorNameFormatException();
            }

            _repository.Add(doctor);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var doctor = _repository.GetById(id);
            _repository.Delete(doctor);
            _unitOfWork.Commit();
        }

        public GetDoctorDto Get(int id)
        {
            return _repository.Get(id);
        }

        public List<GetDoctorDto> GetAll()
        {
            return _repository.GetAll();
        }

        public Doctor GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(int id, UpdateDoctorDto dto)
        {
            var doctor = _repository.GetById(id);


            var isDoctorExist = _repository
                .DoesNationalCodeExist(dto.NationalCode,id);

            if(dto.NationalCode.Length != 10
                ||dto.NationalCode
                .Any(char.IsLetter) == true)
            {
                throw new BadDoctorNationalCodeFormat();
            }

            if (dto.FirstName.Any(char.IsDigit) == true
                || string.IsNullOrEmpty(dto.FirstName)
                || dto.LastName.Any(char.IsDigit) == true
                || string.IsNullOrEmpty(dto.LastName))
            {
                throw new BadDoctorNameFormatException();
            }

            if(isDoctorExist)
            {
                throw new DoctorAlreadyExistsException();
            }


            doctor.FirstName = dto.FirstName;
            doctor.LastName = dto.LastName;
            doctor.NationalCode = dto.NationalCode;
            doctor.Field = dto.Field;


            _unitOfWork.Commit();
        }
    }
}
