using DoctorAppointmentTDD.Entities;
using DoctorAppointmentTDD.Infrastructure.Application;
using System;
using System.Collections.Generic;

namespace DoctorAppointmentTDD.Services.Appointments.Contracts
{
    public interface AppointmentRepository : Repository
    {
        void Add(Appointment appointment);
        List<GetAppointmentWithDoctorAndPatientDto> GetAll();
        GetAppointmentWithDoctorAndPatientDto Get(int id);
        Appointment GetById(int id);
        //void Delete(Appointment appointment);
        int GetCount(int id, DateTime date);
        bool CheckDuplicate(Appointment appointment);
    }
}
