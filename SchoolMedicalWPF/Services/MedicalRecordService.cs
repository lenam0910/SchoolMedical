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

        public  List<MedicalRecord> GetAllAsync()
        {
            return  _context.MedicalRecords.Include(m => m.Student).ToList();
        }

        public MedicalRecord GetByIdAsync(int id)
        {
            return  _context.MedicalRecords.Find(id);
        }

        public void AddAsync(MedicalRecord record)
        {
            if (record.StudentId <= 0)
                throw new ArgumentException("ID học sinh không hợp lệ.");
            _context.MedicalRecords.Add(record);
             _context.SaveChanges();
        }

        public void UpdateAsync(MedicalRecord record)
        {
            if (record.StudentId <= 0)
                throw new ArgumentException("ID học sinh không hợp lệ.");
            _context.MedicalRecords.Update(record);
            _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            var record =  _context.MedicalRecords.Find(id);
            if (record == null)
                throw new KeyNotFoundException("Không tìm thấy hồ sơ y tế.");
            _context.MedicalRecords.Remove(record);
            _context.SaveChanges();
        }
    }
}