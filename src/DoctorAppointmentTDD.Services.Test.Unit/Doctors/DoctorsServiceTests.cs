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
using System.Linq;
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
                .GenerateAddDoctorDto("Dummy name", "1234567890");


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
            var doctors = DoctorFactory.GenerateDoctors();
            _dataContext.Manipulate(_ => _.Doctors.AddRange(doctors));


            var expected = _sut.GetAll();


            expected.Should().HaveCount(3);
            expected.Should()
                .Contain(_ => _.FirstName == doctors[0].FirstName);
            expected.Should()
                .Contain(_ => _.FirstName == doctors[1].FirstName);
            expected.Should()
                .Contain(_ => _.FirstName == doctors[2].FirstName);
            expected.Should()
                .Contain(_ => _.NationalCode == doctors[0].NationalCode);
            expected.Should()
                .Contain(_ => _.NationalCode == doctors[1].NationalCode);
            expected.Should()
                .Contain(_ => _.NationalCode == doctors[2].NationalCode);
        }

        [Fact]
        public void Get_returns_a_doctorDto_properly()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("TestName", "1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));


            var expected = _sut.Get(doctor.Id);


            expected.FirstName.Should().Be(doctor.FirstName);
            expected.NationalCode.Should().Be(doctor.NationalCode);
        }

        [Fact]
        public void GetById_returns_a_Doctor_properly()
        {
            var doctor = DoctorFactory.GenerateDoctor("Name", "1234567890");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));


            var expected = _sut.GetById(doctor.Id);


            expected.Id.Should().Be(doctor.Id);
            expected.FirstName.Should().Be(doctor.FirstName);
            expected.NationalCode.Should().Be(doctor.NationalCode);
        }

        [Fact]
        public void Update_updates_the_doctor_properly()
        {
            var doctor = DoctorFactory.GenerateDoctor("Name", "9876543210");
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var dto = DoctorFactory
                .GenerateUpdateDoctorDto("UpdatedFirstname", "0147852369");


            _sut.Update(doctor.Id, dto);


            _dataContext.Doctors.Should().HaveCount(1);
            _dataContext.Doctors.Should()
                .Contain(_ => _.FirstName == dto.FirstName);
            _dataContext.Doctors.Should()
                .Contain(_ => _.LastName == dto.LastName);
            _dataContext.Doctors.Should()
                .Contain(_ => _.NationalCode == dto.NationalCode);
        }

        [Fact]
        public void Update_throws_DoctorDosntExistException_if_the_doctor_dosnt_exist()
        {
            var dto = DoctorFactory
                .GenerateUpdateDoctorDto("UpdatedFirstname", "0147852369");
            var fakeDoctorId = 20;


            Action expected = () => _sut.Update(fakeDoctorId, dto);


            expected.Should().ThrowExactly<DoctorDosntExistException>();
        }

        [Fact]
        public void Update_throws_exception_DoctorAlreadyExistsException_if_doctor_already_exists()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("Dummy name", "1234567890");
            var doctor2 = DoctorFactory
                .GenerateDoctor("Dummy name", "1234567891");
            var dto = DoctorFactory
                .GenerateUpdateDoctorDto("UpdatedFirstname", "1234567891");
            _dataContext.Manipulate(_ => _.Doctors.AddRange(doctor));
            _dataContext.Manipulate(_ => _.Doctors.AddRange(doctor2));


            Action expected = () => _sut.Update(doctor.Id, dto);


            expected.Should().ThrowExactly<DoctorAlreadyExistsException>();
        }

        [Fact]
        public void Update_throws_exception_BadDoctorNationalCodeFormatException_if_national_format_is_wrong()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("Dummy name", "1234567890");
            var dto = DoctorFactory
                .GenerateUpdateDoctorDto("UpdatedFirstname",
                "123456badnationalcode");
            _dataContext.Manipulate(_ => _.Doctors.AddRange(doctor));


            Action expected = () => _sut.Update(doctor.Id, dto);


            expected.Should().ThrowExactly<BadDoctorNationalCodeFormat>();
        }

        [Fact]
        public void Update_throws_exception_BadDoctorNameFormatException_if_the_doctor_name_format_is_wrong()
        {
            var doctor = DoctorFactory
                .GenerateDoctor("Dummy name", "1234567890");
            var dto = DoctorFactory
                .GenerateUpdateDoctorDto("badname2", "1234567898");
            _dataContext.Manipulate(_ => _.Doctors.AddRange(doctor));


            Action expected = () => _sut.Update(doctor.Id, dto);


            expected.Should().ThrowExactly<BadDoctorNameFormatException>();
        }

        [Fact]
        public void Delete_deletes_the_doctor_properly()
        {
            var Doctors = DoctorFactory.GenerateDoctors();
            _dataContext.Manipulate(_ => _.Doctors.AddRange(Doctors));
            Doctors.Should().HaveCount(3);


            _sut.Delete(Doctors[0].Id);


            Doctors = _dataContext.Doctors.ToList();
            Doctors.Should().HaveCount(2);
        }

        [Fact]
        public void Delete_throws_DoctorDosntExistException_if_the_doctor_dosnt_exist()
        {
            var fakeAppointmentId = 20;


            Action expected = () => _sut.Delete(fakeAppointmentId);


            expected.Should().ThrowExactly<DoctorDosntExistException>();
        }
    }
}
