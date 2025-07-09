using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using SchoolMedicalWPF.Models;
using SchoolMedicalWPF.Services;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolMedicalWPF.Pages
{
    public partial class DocumentPage : Page
    {
        private readonly DocumentService _documentService;
        private readonly StudentService _studentService;
        private readonly StaffService _staffService;
        private Document _selectedDocument;

        public DocumentPage()
        {
            InitializeComponent();
           
            _documentService = new DocumentService();
            _studentService = new StudentService();
            _staffService = new StaffService();
            LoadData();
        }


        private async void LoadData()
        {
            try
            {
                DocumentsDataGrid.ItemsSource = await _documentService.GetAllAsync();
                StudentComboBox.ItemsSource = await _studentService.GetAllAsync();
                UploadedByComboBox.ItemsSource = await _staffService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FileNameTextBox.Text = Path.GetFileName(openFileDialog.FileName);
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private async void AddDocument(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                var selectedStaff = UploadedByComboBox.SelectedItem as Staff;
                if (selectedStudent == null || selectedStaff == null || string.IsNullOrWhiteSpace(FileNameTextBox.Text))
                    throw new ArgumentException("Vui lòng chọn học sinh, người tải lên và tệp.");

                var document = new Document
                {
                    StudentId = selectedStudent.StudentId,
                    FileName = FileNameTextBox.Text,
                    FilePath = FilePathTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    UploadedBy = selectedStaff.StaffId,
                    UploadedAt = DateTime.Now
                };
                await _documentService.AddAsync(document);
                LoadData();
                ClearInputs();
                MessageBox.Show("Thêm tài liệu thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateDocument(object sender, RoutedEventArgs e)
        {
            if (_selectedDocument == null)
            {
                MessageBox.Show("Vui lòng chọn tài liệu để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                var selectedStaff = UploadedByComboBox.SelectedItem as Staff;
                if (selectedStudent == null || selectedStaff == null || string.IsNullOrWhiteSpace(FileNameTextBox.Text))
                    throw new ArgumentException("Vui lòng chọn học sinh, người tải lên và tệp.");

                _selectedDocument.StudentId = selectedStudent.StudentId;
                _selectedDocument.FileName = FileNameTextBox.Text;
                _selectedDocument.FilePath = FilePathTextBox.Text;
                _selectedDocument.Description = DescriptionTextBox.Text;
                _selectedDocument.UploadedBy = selectedStaff.StaffId;
                await _documentService.UpdateAsync(_selectedDocument);
                LoadData();
                ClearInputs();
                MessageBox.Show("Cập nhật tài liệu thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteDocument(object sender, RoutedEventArgs e)
        {
            if (_selectedDocument == null)
            {
                MessageBox.Show("Vui lòng chọn tài liệu để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa tài liệu này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _documentService.DeleteAsync(_selectedDocument.DocumentId);
                    LoadData();
                    ClearInputs();
                    MessageBox.Show("Xóa tài liệu thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DocumentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDocument = DocumentsDataGrid.SelectedItem as Document;
            if (_selectedDocument != null)
            {
                StudentComboBox.SelectedItem = _selectedDocument.Student;
                FileNameTextBox.Text = _selectedDocument.FileName;
                FilePathTextBox.Text = _selectedDocument.FilePath;
                DescriptionTextBox.Text = _selectedDocument.Description;
                UploadedByComboBox.SelectedItem = _selectedDocument.UploadedByNavigation;
            }
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            StudentComboBox.SelectedIndex = -1;
            FileNameTextBox.Text = string.Empty;
            FilePathTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
            UploadedByComboBox.SelectedIndex = -1;
            _selectedDocument = null;
        }
    }
}