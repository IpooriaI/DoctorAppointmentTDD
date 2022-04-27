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
using System.Linq;
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
                .GeneratePatient("TestName", "1478523690");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var dto = AppointmentFactory
                .GenerateAddAppointmentDto(doctor.Id, patient.Id);


            _sut.Add(dto);


            _dataContext.Appointments.Should()
                .Contain(_ => _.DoctorId == dto.DoctorId);
            _dataContext.Appointments.Should()
                .Contain(_ => _.PatientId == dto.PatientId);
            _dataContext.Appointments.Should()
                .Contain(_ => _.Date == dto.Date);
        }

        [Fact]
        public void Add_Throws_exception_VisitTimeIsFullException_if_doctor_has_no_visit_time_today()
        {
            var patient = PatientFactory.GeneratePatient("Name", "1234567856");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var appointments = AppointmentFactory.GenerateAppointments(5);
            _dataContext.Manipulate(_ => _.Appointments.AddRange(appointments));
            var dto = AppointmentFactory.GenerateAddAppointmentDto(
                appointments[0].DoctorId, patient.Id);


            Action expected = () => _sut.Add(dto);


            expected.Should().ThrowExactly<VisitTimeIsFullException>();
        }

        [Fact]
        public void Add_throws_exception_DuplicateAppointmentException_if_this_appointment_already_exists_today()
        {
            var appointment = AppointmentFactory
                .GenerateAppointment("1234567893", "DummyName");
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));
            var dto = AppointmentFactory.GenerateAddAppointmentDto(
                appointment.DoctorId, appointment.PatientId);


            Action expected = () => _sut.Add(dto);


            expected.Should().ThrowExactly<DuplicateAppointmentException>();
        }

        [Fact]
        public void Get_returns_a_appointment_and_its_patient_and_doctor_properly()
        {
            var appointment = AppointmentFactory
                .GenerateAppointment("1234567890", "TestName");
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));


            var expected = _sut.Get(appointment.Id);


            expected.Date.Should().Be(appointment.Date.Date);
            expected.PatientId.Should().Be(appointment.PatientId);
            expected.DoctorId.Should().Be(appointment.DoctorId);
        }

        [Fact]
        public void GetAll_returns_list_of_appointments_and_its_patients_and_doctors_properly()
        {
            var appointments = AppointmentFactory.GenerateAppointments(3);
            _dataContext.Manipulate(_ => _.Appointments
            .AddRange(appointments));


            var expected = _sut.GetAll();


            expected.Should().HaveCount(3);
            expected.Should().Contain(_ => _.DoctorId == appointments[0].DoctorId);
            expected.Should().Contain(_ => _.PatientId == appointments[0].PatientId);
            expected.Should().Contain(_ => _.DoctorId == appointments[1].DoctorId);
            expected.Should().Contain(_ => _.PatientId == appointments[1].PatientId);
            expected.Should().Contain(_ => _.DoctorId == appointments[2].DoctorId);
            expected.Should().Contain(_ => _.PatientId == appointments[2].PatientId);
        }

        [Fact]
        public void Update_updates_the_Appointment_properly()
        {
            var dtoDoctor = DoctorFactory
                .GenerateDoctor("UpdatedName", "12345678888");
            _dataContext.Manipulate(_ => _.Doctors.Add(dtoDoctor));
            var dtoPatient = PatientFactory
                .GeneratePatient("TestName", "1234567899");
            _dataContext.Manipulate(_ => _.Patients.Add(dtoPatient));
            var appointment = AppointmentFactory
                .GenerateAppointment("1234567888", "DummyName");
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));
            var dto = AppointmentFactory
                .GenerateUpdateAppointmentDto(dtoDoctor.Id, dtoPatient.Id);


            _sut.Update(appointment.Id, dto);


            _dataContext.Appointments.Should()
                .Contain(_ => _.PatientId == dto.PatientId);
            _dataContext.Appointments.Should()
                .Contain(_ => _.DoctorId == dto.DoctorId);
            _dataContext.Appointments.Should()
                .Contain(_ => _.Date == dto.Date);
        }

        [Fact]
        public void Update_throws_AppointmentDosntExistException_if_the_appointment_dosnt_exist()
        {
            var fakeAppointmentId = 20;
            var dto = AppointmentFactory.GenerateUpdateAppointmentDto(1, 2);

            Action expected = () => _sut.Update(fakeAppointmentId, dto);


            expected.Should().ThrowExactly<AppointmentDosntExistException>();
        }

        [Fact]
        public void Update_Throws_exception_VisitTimeIsFullException_if_doctor_has_no_visit_time_today()
        {
            var appointments = AppointmentFactory.GenerateAppointments(5);
            _dataContext.Manipulate(_ => _.Appointments.AddRange(appointments));
            var appointment = AppointmentFactory
                .GenerateAppointment("1234567654", "DummyName");
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));
            var dto = AppointmentFactory.GenerateUpdateAppointmentDto(
                appointments[0].DoctorId, appointment.PatientId);


            Action expected = () => _sut.Update(appointment.Id, dto);


            expected.Should().ThrowExactly<VisitTimeIsFullException>();
        }

        [Fact]
        public void Update_throws_exception_DuplicateAppointmentException_if_this_appointment_already_exists_today()
        {
            var appointments = AppointmentFactory.GenerateAppointments(3);
            _dataContext.Manipulate(_ => _.Appointments.AddRange(appointments));
            var appointment = AppointmentFactory
                .GenerateAppointment("1234567654", "DummyName");
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));
            var dto = AppointmentFactory.GenerateUpdateAppointmentDto(
                appointments[0].DoctorId, appointments[0].DoctorId);


            Action expected = () => _sut.Update(appointment.Id, dto);


            expected.Should().ThrowExactly<DuplicateAppointmentException>();
        }

        [Fact]
        public void Delete_deletes_the_Appointment_properly()
        {
            var appointments = AppointmentFactory.GenerateAppointments(2);
            _dataContext.Manipulate(_ => _.Appointments
            .AddRange(appointments));


            _sut.Delete(appointments[0].Id);


            _dataContext.Appointments.Count().Should()
                .NotBe(2);
            _dataContext.Appointments.Should()
                .NotContain(_ => _.Id == appointments[0].Id);
        }

        [Fact]
        public void Delete_throws_AppointmentDosntExistException_if_the_appointment_dosnt_exist()
        {
            var fakeAppointmentId = 20;

            Action expected = () => _sut.Delete(fakeAppointmentId);

            expected.Should().ThrowExactly<AppointmentDosntExistException>();
        }
    }
}
