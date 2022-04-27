using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Services.Doctors.Contracts;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.Test.Tools.Doctors
{
    public static class DoctorFactory
    {

        public static Doctor 
            GenerateDoctor(string firstName, string nationalCode)
        {
            return new Doctor
            {
                FirstName = firstName,
                LastName = "TestLastName",
                Field = "DummyField",
                NationalCode = nationalCode
            };
        }

        public static AddDoctorDto 
            GenerateAddDoctorDto(string firstName, string nationalCode)
        {
            return new AddDoctorDto
            {
                FirstName = firstName,
                LastName = "DummyFamily",
                Field = "DummyField",
                NationalCode = nationalCode,
            };
        }

        public static UpdateDoctorDto 
            GenerateUpdateDoctorDto(string firstName, string nationalCode)
        {
            return new UpdateDoctorDto
            {
                FirstName = firstName,
                LastName = "UpdatedFamily",
                Field = "UpdatedField",
                NationalCode = nationalCode,
            };
        }

        public static List<Doctor> GenerateDoctors()
        {
            return new List<Doctor>
            {
                new Doctor { FirstName = "Name1",LastName
                = "LastName1",Field = "Field1",NationalCode="1234567890"},
                new Doctor { FirstName = "Name2",LastName
                = "LastName2",Field = "Field2",NationalCode="1234567891"},
                new Doctor { FirstName = "Name3",LastName
                = "LastName3",Field = "Field3",NationalCode="1234567892"}
            };
        }
    }
}
