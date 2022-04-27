using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Infrastructure.Tests;
using DoctorAppointmentTDD.Persistence.EF;
using DoctorAppointmentTDD.Persistence.EF.Appointments;
using DoctorAppointmentTDD.Services.Appointments;
using DoctorAppointmentTDD.Services.Appointments.Contracts;
using DoctorAppointmentTDD.Services.Appointments.Exceptions;
using DoctorAppointmentTDD.Test.Tools.Appointments;
using DoctorAppointmentTDD.Test.Tools.Doctors;
using DoctorAppointmentTDD.Test.Tools.Patients;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DoctorAppointmentTDD.Services.Test.Unit.Appointments
{
    public class AppointmentServiceTests
    {
        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly AppointmentService _sut;
        private readonly AppointmentRepository _repository;
        public AppointmentServiceTests()
        {
            _dataContext = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new EFAppointmentRepository(_dataContext);
            _sut = new AppointmentAppService(_repository, _unitOfWork);
        }

        [Fact]
        public void Add_adds_an_appointment_properly()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("TestName", "1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var patient = PatientFactory
                .GeneratePatient("TestName","1478523690");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var dto = AppointmentFactory
                .GenerateAddAppointmentDto(doctor.Id, patient.Id);


            _sut.Add(dto);


            _dataContext.Appointments.Should().Contain(_ => _.DoctorId == dto.DoctorId);
        }

        [Fact]
        public void Add_Throws_exception_VisitTimeIsFullException_if_doctor_has_now_visit_time_today()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("TestName","1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var patients = PatientFactory
                .GeneratePatients();
            patients.AddRange(PatientFactory.GeneratePatients());
            _dataContext.Manipulate(_ => _.Patients.AddRange(patients));
            var appointments = new List<Appointment>
            {
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[0].Id,Date=DateTime.Now.Date,},
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[1].Id,Date=DateTime.Now.Date,},
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[2].Id,Date=DateTime.Now.Date,},
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[3].Id,Date=DateTime.Now.Date,},
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[4].Id,Date=DateTime.Now.Date,},
            };
            _dataContext
                .Manipulate(_ => _.Appointments.AddRange(appointments));
            var appointment = 
                AppointmentFactory
                .GenerateAddAppointmentDto(doctor.Id,patients[5].Id);

            Action expected =()=> _sut.Add(appointment);

            expected.Should().ThrowExactly<VisitTimeIsFullException>();
        }

        [Fact]
        public void Add_throws_exception_DuplicateAppointmentException_if_this_appointment_already_exists_today()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("TestName","1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var patient = PatientFactory
                .GeneratePatient("TestName","1234567899");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var appointment = AppointmentFactory
                .GenerateAppointment(doctor.Id,patient.Id);
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));
            var dto = AppointmentFactory
                .GenerateAddAppointmentDto(appointment.DoctorId,
                appointment.PatientId);

            Action expected = () => _sut.Add(dto);

            expected.Should().ThrowExactly<DuplicateAppointmentException>();
        }

        [Fact]
        public void Get_returns_a_appointment_and_its_patient_and_doctor_properly()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("TestName","1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var patient = PatientFactory
                .GeneratePatient("TestName","1234567899");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var appointment = AppointmentFactory
                .GenerateAppointment(doctor.Id,patient.Id);
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));


            var expected = _sut.Get(appointment.Id);


            expected.Date.Should().Be(appointment.Date.Date);
            expected.PatientId.Should().Be(appointment.PatientId);
            expected.DoctorId.Should().Be(appointment.DoctorId);
        }

        [Fact]
        public void GetAll_returns_list_of_appointments_and_its_patients_and_doctors_properly()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("TestName", "1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var patients = PatientFactory
                .GeneratePatients();
            patients.AddRange(PatientFactory.GeneratePatients());
            _dataContext.Manipulate(_ => _.Patients.AddRange(patients));
            var appointments = new List<Appointment>
            {
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[0].Id,Date=DateTime.Now.Date,},
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[1].Id,Date=DateTime.Now.Date,},
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[2].Id,Date=DateTime.Now.Date,},
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[3].Id,Date=DateTime.Now.Date,},
                new Appointment{DoctorId=doctor
                .Id,PatientId=patients[4].Id,Date=DateTime.Now.Date,},
            };
            _dataContext
                .Manipulate(_ => _.Appointments.AddRange(appointments));


            var expected = _sut.GetAll();


            expected.Should().HaveCount(5);
            expected.Should().Contain(_ => _.DoctorId == doctor.Id);
            expected.Should().Contain(_ => _.PatientId == patients[0].Id);
            expected.Should().Contain(_ => _.PatientId == patients[1].Id);
        }
    }
}
