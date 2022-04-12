using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace MDiSUBD_WPF
{
    /// <summary>
    /// Логика взаимодействия для AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        private string connectionString;
        private MySqlConnection connection;
        private MainWindow mainWindow;
        public AddCustomerWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = null;
            mainWindow = new MainWindow();
        }

        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sqlCustomer = "INSERT INTO mdisubd.customer(first_name, last_name, telephone_number) " +
                    "VALUES ('" + firstNameTextBox.Text + "', '" + lastNameTextBox.Text + "', '" + phoneNumberTextBox.Text + "')";
                connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(sqlCustomer, connection);
                connection.Open();
                command.ExecuteNonQuery();
                mainWindow.Logging("Добавлен заказчик");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                this.Close();
            }
        }
    }
}
