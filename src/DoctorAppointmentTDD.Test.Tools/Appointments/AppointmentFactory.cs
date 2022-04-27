using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Services.Appointments.Contracts;
using System;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.Test.Tools.Appointments
{
    public static class AppointmentFactory
    {
        public static AddAppointmentDto GenerateAddAppointmentDto
            (int doctorId, int patientId)
        {
            return new AddAppointmentDto
            {
                Date = DateTime.Now.Date,
                DoctorId = doctorId,
                PatientId = patientId
            };
        }

        public static Appointment GenerateAppointment
            (string doctorNationalCode,string DoctorName)   
        {
            return new Appointment
            {
                Date = DateTime.Now.Date,
                Doctor = new Doctor
                {
                    Field = "testfield",
                    FirstName = DoctorName,
                    LastName = "DummyLName",
                    NationalCode = doctorNationalCode
                },
                Patient = new Patient
                {
                    FirstName = "DummyName",
                    LastName = "DummyLName",
                    NationalCode = "1234567890"
                }
            };
        }

        public static UpdateAppointmentDto GenerateUpdateAppointmentDto
            (int doctorId, int patientId)
        {
            return new UpdateAppointmentDto
            {
                Date = DateTime.Now.Date,
                DoctorId = doctorId,
                PatientId = patientId
            };
        }

        public static List<Appointment> GenerateAppointments(int count)
        {
            var appointments = new List<Appointment>
            {
                new Appointment
                {

                Date = DateTime.Now.Date,
                Doctor = new Doctor
                {
                    Field = "field1",
                    FirstName = "testName",
                    LastName = "testLname",
                    NationalCode ="1234567891"
                },

                Patient = new Patient
                {
                    FirstName = "name1",
                    LastName= "Lname1",
                    NationalCode= "1234567891"
                }

                }};

            for (int i = 1; i < count; i++)
            {
                appointments.Add(new Appointment
                {

                    Date = DateTime.Now.Date,
                    Doctor = appointments[0].Doctor,
                    Patient = new Patient
                    {
                        FirstName = "name" + i,
                        LastName = "Lname" + i,
                        NationalCode = "123456789" + i
                    }
                });
            }

            return appointments;
        }
    }
}
