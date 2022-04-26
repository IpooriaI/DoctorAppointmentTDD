using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using DoctorAppointmentTDD.Services.Doctors.Exceptions;

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


            _repository.Add(doctor);
            _unitOfWork.Commit();
        }
    }
}
