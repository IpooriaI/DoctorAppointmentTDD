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
using System.Collections.Generic;
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
            var dto = DoctorFactory
                .GenerateAddDoctorDto("Dummy name","1234567890");


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

        [Fact]
        public void Add_should_throw_exception_BadDoctorNationalCodeFormatException_if_national_format_is_wrong()
        {
            var dto = DoctorFactory
                .GenerateAddDoctorDto("Dummy name", "123456789");


            Action expected = () => _sut.Add(dto);


            expected.Should().ThrowExactly<BadDoctorNationalCodeFormat>();
        }

        [Fact]
        public void Add_should_throw_exception_BadDoctorNameFormatException_if_the_doctor_name_format_is_wrong()
        {
            var dto = DoctorFactory
                .GenerateAddDoctorDto("123", "1234567890");


            Action expected = () => _sut.Add(dto);


            expected.Should().ThrowExactly<BadDoctorNameFormatException>();
        }

        [Fact]
        public void GetAll_returns_a_list_of_DoctorDto_properly()
        {
            var Doctors = DoctorFactory.GenerateDoctors();
            _dataContext.Manipulate(_ => _.Doctors.AddRange(Doctors));

            var expected = _sut.GetAll();


            expected.Should().HaveCount(3);
            expected.Should().Contain(_ => _.FirstName == Doctors[0].FirstName);
            expected.Should().Contain(_ => _.FirstName == Doctors[1].FirstName);
            expected.Should().Contain(_ => _.FirstName == Doctors[2].FirstName);
            expected.Should().Contain(_ => _.NationalCode == Doctors[0].NationalCode);
            expected.Should().Contain(_ => _.NationalCode == Doctors[1].NationalCode);
            expected.Should().Contain(_ => _.NationalCode == Doctors[2].NationalCode);
        }

        [Fact]
        public void Get_returns_a_doctorDto_properly()
        {
            var Doctor = DoctorFactory.GenerateDoctor("TestName","1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(Doctor));


            var expected = _sut.Get(Doctor.Id);


            expected.FirstName.Should().Be(Doctor.FirstName);
            expected.NationalCode.Should().Be(Doctor.NationalCode);
        }

        [Fact]
        public void GetById_returns_a_Doctor_properly()
        {
            var Doctor = DoctorFactory.GenerateDoctor("Name", "1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(Doctor));


            var expected = _sut.GetById(Doctor.Id);


            expected.Id.Should().Be(Doctor.Id);
            expected.FirstName.Should().Be(Doctor.FirstName);
            expected.NationalCode.Should().Be(Doctor.NationalCode);
        }

        [Fact]
        public void Update_updates_the_doctor_properly()
        {
            var Doctor = DoctorFactory.GenerateDoctor("Name", "1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(Doctor));
            var dto = new UpdateDoctorDto
            {
                FirstName = "UpdatedName",
                LastName = "UpdatedLastName",
                Field = "UpdatedField",
                NationalCode = "0987654321"
            };


            _sut.Update(Doctor.Id, dto);


            _dataContext.Doctors.Should().HaveCount(1);
            _dataContext.Doctors.Should().Contain(_ => _.FirstName == dto.FirstName);
            _dataContext.Doctors.Should().Contain(_ => _.LastName == dto.LastName);
            _dataContext.Doctors.Should().Contain(_ => _.NationalCode == dto.NationalCode);
        }


    }
}
