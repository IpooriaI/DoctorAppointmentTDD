using DoctorAppointmentTDD.Services.Doctors.Contracts;
using DoctorAppointmentTDD.Services.Patients.Contracts;
using System;

namespace DoctorAppointmentTDD.Services.Appointments.Contracts
{
    public class GetAppointmentWithDoctorAndPatientDto
    {
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public GetDoctorDto Doctor { get; set; }

        public int PatientId { get; set; }
        public GetPatientDto Patient { get; set; }
    }
}
