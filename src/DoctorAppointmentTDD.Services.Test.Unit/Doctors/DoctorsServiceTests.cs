using DoctorAppointmentTDD.Infrastructure.Application;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentTDD.Services.Test.Unit.Doctors
{
    public class DoctorsServiceTests
    {
        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly DoctorService _sut;
        private readonly DoctorRepository _repository;
    }
}
