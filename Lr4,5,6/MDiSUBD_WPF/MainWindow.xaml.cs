using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace MDiSUBD_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString;
        private DataTable oper;
        private MySqlDataAdapter adapt;
        private MySqlConnection connection;
        private string sql = "SELECT order.id, customer.first_name AS c_name, workshop.name AS w_name,  " +
                "order.number, detail.name AS d_name, detail.price AS d_price, car_photo.path AS p_name, master.first_name AS m_name, " +
                "work_type.name AS wt_name, work_type.price AS wt_price, payment_type. name AS pt_name, " +
                "user.login AS u_name " +
                "FROM mdisubd.order, mdisubd.customer, mdisubd.workshop, mdisubd.detail, mdisubd.car_photo, " +
                "mdisubd.master, mdisubd.work_type, mdisubd.payment_type, mdisubd.user " +
                "WHERE order.id_customer=customer.id AND order.id_workshop=workshop.id AND order.id_detail=detail.id " +
                "AND order.id_car_photo=car_photo.id AND order.id_master=master.id AND order.id_work_type=work_type.id " +
                "AND order.id_payment_type=payment_type.id AND order.id_user=user.id ";
        public static int currentUserId = 0;
        public static int currentAdmin = 0;


        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = null;
            oper = new DataTable();

            try
            {
                string sqlInitialize = sql + "ORDER BY order.id";

                connection = new MySqlConnection(connectionString);
                MySqlCommand comm = new MySqlCommand(sqlInitialize, connection);
                adapt = new MySqlDataAdapter(comm);
                connection.Open();
                adapt.Fill(oper);
                infoGrid.ItemsSource = oper.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            Delete.IsEnabled = Convert.ToBoolean(currentAdmin);
            logButton.IsEnabled = Convert.ToBoolean(currentAdmin);
        }

        private int GetCell(int iCell)
        {
            var selected = infoGrid.SelectedCells[iCell];
            var cellContent = selected.Column.GetCellContent(selected.Item);
            int selectedId = int.Parse((cellContent as TextBlock).Text);
            return selectedId;
        }

        public void Logging(string log)
        {
            string logTime = DateTime.Now.ToString();
            string sql = "INSERT INTO mdisubd.user_action(name, time, user_id) VALUES ('" + log + "', '" + logTime + "', " + currentUserId + ")";
            MySqlCommand logCommand = new MySqlCommand(sql, connection);
            connection.Open();
            logCommand.ExecuteNonQuery();
            connection.Close();
        }

        private void TableUpdate(string sql)
        {
            MySqlCommand comm = new MySqlCommand(sql, connection);
            adapt = new MySqlDataAdapter(comm);
            connection.Open();
            DataTable ordered = new DataTable();
            adapt.Fill(ordered);
            infoGrid.ItemsSource = ordered.DefaultView;
            connection.Close();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            infoGrid.Items.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int selectedId = GetCell(0);
            var selected = infoGrid.SelectedCells[0];
            int selectedRow = infoGrid.Items.IndexOf(selected.Item);
            string sqlDelete = "DELETE FROM mdisubd.order WHERE id=" + selectedId;
            MySqlCommand commD = new MySqlCommand(sqlDelete, connection);
            connection.Open();
            commD.ExecuteNonQuery();
            connection.Close();
            TableUpdate(sql + " ORDER BY order.id");
            Logging("Заказ удален");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
            TableUpdate(sql+ " ORDER BY order.id");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string number = ((DataRowView)infoGrid.Items.GetItemAt(infoGrid.SelectedIndex)).Row.ItemArray[3].ToString();
            int id = int.Parse(((DataRowView)infoGrid.Items.GetItemAt(infoGrid.SelectedIndex)).Row.ItemArray[0].ToString());
            string sqlUpdate = "UPDATE mdisubd.order SET number='" + number + "'" +
                " WHERE id=" + id;
            MySqlCommand commD = new MySqlCommand(sqlUpdate, connection);
            connection.Open();
            commD.ExecuteNonQuery();
            connection.Close();
            TableUpdate(sql);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string sqlC = sql + " ORDER BY c_name";
            TableUpdate(sqlC);
            Logging("Выполнена сортировка по имени заказчика");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string sqlO = sql + " ORDER BY c_name, d_price";
            TableUpdate(sqlO);
            Logging("Выполнена сортировка по цене");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string sqlS = sql + " AND customer.first_name = '" + SearchText.Text + "'";
            string sST = (SearchCombo.SelectedItem as TextBlock).Text;
            if (sST == "Тип работы")
                sqlS = sql + " AND work_type.name = '" + SearchText.Text + "'";
            else
                if (sST == "Мастер")
                    sqlS = sql + " AND master.first_name = '" + SearchText.Text + "'";
            TableUpdate(sqlS);
            Logging("Выполнен поиск по заданному параметру");
        }

        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            LoggerWindow loggerWindow = new LoggerWindow();
            loggerWindow.ShowDialog();
            Logging("Выведен журнал действий");
        }

        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow customerWindow = new AddCustomerWindow();
            customerWindow.ShowDialog();
        }
    }
}
