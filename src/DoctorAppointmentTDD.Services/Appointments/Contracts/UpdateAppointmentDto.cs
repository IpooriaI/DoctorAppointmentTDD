using System;

namespace DoctorAppointmentTDD.Services.Appointments.Contracts
{
    public class UpdateAppointmentDto
    {
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
