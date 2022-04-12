using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Логика взаимодействия для LoggerWindow.xaml
    /// </summary>
    public partial class LoggerWindow : Window
    {
        private string sql = "SELECT user_action.id AS action_id, user_action.name AS action_name, user_action.time AS action_time, user.login " +
            "FROM mdisubd.user_action, mdisubd.user" +
            " WHERE user_action.user_id = user.id";
        private MySqlConnection connection;
        private MySqlDataAdapter adapter;
        private DataTable table;
        private string connectionString;
        public LoggerWindow()
        {
            InitializeComponent();
            connection = null;
            connectionString = connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            table = new DataTable();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(sql, connection);
                adapter = new MySqlDataAdapter(command);
                connection.Open();
                adapter.Fill(table);
                logGrid.ItemsSource = table.DefaultView;
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
