using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Services.Patients.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentTDD.Test.Tools.Patients
{
    public class PatientFactory
    {
        public static Patient GeneratePatient(string firstName, string nationalCode)
        {
            return new Patient
            {
                FirstName = firstName,
                LastName = "TestLastName",
                NationalCode = nationalCode
            };
        }

        public static AddPatientDto GenerateAddPatientDto(string firstName, string nationalCode)
        {
            return new AddPatientDto
            {
                FirstName = firstName,
                LastName = "DummyFamily",
                NationalCode = nationalCode,
            };
        }

        public static UpdatePatientDto GenerateUpdatePatientDto(string firstName, string nationalCode)
        {
            return new UpdatePatientDto
            {
                FirstName = firstName,
                LastName = "UpdatedFamily",
                NationalCode = nationalCode,
            };
        }

        public static List<Patient> GeneratePatients()
        {
            return new List<Patient>
            {
                new Patient { FirstName = "Name1",LastName
                = "LastName1",NationalCode="1234567890"},
                new Patient { FirstName = "Name2",LastName
                = "LastName2",NationalCode="1234567891"},
                new Patient { FirstName = "Name3",LastName
                = "LastName3",NationalCode="1234567892"}
            };
        }
    }
}
