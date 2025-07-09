using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SchoolMedicalWPF.Models;
using SchoolMedicalWPF.Services;
using SchoolMedicalWPF.Windows;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.User;

namespace SchoolMedicalWPF
{
    public partial class Dashboard : Window
    {
        private readonly StudentService _studentService;
        private readonly AppointmentService _appointmentService;
        private readonly Staff _currentStaff;
        private readonly ChatBotAI _chatBot;

        public Dashboard(Staff staff)
        {
            InitializeComponent();
            _currentStaff = staff;
            _studentService = new StudentService();
            _appointmentService = new AppointmentService();
            _chatBot = new ChatBotAI();
            LoadDashboardData();
            MainFrame.Navigate(new Pages.StudentPage());
        }

        private async void LoadDashboardData()
        {
            try
            {
                TotalStudents.Text = (await _studentService.GetAllAsync()).Count.ToString();
                TodayAppointments.Text = (await _appointmentService.GetAllAsync())
                    .Count(a => a.AppointmentDate.Date == DateTime.Today).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NavigateToStudents(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.StudentPage());
        private void NavigateToStaff(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.StaffPage());
        private void NavigateToMedicalRecords(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.MedicalRecordPage());
        private void NavigateToMedications(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.MedicationPage());
        private void NavigateToAppointments(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.AppointmentPage());
        private void NavigateToHealthIncidents(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.HealthIncidentPage());
        private void NavigateToAuditLogs(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.AuditLogPage());
        private void NavigateToNotifications(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.NotificationPage());
        private void NavigateToDocuments(object sender, RoutedEventArgs e) => MainFrame.Navigate(new Pages.DocumentPage());

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            ChatPopup.Visibility = ChatPopup.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            if (ChatPopup.Visibility == Visibility.Visible)
            {
                ChatDisplay.Text = "Chào bạn! Hãy nhập tin nhắn...\n";
                ChatInput.Focus();
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ChatInput.Text))
            {
                var userInput = ChatInput.Text;
                ChatDisplay.Text += $"👤 Bạn: {userInput}\n";
                var response = await _chatBot.SendRequestAndGetResponse(userInput);
                ChatDisplay.Text += $"🤖 Tư vấn viên: {response}\n";
                ChatInput.Text = string.Empty;
            }
        }

        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(ChatInput.Text))
            {
                SendButton_Click(sender, e);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ChatPopup.Visibility = Visibility.Collapsed;
        }
    }
}