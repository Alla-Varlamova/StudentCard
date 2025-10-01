using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class ProgressSumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return 0.0;

            const int totalFields = 11;
            int filledFields = 0;

            foreach (var value in values)
            {
                // Для текстовых полей: проверяем, что строка не пустая
                if (value is string str && !string.IsNullOrEmpty(str))
                    filledFields++;
                else if (value is double doubleValue && doubleValue > 0)  // Для Value (слайдер)
                    filledFields++;
                else if (value is bool boolValue && boolValue)  // Для IsChecked (радио-кнопки)
                    filledFields++;
                else if (value is DateTime?)  // Для SelectedDate (DatePicker)
                    filledFields++;  // Если не null, считаем заполненным
                else if (value is int intValue && intValue > 0)
                    filledFields++;
                else if (value is int Value && Value > 0)  // Для Source.Length (int > 0 = изображение загружено)
                    filledFields++;
            }

            double progress = (filledFields / (double)totalFields) * 100;
            return Math.Round(progress, 2);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnLoadPhoto.Click += btnLoadPhoto_Click;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) ||
                string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Заполните обязательные поля!");
                return;
            }

            string gender = rbMale.IsChecked == true ? "Мужской" : "Женский";
            string message =
               $"Студент: {txtFirstName.Text} {txtLastName.Text}\n" +
               $"Возраст: {txtAge.Text}\nПол: {gender}\n" +
               $"Email: {txtEmail.Text}\nТелефон: {txtPhone.Text}\n" +
               $"Курс: {(cmbCourse.SelectedItem as ComboBoxItem)?.Content}\n" +
               $"Специализация: {(cmbSpecialization.SelectedItem as ComboBoxItem)?.Content}";

            MessageBox.Show(message, "Данные сохранены");

        }

        private void btrClear_click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAge.Text = string.Empty;
            dpDateBirthday.SelectedDate = null;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            rbMale.IsChecked = false;
            rbFemale.IsChecked = false;
            cmbCourse.SelectedIndex = -1;
            cmbSpecialization.SelectedIndex = -1;
            sliderGrade.Value = sliderGrade.Minimum;
            txtGradeDisplay.Text = string.Empty;
            imgPhoto.Source = null;
        }

        private void btnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.png)|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double grade = e.NewValue; // Получаем новое значение Slider (оценка в баллах)
            // Обновляем отображение в TextBlock 
            if (txtGradeDisplay != null)
            {
                txtGradeDisplay.Text = $"{grade:F0} баллов"; // F0 для целого числа
            }
        }

}
}
