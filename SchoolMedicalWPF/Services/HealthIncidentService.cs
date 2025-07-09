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

        public List<HealthIncident> GetAllAsync()
        {
            return  _context.HealthIncidents.Include(h => h.Student).Include(h => h.Staff).ToList();
        }

        public HealthIncident GetByIdAsync(int id)
        {
            return  _context.HealthIncidents.Find(id);
        }

        public void AddAsync(HealthIncident incident)
        {
            if (incident.StudentId <= 0 || incident.StaffId <= 0)
                throw new ArgumentException("ID học sinh hoặc nhân viên không hợp lệ.");
            _context.HealthIncidents.Add(incident);
            _context.SaveChanges();
        }

        public void UpdateAsync(HealthIncident incident)
        {
            if (incident.StudentId <= 0 || incident.StaffId <= 0)
                throw new ArgumentException("ID học sinh hoặc nhân viên không hợp lệ.");
            _context.HealthIncidents.Update(incident);
            _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            var incident =  _context.HealthIncidents.Find(id);
            if (incident == null)
                throw new KeyNotFoundException("Không tìm thấy sự cố sức khỏe.");
            _context.HealthIncidents.Remove(incident);
            _context.SaveChanges();
        }
    }
}