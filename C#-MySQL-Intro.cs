// David Emery 12/08/14
// A C# program which deonstrates basic connectioon to a local MySQL database.  

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql;

namespace Connecting_to_a_MySQL_Database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                MySql.Data.MySqlClient.MySqlConnection cn = new MySql.Data.MySqlClient.MySqlConnection();
                // Assign the connection string:
                cn.ConnectionString = "server=localhost;user id=root;database=test";
                cn.Open();

                status(true); // Sets status to true since the opening above was successful.
                // Selects all(*) from the users table:
                String querystring = "select * from users";
                MySql.Data.MySqlClient.MySqlCommand cmd =
                    new MySql.Data.MySqlClient.MySqlCommand(querystring, cn);
                MySql.Data.MySqlClient.MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    { lbPeople.Items.Add(dr.GetString("username")); }
                    catch (IndexOutOfRangeException iore)
                    {
                        lblDebug.Text = iore.ToString();
                    }
                }
            }
            catch (System.ArgumentException connError)
            {
                lblDebug.Text = connError.ToString();
                status(false);
            }
            catch (MySql.Data.MySqlClient.MySqlException MySQLExc)
            {
                lblDebug.Text = MySQLExc.ToString();
                status(false);
            }

        }

        // This function handles things related to status messages to the form
        public void status(Boolean b)
        {
            if (b)
            { //success:
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Connected!";
            }
            else
            { // fail:
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Connection Failed";
            }
        }
    }
}
