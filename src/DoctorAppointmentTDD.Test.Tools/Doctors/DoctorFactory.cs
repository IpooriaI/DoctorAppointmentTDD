using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentTDD.Test.Tools.Doctors
{
    public static class DoctorFactory
    {

        public static Doctor GenerateDoctor(string firstName,string nationalCode)
        {
            return new Doctor
            {
                FirstName = firstName,
                LastName = "TestLastName",
                Field = "DummyField",
                NationalCode = nationalCode
            };
        }

        public static AddDoctorDto GenerateAddDoctorDto(string firstName, string nationalCode)
        {
            return new AddDoctorDto
            {
                FirstName = firstName,
                LastName = "DummyFamily",
                Field = "DummyField",
                NationalCode = nationalCode,
            };
        }
    }
}
