using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WorkshopProject
{ 
    public partial class Form1 : Form
    {
        private SqlConnection conn = new SqlConnection();
        private string conString = "Server = NILOY; Database = Workshop; User = sa; password = 1234";
        private SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }
        

        //for showing value in gridview

        private void Form1_Load_1(object sender, EventArgs e)
        {
            conn.ConnectionString = conString;

            cmd = conn.CreateCommand();
            try
            {
                string query = "sp_ShowAllData";
                cmd.CommandText = query;
                conn.Open();




                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridView1.DataSource = dt;

                reader.Close();
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }

        }
        //click and show value

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            tb_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tb_name.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tb_email.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tb_address.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        //rafresh all

        private void refresh_Click_1(object sender, EventArgs e)
        {
            Form1_Load_1(this, e);
            tb_id.Clear();
            tb_name.Clear();
            tb_email.Clear();
            tb_address.Clear();
            tb_search.Clear();
        }
        //insert value

        private void Save_Click(object sender, EventArgs e)
        {

            string name = tb_name.Text;
            string address = tb_address.Text;
            string email = tb_email.Text;
            string id = tb_id.Text;

            if ((name == "") || (address == "") || (email == ""))
            {
                string msg = "No text box can be empty";
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                conn.ConnectionString = conString;
                cmd = conn.CreateCommand();
                try
                {
                    string query = "sp_insertMember";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar);
                    cmd.Parameters["@id"].Value = id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar);
                    cmd.Parameters["@name"].Value = name;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar);
                    cmd.Parameters["@address"].Value = address;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar);
                    cmd.Parameters["@email"].Value = email;
                    cmd.CommandText = query;
                    conn.Open();
                    cmd.ExecuteScalar();
                    MessageBox.Show("Value Successfully saved\n" + "Your ID: " + tb_id.Text + "\nYour Name: " + tb_name.Text + "\nGmail: " + tb_address.Text + "\nAddress: " + tb_email.Text);
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                    tb_id.Clear();
                    tb_name.Clear();
                    tb_email.Clear();
                    tb_address.Clear();
                    tb_search.Clear();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                    string caption = "Error";
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }

                Form1_Load_1(this, e);


            }
        }

        //value update
        private void update_Click_1(object sender, EventArgs e)
        {
            string name = tb_name.Text;
            string address = tb_address.Text;
            string email = tb_email.Text;
            string id = tb_id.Text;
            string updateid = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if ((name == "") || (address == "") || (email == ""))
            {
                string msg = "No text box can be empty";
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                conn.ConnectionString = conString;
                cmd = conn.CreateCommand();
                try
                {
                    string query = "sp_update";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@updateid", SqlDbType.VarChar);
                    cmd.Parameters["@updateid"].Value = updateid;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar);
                    cmd.Parameters["@name"].Value = name;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar);
                    cmd.Parameters["@address"].Value = address;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar);
                    cmd.Parameters["@email"].Value = email;
                    cmd.CommandText = query;
                    conn.Open();
                    cmd.ExecuteScalar();
                    MessageBox.Show("Value of ID - "+updateid+" Updated successfully");
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                    tb_id.Clear();
                    tb_name.Clear();
                    tb_email.Clear();
                    tb_address.Clear();
                    tb_search.Clear();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                    string caption = "Error";
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }

                Form1_Load_1(this, e);


            }
        }

        //value delete

        private void Delete_Click(object sender, EventArgs e)
        {

            String n = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            conn.ConnectionString = conString;
            cmd = conn.CreateCommand();
            try
            {
                string query = "sp_delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@n", SqlDbType.VarChar);
                cmd.Parameters["@n"].Value = n;
                cmd.CommandText = query;
                conn.Open();
                cmd.ExecuteScalar();
                MessageBox.Show("Value of "+n+" deleted successfully");
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                tb_id.Clear();
                tb_name.Clear();
                tb_email.Clear();
                tb_address.Clear();
                tb_search.Clear();
            }
            catch (Exception ex)
            { 
                string msg = ex.Message.ToString();
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }

            Form1_Load_1(this, e);
        }
    

         
        //value search

        private void Search_Click_1(object sender, EventArgs e)
        {
            String s = tb_search.Text;

            conn.ConnectionString = conString;
            cmd = conn.CreateCommand();
            try
            {
                string query = "sp_search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@s", SqlDbType.VarChar);
                cmd.Parameters["@s"].Value = s;
                cmd.CommandText = query;
                conn.Open();
                cmd.ExecuteScalar();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridView1.DataSource = dt;

                reader.Close();
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                tb_search.Clear();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                string caption = "Error";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        
    }
}
    
    
