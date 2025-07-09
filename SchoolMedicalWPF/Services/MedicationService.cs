using Microsoft.EntityFrameworkCore;
using SchoolMedicalWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMedicalWPF.Services
{
    public class MedicationService
    {
        private readonly SchoolMedicalDbContext _context;

        public MedicationService()
        {
            _context = new SchoolMedicalDbContext();
        }

        public async Task<List<Medication>> GetAllAsync()
        {
            return await _context.Medications.Include(m => m.Student).Include(m => m.PrescribedByNavigation).ToListAsync();
        }

        public async Task<Medication> GetByIdAsync(int id)
        {
            return await _context.Medications.FindAsync(id);
        }

        public async Task AddAsync(Medication medication)
        {
            if (medication.StudentId <= 0 || string.IsNullOrWhiteSpace(medication.Name))
                throw new ArgumentException("ID học sinh hoặc tên thuốc không hợp lệ.");
            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Medication medication)
        {
            if (medication.StudentId <= 0 || string.IsNullOrWhiteSpace(medication.Name))
                throw new ArgumentException("ID học sinh hoặc tên thuốc không hợp lệ.");
            _context.Medications.Update(medication);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication == null)
                throw new KeyNotFoundException("Không tìm thấy thuốc.");
            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();
        }
    }
}