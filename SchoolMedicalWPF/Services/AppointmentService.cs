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

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.Include(a => a.Student).Include(a => a.Staff).ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task AddAsync(Appointment appointment)
        {
            if (appointment.StudentId <= 0 || appointment.StaffId <= 0)
                throw new ArgumentException("ID học sinh hoặc nhân viên không hợp lệ.");
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            if (appointment.StudentId <= 0 || appointment.StaffId <= 0)
                throw new ArgumentException("ID học sinh hoặc nhân viên không hợp lệ.");
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                throw new KeyNotFoundException("Không tìm thấy lịch hẹn.");
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}