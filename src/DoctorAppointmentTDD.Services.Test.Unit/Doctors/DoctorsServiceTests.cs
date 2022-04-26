using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Infrastructure.Tests;
using DoctorAppointmentTDD.Persistence.EF;
using DoctorAppointmentTDD.Persistence.EF.Doctors;
using DoctorAppointmentTDD.Services.Doctors;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using DoctorAppointmentTDD.Services.Doctors.Exceptions;
using DoctorAppointmentTDD.Test.Tools.Doctors;
using FluentAssertions;
using System;
using Xunit;

namespace DoctorAppointmentTDD.Services.Test.Unit.Doctors
{
    public class DoctorsServiceTests
    {
        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly DoctorService _sut;
        private readonly DoctorRepository _repository;
        public DoctorsServiceTests()
        {
            _dataContext = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new EFDoctorRepository(_dataContext);
            _sut = new DoctorAppService(_repository, _unitOfWork);
        }


        [Fact]
        public void Add_should_add_Doctor_properly()
        {
            var dto = new AddDoctorDto
            {
                FirstName = "DummyName",
                LastName = "DummyFamily",
                Field = "DummyField",
                NationalCode = "1234567890",
            };


            _sut.Add(dto);


            _dataContext.Doctors.Should()
                .Contain(_ => _.FirstName == dto.FirstName);
        }

        [Fact]
        public void Add_should_throw_exception_DoctorAlreadyExistsException_if_doctor_already_exists()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("Dummy name", "1234567890");
            var dto = DoctorFactory
                .GenerateAddDoctorDto("Dummy name", "1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));


            Action expected = () => _sut.Add(dto);


            expected.Should().ThrowExactly<DoctorAlreadyExistsException>();
        }


    }


}
