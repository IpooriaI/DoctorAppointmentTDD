using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Infrastructure.Tests;
using DoctorAppointmentTDD.Persistence.EF;
using DoctorAppointmentTDD.Persistence.EF.Doctors;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _dataContext= new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new EFDoctorRepository(_dataContext);
        }

        [Fact]
        public void Add_should_add_Doctor_properly()
        {
            var dto = new AddDoctorDto
            {
                NationalCode = "1234567890",
                FirstName = "DummyName",
                LastName = "DummyFamily",
                Field = "DummyField"
            };


            _sut.Add(dto);



        }



    }

}
