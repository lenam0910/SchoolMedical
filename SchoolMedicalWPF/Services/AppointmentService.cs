using Microsoft.EntityFrameworkCore;
using SchoolMedicalWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMedicalWPF.Services
{
    public class AppointmentService
    {
        private readonly SchoolMedicalDbContext _context;

        public AppointmentService()
        {
            _context = new SchoolMedicalDbContext();
        }

        public List<Appointment> GetAllAsync()
        {
            return  _context.Appointments.Include(a => a.Student).Include(a => a.Staff).ToList();
        }

        public Appointment GetByIdAsync(int id)
        {
            return  _context.Appointments.Find(id);
        }

        public void AddAsync(Appointment appointment)
        {
            if (appointment.StudentId <= 0 || appointment.StaffId <= 0)
                throw new ArgumentException("ID học sinh hoặc nhân viên không hợp lệ.");
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void UpdateAsync(Appointment appointment)
        {
            if (appointment.StudentId <= 0 || appointment.StaffId <= 0)
                throw new ArgumentException("ID học sinh hoặc nhân viên không hợp lệ.");
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
                throw new KeyNotFoundException("Không tìm thấy lịch hẹn.");
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }
    }
}