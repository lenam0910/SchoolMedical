using Microsoft.EntityFrameworkCore;
using SchoolMedicalWPF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolMedicalWPF.Services
{
    public class DocumentService
    {
        private readonly SchoolMedicalDbContext _context;

        public DocumentService()
        {
            _context = new SchoolMedicalDbContext();

        }

        public async Task<List<Document>> GetAllAsync()
        {
            return await _context.Documents.Include(d => d.Student).Include(d => d.UploadedByNavigation).ToListAsync();
        }

        public async Task<Document> GetByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);
        }

        public async Task AddAsync(Document document)
        {
            if (document.StudentId <= 0 || string.IsNullOrWhiteSpace(document.FileName))
                throw new ArgumentException("ID học sinh hoặc tên tệp không hợp lệ.");
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document document)
        {
            if (document.StudentId <= 0 || string.IsNullOrWhiteSpace(document.FileName))
                throw new ArgumentException("ID học sinh hoặc tên tệp không hợp lệ.");
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
                throw new KeyNotFoundException("Không tìm thấy tài liệu.");
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }
    }
}