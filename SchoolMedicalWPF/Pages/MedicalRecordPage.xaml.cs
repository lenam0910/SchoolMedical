using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolMedicalWPF.Models;
using SchoolMedicalWPF.Services;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolMedicalWPF.Pages
{
    public partial class MedicalRecordPage : Page
    {
        private readonly MedicalRecordService _recordService;
        private readonly StudentService _studentService;
        private MedicalRecord _selectedRecord;

        public MedicalRecordPage()
        {
            InitializeComponent();
           
            _recordService = new MedicalRecordService();
            _studentService = new StudentService();
            LoadData();
        }


        private async void LoadData()
        {
            try
            {
                RecordsDataGrid.ItemsSource = await _recordService.GetAllAsync();
                var students = await _studentService.GetAllAsync();
                StudentComboBox.ItemsSource = students;
                StudentComboBox.DisplayMemberPath = "FullName";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddRecord(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                if (selectedStudent == null)
                    throw new ArgumentException("Vui lòng chọn học sinh.");

                var record = new MedicalRecord
                {
                    StudentId = selectedStudent.StudentId,
                    Condition = ConditionTextBox.Text,
                    Allergies = AllergiesTextBox.Text,
                    Notes = NotesTextBox.Text,
                    RecordedDate = DateTime.Now
                };
                await _recordService.AddAsync(record);
                LoadData();
                ClearInputs();
                MessageBox.Show("Thêm hồ sơ y tế thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateRecord(object sender, RoutedEventArgs e)
        {
            if (_selectedRecord == null)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ y tế để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var selectedStudent = StudentComboBox.SelectedItem as Student;
                if (selectedStudent == null)
                    throw new ArgumentException("Vui lòng chọn học sinh.");

                _selectedRecord.StudentId = selectedStudent.StudentId;
                _selectedRecord.Condition = ConditionTextBox.Text;
                _selectedRecord.Allergies = AllergiesTextBox.Text;
                _selectedRecord.Notes = NotesTextBox.Text;
                await _recordService.UpdateAsync(_selectedRecord);
                LoadData();
                ClearInputs();
                MessageBox.Show("Cập nhật hồ sơ y tế thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (_selectedRecord == null)
            {
                MessageBox.Show("Vui lòng chọn hồ sơ y tế để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa hồ sơ y tế này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _recordService.DeleteAsync(_selectedRecord.RecordId);
                    LoadData();
                    ClearInputs();
                    MessageBox.Show("Xóa hồ sơ y tế thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RecordsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRecord = RecordsDataGrid.SelectedItem as MedicalRecord;
            if (_selectedRecord != null)
            {
                StudentComboBox.SelectedItem = _selectedRecord.Student;
                ConditionTextBox.Text = _selectedRecord.Condition;
                AllergiesTextBox.Text = _selectedRecord.Allergies;
                NotesTextBox.Text = _selectedRecord.Notes;
            }
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            StudentComboBox.SelectedIndex = -1;
            ConditionTextBox.Text = string.Empty;
            AllergiesTextBox.Text = string.Empty;
            NotesTextBox.Text = string.Empty;
            _selectedRecord = null;
        }
    }
}