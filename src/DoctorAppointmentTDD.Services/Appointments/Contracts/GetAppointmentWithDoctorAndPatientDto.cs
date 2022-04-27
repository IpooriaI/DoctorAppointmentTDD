using DoctorAppointmentTDD.Services.Doctors.Contracts;
using DoctorAppointmentTDD.Services.Patients.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentTDD.Services.Appointments.Contracts
{
    public class GetAppointmentWithDoctorAndPatientDto
    {
        public DateTime Date { get; set; }
        public GetDoctorDto Doctor { get; set; }
        public GetPatientDto Patient { get; set; }
    }
}
