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

namespace Phonebook
{
    public partial class Phonebook : Form
    {
        public Phonebook()
        {
            InitializeComponent();
        }
        string ConString = @"Data Source=.\SQLSERVER2014;Initial Catalog=PhonebookDB;Integrated Security=True";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlDataReader dr;
        DataTable dt = new DataTable();



        void ClearAll()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtTel.Clear();
            txtAddress.Clear();
            txtName.Focus();
            lblID.Text = "";
            txtSearch.Clear();

        }

        object loadData()
        {
            String query = "Select * From tblPhonebook ";
            con = new SqlConnection(ConString);
            con.Open();
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dgvPhonebook.DataSource = dt;

            }
            else
            {
                dt = null;
                return dt;
            }
        }


        private void dgvPhonebook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearAll();
            loadData();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtEmail.Text == "" || txtTel.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Null parameters", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string query = "Insert into tblPhonebook(Name, Tel, Email, Address) values('" + txtName.Text
                    + "', '" + txtTel.Text + "', '" + txtEmail.Text + "', '" + txtAddress.Text + "')";
                con = new SqlConnection(ConString);
                con.Open();
                cmd = new SqlCommand(query, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Insert Successful", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Insert Failed", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClearAll();
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Null parameters", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string query = "Delete from tblPhonebook where PhonebookID = '" + lblID.Text + "'";
                con = new SqlConnection(ConString);
                con.Open();
                cmd = new SqlCommand(query, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Delete Successful", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delete Failed", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClearAll();
                loadData();
            }
        }

        private void dgvPhonebook_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblID.Text = dgvPhonebook.SelectedRows[0].Cells[0].Value.ToString();
            txtName.Text = dgvPhonebook.SelectedRows[0].Cells[1].Value.ToString();
            txtTel.Text = dgvPhonebook.SelectedRows[0].Cells[2].Value.ToString();
            txtEmail.Text = dgvPhonebook.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = dgvPhonebook.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (lblID.Text == "")
            {
                MessageBox.Show("Null parameters", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string query = "Update tblPhonebook set Name ='" + txtName.Text
                    + "', Tel = '" + txtTel.Text + "', Email ='" + txtEmail.Text + "', Address = '" + txtAddress.Text + "' where PhonebookID = '" + lblID.Text + "'";
                con = new SqlConnection(ConString);
                con.Open();
                cmd = new SqlCommand(query, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Update Successful", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Update Failed", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClearAll();
                loadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Null parameters", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string query = "Select * From tblPhonebook Where Name = '" + txtSearch.Text + "' ";
                con = new SqlConnection(ConString);
                con.Open();
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dgvPhonebook.DataSource = dt;
                    MessageBox.Show("Search Found", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    loadData();
                    MessageBox.Show("Search Not Found", "Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.ShowDialog();
        }
    }

}
            

    








