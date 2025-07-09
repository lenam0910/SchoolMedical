using Microsoft.EntityFrameworkCore;
using SchoolMedicalWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMedicalWPF.Services
{
    public class HealthIncidentService
    {
        private readonly SchoolMedicalDbContext _context;

        public HealthIncidentService()
        {
            _context = new SchoolMedicalDbContext();
        }

        public async Task<List<HealthIncident>> GetAllAsync()
        {
            return await _context.HealthIncidents.Include(h => h.Student).Include(h => h.Staff).ToListAsync();
        }

        public async Task<HealthIncident> GetByIdAsync(int id)
        {
            return await _context.HealthIncidents.FindAsync(id);
        }

        public async Task AddAsync(HealthIncident incident)
        {
            if (incident.StudentId <= 0 || incident.StaffId <= 0)
                throw new ArgumentException("ID học sinh hoặc nhân viên không hợp lệ.");
            _context.HealthIncidents.Add(incident);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HealthIncident incident)
        {
            if (incident.StudentId <= 0 || incident.StaffId <= 0)
                throw new ArgumentException("ID học sinh hoặc nhân viên không hợp lệ.");
            _context.HealthIncidents.Update(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var incident = await _context.HealthIncidents.FindAsync(id);
            if (incident == null)
                throw new KeyNotFoundException("Không tìm thấy sự cố sức khỏe.");
            _context.HealthIncidents.Remove(incident);
            await _context.SaveChangesAsync();
        }
    }
}