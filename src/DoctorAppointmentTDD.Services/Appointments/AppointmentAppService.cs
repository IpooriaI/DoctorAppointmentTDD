using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Services.Appointments.Contracts;
using DoctorAppointmentTDD.Services.Appointments.Exceptions;

namespace DoctorAppointmentTDD.Services.Appointments
{
    public class AppointmentAppService : AppointmentService
    {
        private readonly AppointmentRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public AppointmentAppService(AppointmentRepository repository
            , UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                Date = dto.Date,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId
            };

            var TodayAppointments = _repository
                .GetCount(dto.DoctorId, dto.Date);

            var duplicateAppointment = _repository
                .CheckDuplicate(appointment);

            if (duplicateAppointment == true)
            {
                throw new DuplicateAppointmentException();
            }

            if (TodayAppointments >= 5)
            {
                throw new VisitTimeIsFullException();
            }


            _repository.Add(appointment);
            _unitOfWork.Commit();
        }

        public GetAppointmentWithDoctorAndPatientDto Get(int id)
        {
            return _repository.Get(id);
        }
    }
}
