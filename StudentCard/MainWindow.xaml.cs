using System;
using System.Collections.Generic;
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
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            rbMale.IsChecked = false;
            rbFemale.IsChecked = false;
            cmbCourse.SelectedIndex = -1;
            cmbSpecialization.SelectedIndex = -1;
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
    }
}
