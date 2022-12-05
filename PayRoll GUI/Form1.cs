using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PayRoll_GUI.ClassScripts;
using MySql.Data.MySqlClient;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Org.BouncyCastle.Crypto;

namespace PayRoll_GUI
{
    public partial class Form1 : Form
    {
        // MySqlConnection con = new MySqlConnection(
        //   "datasource=127.0.0.1;port=3306; server = localhost; userid = root; password = ; database = ipt"
        //);

        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=ipt;";


        public List<Panel> Menus = new List<Panel>();

        public Form1()
        {
            InitializeComponent();
            Menus.Add(pnlAddEmployee);
            Menus.Add(pnlRemoveEmployee);
            Menus.Add(pnlDisplayEmployees);
            Menus.Add(pnlAddAddress);
        }

        void HideAllMenus()
        {
            foreach (var menu in Menus)
            {
                menu.Visible = false;
            }
        }


        //Loads a given Menu
        private void LoadAddEmployeeMenu(object sender, EventArgs e)
        {
            HideAllMenus();
            pnlAddEmployee.Visible = true;
        }
        private void LoadRemoveEmployeeMenu(object sender, EventArgs e)
        {
            HideAllMenus();
            pnlRemoveEmployee.Visible = true;
        }

        private void LoadAddAddressMenu(object sender, EventArgs e)
        {
            HideAllMenus();
            pnlAddAddress.Visible = true;
        }
        private void LoadDisplayEmployeesMenu(object sender, EventArgs e)
        {
            HideAllMenus();
            pnlDisplayEmployees.Visible = true;
            DisplayEmployees();
        }

        /// <summary>
        /// checks the added employee to see if there is someone with that name on the payroll and if there is updates the data instead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmNewEmployeeData(object sender, EventArgs e)
        {


            float Wage = 0;
            float HoursWorked = 0;
            float.TryParse(txbWage.Text, out Wage);
            float.TryParse(txbHoursWorked.Text, out HoursWorked);
            float salary = Wage * HoursWorked;



            string query = "INSERT INTO `users` (`First Name`, `Last Name`, `Salary`, `Job Title`) VALUES " +
                " ('" + txbFirstName.Text + "', '" + txblastName.Text + "', '" + salary + "', '" + txbJobTitle.Text + "')";

            // Which could be translated manually to :
            // INSERT INTO user(`id`, `first_name`, `last_name`, `address`) VALUES (NULL, 'Bruce', 'Wayne', 'Wayne Manor')

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("User succesfully registered");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }









            long intConv = 0;
            Int64.TryParse(txbAppNum.Text, out intConv);
            int AppNum = (int)intConv;
            Int64.TryParse(txbZip.Text, out intConv);
            int ZIP = (int)intConv;

            Address newAddress = new Address(txbAdress.Text, AppNum, txbCity.Text, txbState.Text, ZIP);

            int PhoneNumber = (int)intConv;
            

            Employee newEmployee = new Employee(txbFirstName.Text, txblastName.Text, PhoneNumber, newAddress, txbJobTitle.Text, Wage, HoursWorked, chbOnPayroll.Checked);
            Program.PayRoll.Add(newEmployee);
            lblConfirmText.Text = txbFirstName.Text + " " + txblastName.Text + " was added to the payroll";



          //  runquery();




        }
        public void runquery()
        {




          







            //  First Name  Last Name   Salary Job Title













        }

        /// <summary>
        /// checks to see if there is a  employee by that name and removes them if there is one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveEmployee(object sender, EventArgs e)
        {
            foreach (var employee in Program.PayRoll)
            {
                if(employee.FirstName == txbRemoveFirstName.Text &&employee.LastName == txbRemoveLastName.Text)
                {
                    Program.PayRoll.Remove(employee);
                    lblConfirmRemove.Text = employee.FirstName + " " +employee.LastName + " was successfully removed from the payroll";
                    return;
                }
            }

            lblConfirmRemove.Text = "that employee does not exist";

        }

        /// <summary>
        /// Displays all the employees on the payroll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayEmployees()
        {
            runquery();






            DataTable dataTable = new DataTable("Employees");
            dataTable.Clear();
            dataTable.Columns.Add("FirstName", typeof(String));
            dataTable.Columns.Add("LastName", typeof(String));
            dataTable.Columns.Add("JobTitle", typeof(String));
            dataTable.Columns.Add("Hours Worked", typeof(float));
            dataTable.Columns.Add("Wage", typeof(float));

            foreach (var employee in Program.PayRoll)
            {
                DataRow row = dataTable.NewRow();
                row["Firstname"] = employee.FirstName;
                row["LastName"] = employee.LastName;
                row["JobTitle"] = employee.JobTitle;
                row["Hours Worked"] = employee.HoursWorked;
                row["Wage"] = employee.Wage;
                dataTable.Rows.Add(row);




               // float salary = employee.Wage * employee.HoursWorked;




















            }

            grdEmployeeData.DataSource = dataTable;


        }

        private void PayEmployees(object sender, EventArgs e)
        {
            lblPayEmployees.Text = "You Paid :" + Program.PayEmployees();
        }

        private void label1_Click(object sender, EventArgs e)
        {









           
































        }


    }

  


    }

