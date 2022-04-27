using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Services.Appointments.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentTDD.Test.Tools.Appointments
{
    public static class AppointmentFactory
    {
        public static AddAppointmentDto
            GenerateAddAppointmentDto(int doctorId,int patientId)
        {
            return new AddAppointmentDto
            {
                Date = DateTime.Now.Date,
                DoctorId = doctorId,
                PatientId = patientId
            };
        }

        public static Appointment
            GenerateAppointment(int doctorId, int patientId)
        {
            return new Appointment
            {
                Date = DateTime.Now.Date,
                DoctorId = doctorId,
                PatientId = patientId
            };
        }

    }
}
