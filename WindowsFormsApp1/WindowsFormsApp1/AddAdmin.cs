using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddAdmin : Form
    {
        DBConnect dbCon = new DBConnect();
        public AddAdmin()
        {
            InitializeComponent();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminName.Text == String.Empty || txtAdminID.Text == String.Empty || txtPassword.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Valid Admin Name, Admin User ID and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clrbtn();
                }
                else
                {
                    // check duplicate record
                    SqlCommand cmd = new SqlCommand("select AdminID from tblAdmin where AdminID=@AdminID", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@AdminID", txtAdminID.Text);
                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Admin ID Already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clrbtn();
                    }
                    else
                    {
                        cmd = new SqlCommand("spAddAdmin", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@AdminID", txtAdminID.Text);
                        cmd.Parameters.AddWithValue("@Password ", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@FullName ", txtAdminName.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Admin inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clrbtn();
                            BindAdmin();
                        }
                    }
                    dbCon.CloseCon();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void BindAdmin()
        {
            SqlCommand cmd = new SqlCommand("select * from tblAdmin", dbCon.GetCon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dbCon.OpenCon();
        }

        private void AddAdmin_Load(object sender, EventArgs e)
        {
            lblAdminID.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnAdd.Visible = true;
            txtAdminName.Focus();
            BindAdmin();
        }
        private void clrbtn()
        {
            txtAdminID.Clear();
            txtAdminName.Clear();
            txtPassword.Clear();
            txtAdminName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminName.Text == String.Empty || txtAdminID.Text == String.Empty || txtPassword.Text == String.Empty || lblAdminID.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Valid Admin Name, Admin User ID and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clrbtn();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("spUpdateAdmin", dbCon.GetCon());
                    dbCon.OpenCon();
                    cmd.Parameters.AddWithValue("@AdminID", lblAdminID.Text);
                    cmd.Parameters.AddWithValue("@Password ", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@FullName ", txtAdminName.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Admin record updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clrbtn();
                        BindAdmin();
                    }
                    dbCon.CloseCon();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblAdminID.Text == String.Empty)
                {
                    MessageBox.Show("Please select Admin record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("spDeleteAdmin", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@AdminID", lblAdminID.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        dbCon.OpenCon();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Admin Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clrbtn();
                            BindAdmin();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            lblAdminID.Visible = false;
                            btnAdd.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Delete Failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clrbtn();
                        }
                        dbCon.CloseCon();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                lblAdminID.Visible = true;
                btnAdd.Visible = false;

                lblAdminID.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                txtAdminID.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                txtPassword.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                txtAdminName.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            
        }
    }
}
