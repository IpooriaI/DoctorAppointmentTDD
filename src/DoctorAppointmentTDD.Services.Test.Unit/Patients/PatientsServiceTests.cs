using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Infrastructure.Tests;
using DoctorAppointmentTDD.Persistence.EF;
using DoctorAppointmentTDD.Persistence.EF.Patients;
using DoctorAppointmentTDD.Services.Patients;
using DoctorAppointmentTDD.Services.Patients.Contracts;
using DoctorAppointmentTDD.Services.Patients.Exceptions;
using DoctorAppointmentTDD.Test.Tools.Patients;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace DoctorAppointmentTDD.Services.Test.Unit.Patients
{
    public class PatientsServiceTests
    {

        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly PatientService _sut;
        private readonly PatientRepository _repository;
        public PatientsServiceTests()
        {
            _dataContext = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new EFPatientRepository(_dataContext);
            _sut = new PatientAppService(_repository, _unitOfWork);
        }


        [Fact]
        public void Add_adds_patient_properly()
        {
            var dto = PatientFactory
                .GenerateAddPatientDto("Dummy name", "1234567890");


            _sut.Add(dto);


            _dataContext.Patients.Should()
                .Contain(_ => _.FirstName == dto.FirstName);
        }

        [Fact]
        public void Add_should_throw_exception_PatientAlreadyExistsException_if_patient_already_exists()
        {
            var patient = PatientFactory
                .GeneratePatient("Dummy name", "1234567890");
            var dto = PatientFactory
                .GenerateAddPatientDto("Dummy name", "1234567890");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));


            Action expected = () => _sut.Add(dto);


            expected.Should().ThrowExactly<PatientAlreadyExistsException>();
        }

        [Fact]
        public void Add_should_throw_exception_BadPatientNationalCodeFormatException_if_national_format_is_wrong()
        {
            var dto = PatientFactory
                .GenerateAddPatientDto("Dummy name", "123456789");


            Action expected = () => _sut.Add(dto);


            expected.Should().ThrowExactly<BadPatientNationalCodeFormat>();
        }

        [Fact]
        public void Add_should_throw_exception_BadPatientNameFormatException_if_the_patient_name_format_is_wrong()
        {
            var dto = PatientFactory
                .GenerateAddPatientDto("123", "1234567890");


            Action expected = () => _sut.Add(dto);


            expected.Should().ThrowExactly<BadPatientNameFormatException>();
        }

        [Fact]
        public void GetAll_returns_a_list_of_GetPatientDto_properly()
        {
            var patients = PatientFactory.GeneratePatients();
            _dataContext.Manipulate(_ => _.Patients.AddRange(patients));

            var expected = _sut.GetAll();


            expected.Should().HaveCount(3);
            expected.Should()
                .Contain(_ => _.FirstName == patients[0].FirstName);
            expected.Should()
                .Contain(_ => _.FirstName == patients[1].FirstName);
            expected.Should()
                .Contain(_ => _.FirstName == patients[2].FirstName);
            expected.Should()
                .Contain(_ => _.NationalCode == patients[0].NationalCode);
            expected.Should()
                .Contain(_ => _.NationalCode == patients[1].NationalCode);
            expected.Should()
                .Contain(_ => _.NationalCode == patients[2].NationalCode);
        }

        [Fact]
        public void Get_returns_a_GetPatientDto_properly()
        {
            var patient = PatientFactory
                .GeneratePatient("TestName", "1234567890");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));


            var expected = _sut.Get(patient.Id);


            expected.FirstName.Should().Be(patient.FirstName);
            expected.NationalCode.Should().Be(patient.NationalCode);
        }

        [Fact]
        public void GetById_returns_a_Patient_properly()
        {
            var patient = PatientFactory
                .GeneratePatient("Name", "1234567890");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));


            var expected = _sut.GetById(patient.Id);


            expected.Id.Should().Be(patient.Id);
            expected.FirstName.Should().Be(patient.FirstName);
            expected.NationalCode.Should().Be(patient.NationalCode);
        }

        [Fact]
        public void Update_updates_the_Patient_properly()
        {
            var patient = PatientFactory
                .GeneratePatient("Name", "9876543210");
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var dto = PatientFactory
                .GenerateUpdatePatientDto("UpdatedFirstname", "0147852369");


            _sut.Update(patient.Id, dto);


            _dataContext.Patients.Should().HaveCount(1);
            _dataContext.Patients.Should()
                .Contain(_ => _.FirstName == dto.FirstName);
            _dataContext.Patients.Should()
                .Contain(_ => _.LastName == dto.LastName);
            _dataContext.Patients.Should()
                .Contain(_ => _.NationalCode == dto.NationalCode);
        }

        [Fact]
        public void Update_throws_PatientDosntExistException_if_the_Patient_dosnt_exist()
        {
            var dto = PatientFactory
                .GenerateUpdatePatientDto("UpdatedFirstname", "0147852369");
            var fakePatientId = 20;

            Action expected = () => _sut.Update(fakePatientId,dto);

            expected.Should().ThrowExactly<PatientDosntExistException>();
        }

        [Fact]
        public void Update_throws_exception_PatientAlreadyExistsException_if_patient_already_exists()
        {
            var patient = PatientFactory
                .GeneratePatient("Dummy name", "1234567890");
            var patient2 = PatientFactory
                .GeneratePatient("Dummy name", "1234567891");
            var dto = PatientFactory
                .GenerateUpdatePatientDto("UpdatedFirstname", "1234567891");
            _dataContext.Manipulate(_ => _.Patients.AddRange(patient));
            _dataContext.Manipulate(_ => _.Patients.AddRange(patient2));


            Action expected = () => _sut.Update(patient.Id, dto);


            expected.Should().ThrowExactly<PatientAlreadyExistsException>();
        }

        [Fact]
        public void Update_throws_exception_BadPatientNationalCodeFormatException_if_patient_nationalcode_format_is_wrong()
        {
            var patient = PatientFactory
                .GeneratePatient("Dummy name", "1234567890");
            var dto = PatientFactory
                .GenerateUpdatePatientDto("UpdatedFirstname",
                "123456badnationalcode");
            _dataContext.Manipulate(_ => _.Patients.AddRange(patient));


            Action expected = () => _sut.Update(patient.Id, dto);


            expected.Should().ThrowExactly<BadPatientNationalCodeFormat>();
        }

        [Fact]
        public void Update_throws_exception_BadPatientNameFormatException_if_the_patient_name_format_is_wrong()
        {
            var patient = PatientFactory
                .GeneratePatient("Dummy name", "1234567890");
            var dto = PatientFactory
                .GenerateUpdatePatientDto("badname2", "1234567898");
            _dataContext.Manipulate(_ => _.Patients.AddRange(patient));


            Action expected = () => _sut.Update(patient.Id, dto);


            expected.Should().ThrowExactly<BadPatientNameFormatException>();
        }

        [Fact]
        public void Delete_deletes_the_patient_properly()
        {
            var patients = PatientFactory.GeneratePatients();
            _dataContext.Manipulate(_ => _.Patients.AddRange(patients));
            patients.Should().HaveCount(3);

            _sut.Delete(patients[0].Id);

            patients = _dataContext.Patients.ToList();
            patients.Should().HaveCount(2);
        }

        [Fact]
        public void Delete_throws_PatientDosntExistException_if_the_Patient_dosnt_exist()
        {
            var fakeAppointmentId = 20;

            Action expected = () => _sut.Delete(fakeAppointmentId);

            expected.Should().ThrowExactly<PatientDosntExistException>();
        }
    }
}
