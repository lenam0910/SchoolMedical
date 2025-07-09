using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolMedicalWPF.Models;
using SchoolMedicalWPF.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SchoolMedicalWPF.Pages
{
    public partial class StudentPage : Page
    {
        private readonly StudentService _studentService;
        private Student _selectedStudent;

        public StudentPage()
        {
            InitializeComponent();
            _studentService = new StudentService();
            LoadStudents();
        }

        

        private async void LoadStudents()
        {
            try
            {
                StudentsDataGrid.ItemsSource = await _studentService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedDate = DateOfBirthPicker.SelectedDate;
                var student = new Student
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    DateOfBirth = selectedDate.HasValue
                        ? DateOnly.FromDateTime(selectedDate.Value)
                        : DateOnly.FromDateTime(DateTime.Now),
                    Gender = GenderTextBox.Text,
                    Class = ClassTextBox.Text,
                    EmergencyContact = EmergencyContactTextBox.Text
                };
                await _studentService.AddAsync(student);
                LoadStudents();
                ClearInputs();
                MessageBox.Show("Thêm học sinh thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateStudent(object sender, RoutedEventArgs e)
        {
            if (_selectedStudent == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _selectedStudent.FirstName = FirstNameTextBox.Text;
                _selectedStudent.LastName = LastNameTextBox.Text;

                // Fix for CS0019 and CS8604
                var selectedDate = DateOfBirthPicker.SelectedDate;
                _selectedStudent.DateOfBirth = selectedDate.HasValue
                    ? DateOnly.FromDateTime(selectedDate.Value)
                    : DateOnly.FromDateTime(DateTime.Now);

                _selectedStudent.Gender = GenderTextBox.Text;
                _selectedStudent.Class = ClassTextBox.Text;
                _selectedStudent.EmergencyContact = EmergencyContactTextBox.Text;

                await _studentService.UpdateAsync(_selectedStudent);
                LoadStudents();
                ClearInputs();
                MessageBox.Show("Cập nhật học sinh thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteStudent(object sender, RoutedEventArgs e)
        {
            if (_selectedStudent == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa học sinh này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _studentService.DeleteAsync(_selectedStudent.StudentId);
                    LoadStudents();
                    ClearInputs();
                    MessageBox.Show("Xóa học sinh thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedStudent = StudentsDataGrid.SelectedItem as Student;
            if (_selectedStudent != null)
            {
                FirstNameTextBox.Text = _selectedStudent.FirstName;
                LastNameTextBox.Text = _selectedStudent.LastName;
                DateOfBirthPicker.SelectedDate =DateTime.Parse(_selectedStudent.DateOfBirth.ToString());
                GenderTextBox.Text = _selectedStudent.Gender;
                ClassTextBox.Text = _selectedStudent.Class;
                EmergencyContactTextBox.Text = _selectedStudent.EmergencyContact;
            }
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            DateOfBirthPicker.SelectedDate = null;
            GenderTextBox.Text = string.Empty;
            ClassTextBox.Text = string.Empty;
            EmergencyContactTextBox.Text = string.Empty;
            _selectedStudent = null;
        }
    }
}