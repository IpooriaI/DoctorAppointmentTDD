using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Infrastructure.Tests;
using DoctorAppointmentTDD.Persistence.EF;
using DoctorAppointmentTDD.Persistence.EF.Appointments;
using DoctorAppointmentTDD.Services.Appointments;
using DoctorAppointmentTDD.Services.Appointments.Contracts;
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


    }
}
