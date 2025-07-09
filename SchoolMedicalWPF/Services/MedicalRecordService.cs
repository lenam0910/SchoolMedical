using Microsoft.EntityFrameworkCore;
using SchoolMedicalWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMedicalWPF.Services
{
    public class MedicalRecordService
    {
        private readonly SchoolMedicalDbContext _context;

        public MedicalRecordService()
        {
            _context = new SchoolMedicalDbContext();
        }

        public async Task<List<MedicalRecord>> GetAllAsync()
        {
            return await _context.MedicalRecords.Include(m => m.Student).ToListAsync();
        }

        public async Task<MedicalRecord> GetByIdAsync(int id)
        {
            return await _context.MedicalRecords.FindAsync(id);
        }

        public async Task AddAsync(MedicalRecord record)
        {
            if (record.StudentId <= 0)
                throw new ArgumentException("ID học sinh không hợp lệ.");
            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicalRecord record)
        {
            if (record.StudentId <= 0)
                throw new ArgumentException("ID học sinh không hợp lệ.");
            _context.MedicalRecords.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null)
                throw new KeyNotFoundException("Không tìm thấy hồ sơ y tế.");
            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}