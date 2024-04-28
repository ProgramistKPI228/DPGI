using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Task1
{
    public class DataAdapter
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT StudentNumber, FirstName, SecondName, LastName, GroupName, Address FROM Students", connection);
                DataTable studentTable = new DataTable();
                adapter.Fill(studentTable);

                foreach (DataRow row in studentTable.Rows)
                {
                    Student student = new Student()
                    {
                        StudentNumber = Convert.ToInt32(row["StudentNumber"]),
                        FirstName = row["FirstName"].ToString(),
                        SecondName = row["SecondName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        GroupName = row["GroupName"].ToString(),
                        Address = row["Address"].ToString()
                    };
                    students.Add(student);
                }
            }
            return students;
        }

        public void UpdateStudent(Student student)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(@"
            UPDATE Students SET 
            FirstName=@FirstName, 
            SecondName=@SecondName, 
            LastName=@LastName, 
            GroupName=@GroupName, 
            Address=@Address 
            WHERE StudentNumber=@StudentNumber", connection);

            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@SecondName", student.SecondName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@GroupName", student.GroupName);
            command.Parameters.AddWithValue("@Address", student.Address);
            command.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void CreateStudent(Student student)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(@"
            INSERT INTO Students 
            (FirstName, SecondName, LastName, GroupName, Address) VALUES 
            (@FirstName, @SecondName, @LastName, @GroupName, @Address)", connection);

            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@SecondName", student.SecondName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@GroupName", student.GroupName);
            command.Parameters.AddWithValue("@Address", student.Address);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void DeleteStudent(int studentNumber)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("DELETE FROM Students WHERE StudentNumber=@StudentNumber", connection);
            command.Parameters.AddWithValue("@StudentNumber", studentNumber);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
