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

        private void LoadStudents()
        {
            try
            {
                StudentsDataGrid.ItemsSource = _studentService.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedDate = DateOfBirthPicker.SelectedDate;
                if (!selectedDate.HasValue)
                    throw new ArgumentException("Vui lòng chọn ngày sinh.");

                var student = new Student
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    DateOfBirth = DateOnly.FromDateTime(selectedDate.Value),
                    Gender = GenderTextBox.Text,
                    Class = ClassTextBox.Text,
                    EmergencyContact = EmergencyContactTextBox.Text
                };
                _studentService.AddAsync(student);
                LoadStudents();
                ClearInputs();
                MessageBox.Show("Thêm học sinh thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateStudent(object sender, RoutedEventArgs e)
        {
            if (_selectedStudent == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh để cập nhật.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var selectedDate = DateOfBirthPicker.SelectedDate;
                if (!selectedDate.HasValue)
                    throw new ArgumentException("Vui lòng chọn ngày sinh.");

                _selectedStudent.FirstName = FirstNameTextBox.Text;
                _selectedStudent.LastName = LastNameTextBox.Text;
                _selectedStudent.DateOfBirth = DateOnly.FromDateTime(selectedDate.Value);
                _selectedStudent.Gender = GenderTextBox.Text;
                _selectedStudent.Class = ClassTextBox.Text;
                _selectedStudent.EmergencyContact = EmergencyContactTextBox.Text;
                _studentService.UpdateAsync(_selectedStudent);
                LoadStudents();
                ClearInputs();
                MessageBox.Show("Cập nhật học sinh thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteStudent(object sender, RoutedEventArgs e)
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
                    _studentService.DeleteAsync(_selectedStudent.StudentId);
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
                DateOfBirthPicker.SelectedDate = _selectedStudent.DateOfBirth.ToDateTime(new TimeOnly(0, 0));
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
