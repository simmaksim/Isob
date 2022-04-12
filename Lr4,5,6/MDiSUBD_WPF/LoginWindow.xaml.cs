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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MySqlConnection connection;
        private string connectionString;

        public LoginWindow()
        {
            InitializeComponent();
            connection = null;
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string login = signInLoginTextBox.Text;
            string password = signInPasswordTextBox.Text;

            string loginSql = "SELECT user.id, user.login, user.password FROM mdisubd.user WHERE user.login = '" + login + "' AND user.password = '" + password + "'";
            
            try
            {
                connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(loginSql, connection);
                connection.Open();

                object loginCheck = command.ExecuteScalar();

                if(loginCheck != null)
                {                    
                    MainWindow.currentUserId = (int)loginCheck;
                    string adminSql = "SELECT user.is_admin FROM mdisubd.user WHERE user.id = " + MainWindow.currentUserId;
                    command = new MySqlCommand(adminSql, connection);
                    object adminCheck = command.ExecuteScalar();
                    if(adminCheck != null)
                        MainWindow.currentAdmin = (int)adminCheck;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Logging("Пользователь вошел в аккаунт");
                    this.Close();
                    mainWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

           
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            string registerLogin = signUpLoginTextBox.Text;
            string registerPassword = signUpPasswordTextBox.Text;

            string registerSql = "SELECT user.login FROM mdisubd.user WHERE user.login = '" + registerLogin + "'";

            try
            {
                connection = new MySqlConnection(connectionString);
                MySqlCommand registercommand = new MySqlCommand(registerSql, connection);
                connection.Open();

                object registerCheck = registercommand.ExecuteScalar();

                if(registerCheck == null)
                {
                    string registerInsert = "INSERT INTO mdisubd.user(login, password) " + 
                        "VALUES ('" + registerLogin + "', '" + registerPassword + "')";
                    MySqlCommand insertCommand = new MySqlCommand(registerInsert, connection);
                    insertCommand.ExecuteNonQuery();

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    MainWindow.currentUserId = (int)insertCommand.LastInsertedId;
                    mainWindow.Logging("Польщователь зарегестрировался");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Такой пользователь уже существует!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
