using System.Collections.Generic;

namespace DoctorAppointmentTDD.Entities
{
    public class Doctor : Person
    {
        public Doctor()
        {
            Appointments = new List<Appointment>();
        }
        public string Field { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
