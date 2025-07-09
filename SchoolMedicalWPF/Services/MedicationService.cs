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

        public List<Medication> GetAllAsync()
        {
            return  _context.Medications.Include(m => m.Student).Include(m => m.PrescribedByNavigation).ToList();
        }

        public  Medication GetByIdAsync(int id)
        {
            return  _context.Medications.Find(id);
        }

        public void AddAsync(Medication medication)
        {
            if (medication.StudentId <= 0 || string.IsNullOrWhiteSpace(medication.Name))
                throw new ArgumentException("ID học sinh hoặc tên thuốc không hợp lệ.");
            _context.Medications.Add(medication);
            _context.SaveChanges();
        }

        public void UpdateAsync(Medication medication)
        {
            if (medication.StudentId <= 0 || string.IsNullOrWhiteSpace(medication.Name))
                throw new ArgumentException("ID học sinh hoặc tên thuốc không hợp lệ.");
            _context.Medications.Update(medication);
            _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            var medication =  _context.Medications.Find(id);
            if (medication == null)
                throw new KeyNotFoundException("Không tìm thấy thuốc.");
            _context.Medications.Remove(medication);
            _context.SaveChanges();
        }
    }
}