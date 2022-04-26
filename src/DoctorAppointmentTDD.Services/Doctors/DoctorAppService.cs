using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using DoctorAppointmentTDD.Services.Doctors.Exceptions;
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
            var doesDoctorExist = _repository.DoesNationalCodeExist(dto.NationalCode);
            
            
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
    }
}
