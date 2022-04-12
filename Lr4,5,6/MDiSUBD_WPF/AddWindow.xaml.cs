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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private string connectionString;
        private MySqlConnection connection;
        private MainWindow mainWindow;


        private MySqlDataAdapter adapterCustomer;
        private DataTable tableCustomer;
        private string sql = "SELECT customer.first_name AS customer_name, customer.id AS customer_id " +
                         "FROM mdisubd.customer";

        private string sqlStation = "SELECT workshop.name AS workshop_name, workshop.address AS workshop_address, workshop.id AS workshop_id " +
                                    "FROM mdisubd.workshop";
        private MySqlDataAdapter adapterStation;
        private DataTable tableStation;


        private string sqlDetail = "SELECT detail.id AS detail_id, detail.name AS detail_name, detail.producer AS detail_producer, detail.price AS detail_price " +
                                    "FROM mdisubd.detail";
        private MySqlDataAdapter adapterDetail;
        private DataTable tableDetail;

        private string sqlMaster = "SELECT master.id AS master_id, master.first_name AS master_name, master.category AS master_category FROM mdisubd.master";
        private MySqlDataAdapter adapterMaster;
        private DataTable tableMaster;

        private string sqlWork = "SELECT work_type.id AS work_id, work_type.name AS work_type, work_type.price AS work_price FROM mdisubd.work_type";
        private MySqlDataAdapter adapterWork;
        private DataTable tableWork;

        private string sqlPayment = "SELECT payment_type.id AS payment_id, payment_type.name AS payment_name FROM mdisubd.payment_type";
        private MySqlDataAdapter adapterPayment;
        private DataTable tablePayment;


        private string sqlCarPhoto = "SELECT car_photo.id AS car_id, car_photo.path AS car_path FROM mdisubd.car_photo";
        private MySqlDataAdapter adapterPhoto;
        private DataTable tablePhoto;

        public AddWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectionString = connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = null;
            mainWindow = new MainWindow();

            tableCustomer = new DataTable();

            tableStation = new DataTable();


            tableDetail = new DataTable();

            tableMaster = new DataTable();

            tableWork = new DataTable();

            tablePayment = new DataTable();


            tablePhoto = new DataTable();

            try
            {
                connection = new MySqlConnection(connectionString);

                MySqlCommand commandCustomer = new MySqlCommand(sql, connection);
                adapterCustomer = new MySqlDataAdapter(commandCustomer);
                connection.Open();
                adapterCustomer.Fill(tableCustomer);
                customerComboBox.ItemsSource = tableCustomer.DefaultView;

                MySqlCommand commandStation = new MySqlCommand(sqlStation, connection);
                adapterStation = new MySqlDataAdapter(commandStation);
                adapterStation.Fill(tableStation);
                stationComboBox.ItemsSource = tableStation.DefaultView;


                MySqlCommand commandDetail = new MySqlCommand(sqlDetail, connection);
                adapterDetail = new MySqlDataAdapter(commandDetail);
                adapterDetail.Fill(tableDetail);
                detailComboBox.ItemsSource = tableDetail.DefaultView;

                MySqlCommand commandMaster = new MySqlCommand(sqlMaster, connection);
                adapterMaster = new MySqlDataAdapter(commandMaster);
                adapterMaster.Fill(tableMaster);
                masterComboBox.ItemsSource = tableMaster.DefaultView;

                MySqlCommand commandWork = new MySqlCommand(sqlWork, connection);
                adapterWork = new MySqlDataAdapter(commandWork);
                adapterWork.Fill(tableWork);
                workComboBox.ItemsSource = tableWork.DefaultView;

                MySqlCommand commandPayment = new MySqlCommand(sqlPayment, connection);
                adapterPayment = new MySqlDataAdapter(commandPayment);
                adapterPayment.Fill(tablePayment);
                paymentComboBox.ItemsSource = tablePayment.DefaultView;


                MySqlCommand commandPhoto = new MySqlCommand(sqlCarPhoto, connection);
                adapterPhoto = new MySqlDataAdapter(commandPhoto);
                adapterPhoto.Fill(tablePhoto);
                photoComboBox.ItemsSource = tablePhoto.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            int idCustomer = int.Parse(((DataRowView)customerComboBox.Items.GetItemAt(customerComboBox.SelectedIndex)).Row.ItemArray[1].ToString());
            int idStation = int.Parse(((DataRowView)stationComboBox.Items.GetItemAt(stationComboBox.SelectedIndex)).Row.ItemArray[2].ToString());
            string orderNumber = orderNumberTextBox.Text;
            int idDetail = int.Parse(((DataRowView)detailComboBox.Items.GetItemAt(detailComboBox.SelectedIndex)).Row.ItemArray[0].ToString());
            int idMaster = int.Parse(((DataRowView)masterComboBox.Items.GetItemAt(masterComboBox.SelectedIndex)).Row.ItemArray[0].ToString());
            int idWork = int.Parse(((DataRowView)workComboBox.Items.GetItemAt(workComboBox.SelectedIndex)).Row.ItemArray[0].ToString());
            int idPayment = int.Parse(((DataRowView)paymentComboBox.Items.GetItemAt(paymentComboBox.SelectedIndex)).Row.ItemArray[0].ToString());
            int idPhoto = int.Parse(((DataRowView)photoComboBox.Items.GetItemAt(photoComboBox.SelectedIndex)).Row.ItemArray[0].ToString());

            string sqlInsert = "INSERT INTO mdisubd.order(id_customer, id_workshop, id_detail, " +
                "id_car_photo, id_master, id_work_type, id_payment_type, id_user, number) " +
                "VALUES (" + idCustomer + ", " + idStation + ", " + idDetail + ", "  +
                idPhoto + ", " + idMaster + ", " + idWork + ", " + idPayment + ", " + MainWindow.currentUserId + ", '" + orderNumber + "')";
            MySqlCommand commI = new MySqlCommand(sqlInsert, connection);
            connection.Open();
            commI.ExecuteNonQuery();
            connection.Close();
            this.Close();
            mainWindow.Logging("Добавлен заказ");
        }
    }
}
