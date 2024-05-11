using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace StudentGradesApp
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=\\\"10.0.0.40, 2821\\\";User ID=change;Password=0000;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False\"";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void ConnectToDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectToDatabase())
            {
                MessageBox.Show("Connection successful!");

                DisplayStudentGrades();
            }
            else
            {
                MessageBox.Show("Connection failed!");
            }
        }

        private bool ConnectToDatabase()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        private void DisplayStudentGrades()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM StudentGrades";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    studentGradesListBox.ItemsSource = dataTable.DefaultView;
                }
            }
        }
    }
}