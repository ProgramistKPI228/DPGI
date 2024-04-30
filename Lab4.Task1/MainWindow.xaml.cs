using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4.Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataAdapter _studentRepository = new DataAdapter();
        private List<Student> students;

        public MainWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            students = _studentRepository.GetAllStudents();
            list.ItemsSource = students;
            ResetFields();
        }

        private void ResetFields()
        {
            StudentNumberTextBox.Text = "";
            FirstNameTextBox.Text = "";
            SecondNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            GroupNameTextBox.Text = "";
            AddressTextBox.Text = "";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student()
            {
                FirstName = FirstNameTextBox.Text,
                SecondName = SecondNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                GroupName = GroupNameTextBox.Text,
                Address = AddressTextBox.Text
            };
            ResetFields();
            _studentRepository.CreateStudent(newStudent);
            LoadStudents();
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(StudentNumberTextBox.Text, out int studentNumber))
            {
                Student selectedStudent = new Student()
                {
                    StudentNumber = studentNumber,
                    FirstName = FirstNameTextBox.Text,
                    SecondName = SecondNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    GroupName = GroupNameTextBox.Text,
                    Address = AddressTextBox.Text
                };
                _studentRepository.UpdateStudent(selectedStudent);
                LoadStudents();
            }
            else
            {
                MessageBox.Show("Перевірте валідність даних");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                Student selectedStudent = list.SelectedItem as Student;
                _studentRepository.DeleteStudent(selectedStudent.StudentNumber);
                LoadStudents();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть студента для видалення.");
            }
        }
    }

}